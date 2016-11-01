using ReplicationMonitor.Infrastructure.Configuration;
using SqlServerDataAdapter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplicationMonitor.Service.ReplicationMonitorService
{
    public class SubscribeList
    {

        public static DataTable GetSubscribeMonitorListTable(string mlinkServer, string mpublicattionId)
        {
            string connectionString = ConnectionStringFactory.NXJCConnectionString;
            ISqlServerDataFactory dataFactory = new SqlServerDataFactory(connectionString);
            DataTable table = new DataTable();
            if (mpublicattionId.Equals(""))
            {
                string mySql = @"SELECT B.[publisher],
                                A.[publisher_database_id]
                                ,A.[publisher_id]
                                ,A.[publisher_db]
                                ,A.[publication_id]
                                ,A.[subscriber_id]
                                ,A.[subscriber_db]
                                ,A.[subscription_type]
                                ,A.[sync_type]
                                ,A.[status]
                                ,A.[snapshot_seqno_flag]
                                ,A.[independent_agent]
                                ,A.[loopback_detection]
                                ,A.[agent_id]
                                ,A.[update_mode]
                                ,A.[nosync_type]
                                FROM [{0}].[distribution].[dbo].[MSsubscriptions] A,(select '['+publisher_db+']:'+publication as publisher
                                    ,[publication_id]
                                    from  [{1}].[distribution].[dbo].[MSpublications]) B
                                    where A.subscriber_id>0 and A.[publication_id]=B.[publication_id]
                                group by A.[publisher_database_id]
                                        ,A.[publisher_id]
                                        ,A.[publisher_db]
                                        ,A.[publication_id]
                                        ,A.[subscriber_id]
                                        ,A.[subscriber_db]
                                        ,A.[subscription_type]
                                        ,A.[sync_type]
                                        ,A.[status]
                                        ,A.[snapshot_seqno_flag]
                                        ,A.[independent_agent]
                                        ,A.[loopback_detection]
                                        ,A.[agent_id]
                                        ,A.[update_mode]
                                        ,A.[nosync_type],B.[publisher]
                                    order by [publisher_database_id],[publication_id] ";
                mySql = string.Format(mySql, mlinkServer, mlinkServer);
                table = dataFactory.Query(mySql);
            }
            else
            {
                string mySql = @"SELECT B.[publisher],
                                A.[publisher_database_id]
                                ,A.[publisher_id]
                                ,A.[publisher_db]
                                ,A.[publication_id]
                                ,A.[subscriber_id]
                                ,A.[subscriber_db]
                                ,A.[subscription_type]
                                ,A.[sync_type]
                                ,A.[status]
                                ,A.[snapshot_seqno_flag]
                                ,A.[independent_agent]
                                ,A.[loopback_detection]
                                ,A.[agent_id]
                                ,A.[update_mode]
                                ,A.[nosync_type]
                                FROM [{0}].[distribution].[dbo].[MSsubscriptions] A,(select '['+publisher_db+']:'+publication as publisher
                                    ,[publication_id]
                                    from  [{1}].[distribution].[dbo].[MSpublications]) B
                                    where A.subscriber_id>0 and A.[publication_id]=B.[publication_id]
                                           and A.[publication_id]={2}
                                group by A.[publisher_database_id]
                                        ,A.[publisher_id]
                                        ,A.[publisher_db]
                                        ,A.[publication_id]
                                        ,A.[subscriber_id]
                                        ,A.[subscriber_db]
                                        ,A.[subscription_type]
                                        ,A.[sync_type]
                                        ,A.[status]
                                        ,A.[snapshot_seqno_flag]
                                        ,A.[independent_agent]
                                        ,A.[loopback_detection]
                                        ,A.[agent_id]
                                        ,A.[update_mode]
                                        ,A.[nosync_type],B.[publisher]
                                    order by [publisher_database_id],[publication_id] ";
                mySql = string.Format(mySql, mlinkServer, mlinkServer, mpublicattionId);
                table = dataFactory.Query(mySql);
            }
            return table;
        }
    }
}
