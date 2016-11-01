using ReplicationMonitor.Infrastructure.Configuration;
using SqlServerDataAdapter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ReplicationMonitor.Service.ReplicationMonitorService
{
    public class MonitorNodes
    {
        private static DataTable GetServersDataTable()
        {
            string connectionString = ConnectionStringFactory.NXJCConnectionString;
            ISqlServerDataFactory dataFactory = new SqlServerDataFactory(connectionString);

            string mySql = @"select * from [IndustryEnergy_SH].[dbo].[linkServer]  where name !='' and enabled=1 order by displayIndex";
            //测试语句
//            string mySql = @"select * from [IndustryEnergy_SH].[dbo].[linkServer]  where name !='' and enabled=1   
//                               and link_name='BaiYin' 
//                              order by displayIndex";
            DataTable serverTable = dataFactory.Query(mySql);
            return serverTable;
        }
        public static DataTable GetMonitorNodesTable() 
        {
            DataTable monitorNodesTable = MonitorNodesTableStructure();
            DataTable serverTable = GetServersDataTable();
            int tCount = serverTable.Rows.Count;
            for (int i = 0; i < tCount;i++ ) 
            {
                string serverName = serverTable.Rows[i]["name"].ToString().Trim();
                string linkName = serverTable.Rows[i]["link_name"].ToString().Trim();
                string levelCode="M01" + (i + 1).ToString("00");
             
               
                string connectionString = ConnectionStringFactory.NXJCConnectionString;
                ISqlServerDataFactory dataFactory = new SqlServerDataFactory(connectionString);
                string mySql = @"select publication,publication_id as publicationId from [{0}].[distribution].[dbo].[MSpublications]  order by publisher_db  ";
                mySql = string.Format(mySql, linkName);
                DataTable nodesTable = new DataTable();                   
                try
                {                   
                    nodesTable = dataFactory.Query(mySql);
                    monitorNodesTable.Rows.Add(serverName, "icon-add", levelCode, linkName, serverName);
                }
                catch (Exception e)
                {
                    monitorNodesTable.Rows.Add(serverName, "icon-no", levelCode, linkName);
                }
                int  nodesTableCount = nodesTable.Rows.Count;
                for (int j = 0; j < nodesTableCount; j++)
                {
                    monitorNodesTable.Rows.Add(nodesTable.Rows[j]["publication"], "", levelCode + (j + 1).ToString("00"), linkName, serverName,nodesTable.Rows[j]["publicationId"]);
                }                                     
            }
            return monitorNodesTable;
        }
  
        private static DataTable MonitorNodesTableStructure() 
        {
            DataTable monitorNodesTable = new DataTable();
            monitorNodesTable.Columns.Add("text", typeof(string));
            monitorNodesTable.Columns.Add("iconCls", typeof(string));
            monitorNodesTable.Columns.Add("levelCode", typeof(string));
            monitorNodesTable.Columns.Add("linkServer", typeof(string));
            monitorNodesTable.Columns.Add("serverName", typeof(string));
            monitorNodesTable.Columns.Add("publicationId", typeof(string));
            return monitorNodesTable;
        
        }
        public static DataTable GetArticlesTable(string mlinkServer,string mpublicattionId)
        {
            string connectionString = ConnectionStringFactory.NXJCConnectionString;
            ISqlServerDataFactory dataFactory = new SqlServerDataFactory(connectionString);
            string mySql = @"select * from [{0}].[distribution].[dbo].[MSarticles] where publication_id=@publicattionId";
            mySql = string.Format(mySql, mlinkServer);
            SqlParameter para = new SqlParameter("@publicattionId", mpublicattionId);
            DataTable articlesTable = dataFactory.Query(mySql, para);
            return articlesTable;
        }
    }
}
