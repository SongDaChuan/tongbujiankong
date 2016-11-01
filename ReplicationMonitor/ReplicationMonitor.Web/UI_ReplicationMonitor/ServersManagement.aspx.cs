using ReplicationMonitor.Infrastructure.Configuration;
using SqlServerDataAdapter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReplicationMonitor.Web.UI_ReplicationMonitor
{
    public partial class ServersManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GetServerDetailsTableJson() 
        {
            DataTable table = GetServerDetailsTable();
            string json = EasyUIJsonParser.DataGridJsonParser.DataTableToJson(table);
            return json;            
        }
        private static DataTable GetServerDetailsTable() 
        {
            string connectionString = ConnectionStringFactory.NXJCConnectionString;
            ISqlServerDataFactory dataFactory = new SqlServerDataFactory(connectionString);
            string mySql=@"SELECT * FROM [IndustryEnergy_SH].[dbo].[linkServer] order by displayIndex ";
            DataTable table=dataFactory.Query(mySql);
            return table;
        }
    }
}