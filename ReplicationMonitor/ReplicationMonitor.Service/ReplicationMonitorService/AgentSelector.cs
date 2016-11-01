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
    public class AgentSelector
    {
      
        public static DataTable GetLogreaderAgentTable(string serverName)
        {
            string connectionString = ConnectionStringFactory.NXJCConnectionString;
            ISqlServerDataFactory dataFactory = new SqlServerDataFactory(connectionString);
            string mySql = @"select A.[id]
                              ,A.[name]
                              ,A.[publisher_db]
                              ,A.[publication]
                              ,A.[profile_id]
                              ,B.[session_id]
                              ,B.[run_requested_date]
                              ,B.[run_requested_source]
                              ,B.[last_executed_step_date]
                              ,B.[stop_execution_date]
	                          ,datediff(s,B.[last_executed_step_date],GETDATE()) as secondrunTime
	                          ,convert(varchar,DATEDIFF(MINUTE,B.[last_executed_step_date],GETDATE())/60/24)+'天'+convert(varchar,DATEDIFF(Minute,B.[last_executed_step_date],GETDATE())/60-DATEDIFF(MINUTE,B.[last_executed_step_date],GETDATE())/60/24*24)+'时'+convert(varchar,DATEDIFF(minute,B.[last_executed_step_date],GETDATE())-DATEDIFF(minute,B.[last_executed_step_date],GETDATE())/60/24*24*60-(DATEDIFF(Minute,B.[last_executed_step_date],GETDATE())/60-DATEDIFF(MINUTE,B.[last_executed_step_date],GETDATE())/60/24*24)*60)+'分' as run_time
	                         from [{0}].[distribution].[dbo].[MSlogreader_agents] A,[{1}].[msdb].[dbo].[sysjobactivity] B
                             where A.job_id=B.job_id
                             order by id,B.[last_executed_step_date] desc";
            mySql = string.Format(mySql,serverName,serverName);
            DataTable originalTable = dataFactory.Query(mySql);
            return originalTable;
        }
        public static DataTable GetSnapShotAgentTable(string serverName)
        {
            string connectionString = ConnectionStringFactory.NXJCConnectionString;
            ISqlServerDataFactory dataFactory = new SqlServerDataFactory(connectionString);
            string mySql = @"select * from 
                        (select *, ROW_NUMBER() OVER ( PARTITION BY publisher ORDER BY run_requested_date DESC ) rid
                            FROM(select A.id,'['+A.publisher_db+']:'+A.publication as publisher,B.run_requested_date,B.start_execution_date,
			                B.last_executed_step_date,B.next_scheduled_run_date,A.[publication_type]
			                from [{0}].[distribution].[dbo].[MSsnapshot_agents] A,[{1}].[msdb].[dbo].[sysjobactivity] B
			                where A.job_id=B.job_id)as T1
                         ) as T2
                         where rid=1
                        order by run_requested_date desc";
            mySql = string.Format(mySql, serverName, serverName);
            DataTable originalTable = dataFactory.Query(mySql);
            return originalTable;
        }
    }
}
