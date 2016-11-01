using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ReplicationMonitor.Infrastructure.Configuration;
using SqlServerDataAdapter;
using System.Data.SqlClient;

namespace ReplicationMonitor.Service
{
    public class SeversTree
    {
        public static DataTable GetServersDataTable()
        {
            string connectionString = ConnectionStringFactory.NXJCConnectionString;
            ISqlServerDataFactory dataFactory = new SqlServerDataFactory(connectionString);
            string mySql = @"select displayIndex as id ,name as [text],link_name as linkName from [IndustryEnergy_SH].[dbo].[linkServer]  where name !=''";
            DataTable serverTable = dataFactory.Query(mySql);
            return serverTable;  
        }
        //private static DataTable GetServerStructure()
        //{
        //    //DataTable m_ServerTreeItemsTable = new DataTable();
        //    //m_ServerTreeItemsTable.Columns.Add("ServerFacatoryName",typeof(string));
        //    //m_ServerTreeItemsTable.Columns.Add("LevelCode", typeof(string));
        //    //m_ServerTreeItemsTable.Columns.Add("ServerName", typeof(string));
        //    //m_ServerTreeItemsTable.Columns.Add("IP", typeof(string));
        //    //m_ServerTreeItemsTable.Columns.Add("iconCls", typeof(string));
        //    //return m_ServerTreeItemsTable;
        //}    
    }

}
