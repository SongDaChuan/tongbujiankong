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
    public  class MonitorList
    {
        public static DataTable GetPublicationTable(string serverName)
        {
            string connectionString = ConnectionStringFactory.NXJCConnectionString;
            ISqlServerDataFactory dataFactory = new SqlServerDataFactory(connectionString);
            string mySql = @"select publisher_database_id,'['+publisher_db+']:'+publication as publisher,subscriber_id,subscriber_db,creation_date
                        from [distribution].[dbo].[MSdistribution_agents]
                        where subscriber_id>0
                        group by publisher_db,publication,publisher_database_id,subscriber_db,subscriber_id,creation_date
                        order by publisher_db,publication";
            SqlParameter parameter = new SqlParameter("serverName",serverName);
            DataTable originalTable = dataFactory.Query(mySql, parameter);
            return originalTable;
        }
        public static DataTable GetSubscribeTable(string serverName)
        {
            string connectionString = ConnectionStringFactory.NXJCConnectionString;
            ISqlServerDataFactory dataFactory = new SqlServerDataFactory(connectionString);
            string mySql = @"select publisher_database_id,'['+publisher_db+']:'+publication as publisher,subscriber_id,subscriber_db,creation_date
                        from [distribution].[dbo].[MSdistribution_agents]
                        where subscriber_id>0
                        group by publisher_db,publication,publisher_database_id,subscriber_db,subscriber_id,creation_date
                        order by publisher_db,publication";
            SqlParameter parameter = new SqlParameter("serverName", serverName);
            DataTable originalTable = dataFactory.Query(mySql, parameter);
            return originalTable;
        }
       


    }
}
