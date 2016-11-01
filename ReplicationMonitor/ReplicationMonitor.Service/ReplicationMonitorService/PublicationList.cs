using ReplicationMonitor.Infrastructure.Configuration;
using SqlServerDataAdapter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplicationMonitor.Service.ReplicationMonitorService
{
   public class PublicationList
    {
       public static DataTable GetPublisherMonitorListTable(string mlinkServer, string mpublicattionId)
       {
           string connectionString = ConnectionStringFactory.NXJCConnectionString;
           ISqlServerDataFactory dataFactory = new SqlServerDataFactory(connectionString);
           DataTable table = new DataTable();
           if (mpublicattionId.Equals(""))
           {
               string mySql = @"select '['+[publisher_db]+']:'+ [publication] as publisher, * from [{0}].[distribution].[dbo].[MSpublications] order by publisher_db";
               mySql = string.Format(mySql, mlinkServer);
               table = dataFactory.Query(mySql);
           }
           else {
               string mySql = @"select '['+[publisher_db]+']:'+ [publication] as publisher, * from [{0}].[distribution].[dbo].[MSpublications] 
                               where publication_id={1}
                               order by publisher_db";
               mySql = string.Format(mySql, mlinkServer, mpublicattionId);
               table = dataFactory.Query(mySql);
           }
           return table;
       }
    }
}
