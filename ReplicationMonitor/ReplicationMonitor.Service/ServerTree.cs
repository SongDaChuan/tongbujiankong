using ReplicationMonitor.Infrastructure.Configuration;
using SqlServerDataAdapter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplicationMonitor.Service.ServerSelector
{
    public class ServerTree
    {
        public static DataTable GetServerTable(string serveName)
        {
            string connectionString = ConnectionStringFactory.NXJCConnectionString;
            ISqlServerDataFactory dataFactory = new SqlServerDataFactory(connectionString);
            string mySql01 = @"select publisher from {0}.[distribution].[dbo].[MSreplication_monitordata]
                            group by publisher
                            order by publisher";         //获取发布服务器
           mySql01= string.Format(mySql01, serveName);
            DataTable originalTable = dataFactory.Query(mySql01);
            DataColumn levelCode = new DataColumn("levelcode", typeof(string));
            originalTable.Columns.Add(levelCode);
            string SelectString = "publisher='" + serveName + "\'";
            DataRow[] publisherRows = originalTable.Select(SelectString);
            int length = publisherRows.Count();
            for (int i = 0; i < length; i++)
            {
                DataRow dr = publisherRows[i];
                dr["levelcode"] = "M" + (i + 1).ToString("00");

                string mySql02 = @"select '['+A.publisher_db+']:'+B.publication as publisher from {0}.[distribution].[dbo].[MSarticles] A,
                                            {1}.[distribution].[dbo].[MSreplication_monitordata] B 
                                    where A.publication_id=B.publication_id
                                       and B.publisher=@publisher
                                    group by A.publisher_db,B.publication
                                    order by publisher";                         //获取发布节点
               mySql02= string.Format(mySql02,serveName,serveName);
               SqlParameter parameter = new SqlParameter("publisher", dr["publisher"]);
               DataTable publicationTable = dataFactory.Query(mySql02, parameter);
               publicationTable.Columns.Add(new DataColumn("levelcode", typeof(string)));
               int pubLength = publicationTable.Rows.Count;
               for (int j = 0; j < pubLength; j++)
               {
                   publicationTable.Rows[j]["levelcode"] = "M" + (i + 1).ToString("00") + (j + 1).ToString("00");
                   DataRow pr = publicationTable.Rows[j];
                   string mySql03 = @"select article as publisher from 
                                            (select B.publisher,'['+A.publisher_db+']:'+B.publication as publication,A.article as article 
                                            from {0}.[distribution].[dbo].[MSarticles] A,{1}.[distribution].[dbo].[MSreplication_monitordata] B 
                                            where A.publication_id=B.publication_id) T
                                       where T.publisher=@publisher
		                                and publication=@publication
                                        group by article
                                        order by article ";
                   mySql03 = string.Format(mySql03, serveName, serveName);        
                   SqlParameter[] parameters = { new SqlParameter("publisher", dr["publisher"]), new SqlParameter("publication", pr["publisher"]) };
                   DataTable articleTable = dataFactory.Query(mySql03, parameters);
                   articleTable.Columns.Add(new DataColumn("levelcode", typeof(string)));
                   int artLength = articleTable.Rows.Count;
                   for (int k = 0; k < artLength; k++)
                   {
                       articleTable.Rows[k]["levelcode"] = "M" + (i + 1).ToString("00") + (j + 1).ToString("00") + (k + 1).ToString("00");
                   }
                   originalTable.Merge(articleTable);
               }
               originalTable.Merge(publicationTable);
            }


            DataRow[] rows = originalTable.Select("LevelCode='M01'");
            foreach (DataRow rw in rows)
            {
                originalTable.Rows.Remove(rw);
            }
            originalTable.AcceptChanges();  
            return originalTable;
        }
    }
}
