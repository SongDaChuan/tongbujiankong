using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReplicationMonitor01.UI_ReplicationMonitor
{
    public partial class ReplicationMonitors : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string ServerDataTable()
        {
            DataTable table = ReplicationMonitor.Service.ReplicationMonitorService.MonitorNodes.GetMonitorNodesTable();
            string[] paras = { "text", "iconCls", "linkServer", "serverName", "publicationId" };
            string json = EasyUIJsonParser.TreeGridJsonParser.DataTableToJsonByLevelCode(table, "levelCode", paras);
            return json;
        }
        [WebMethod]
        public static string GetArticlesJson(string mlinkServer, string mpublicattionId)
        {
            DataTable table = ReplicationMonitor.Service.ReplicationMonitorService.MonitorNodes.GetArticlesTable(mlinkServer, mpublicattionId);
            string[] param = { "article", "publisher_db", "article_id" };
            string json = EasyUIJsonParser.DataGridJsonParser.DataTableToJson(table, param);
            return json;
        }
        [WebMethod]
        public static string GetPublisherMonitorListDataGridJson(string mlinkServer, string mpublicattionId) 
        {
            DataTable table = ReplicationMonitor.Service.ReplicationMonitorService.PublicationList.GetPublisherMonitorListTable(mlinkServer, mpublicattionId);          
            string json = EasyUIJsonParser.DataGridJsonParser.DataTableToJson(table);
            return json;
        }
        [WebMethod]
        public static string GetSubscribeMonitorListDataGridJson(string mlinkServer, string mpublicattionId) 
        {
            DataTable table = ReplicationMonitor.Service.ReplicationMonitorService.SubscribeList.GetSubscribeMonitorListTable(mlinkServer, mpublicattionId);
            string json = EasyUIJsonParser.DataGridJsonParser.DataTableToJson(table);
            return json;    
        }
        ///获取代理
        [WebMethod]
        public static string GetLogReaderAgentJson(string mlinkServer) 
        {
            DataTable table = ReplicationMonitor.Service.ReplicationMonitorService.AgentSelector.GetLogreaderAgentTable(mlinkServer);
            string json = EasyUIJsonParser.DataGridJsonParser.DataTableToJson(table);
            return json;       
        }
        [WebMethod]
        public static string GetSnapShotAgentJson(string mlinkServer)
        {
            DataTable table = ReplicationMonitor.Service.ReplicationMonitorService.AgentSelector.GetSnapShotAgentTable(mlinkServer);
            string json = EasyUIJsonParser.DataGridJsonParser.DataTableToJson(table);
            return json;          
        }




        //[WebMethod]
        //public static string GetPublicationList()
        //{
        //    DataTable table = ReplicationMonitor.Service.ServerSelector.MonitorList.GetPublicationTable("WIN-G75RSO0NS01");
        //    string json = EasyUIJsonParser.DataGridJsonParser.DataTableToJson(table);
        //    return json;
        //}
        //[WebMethod]
        //public static string GetSubscribeList()
        //{
        //    DataTable table = ReplicationMonitor.Service.ServerSelector.MonitorList.GetSubscribeTable("WIN-G75RSO0NS01");
        //    string json = EasyUIJsonParser.DataGridJsonParser.DataTableToJson(table);
        //    return json;
        //}
        //[WebMethod]
        //public static string GetSnapshotAgentList()
        //{
        //    DataTable table = ReplicationMonitor.Service.ServerSelector.AgentSelector.GetSnapshotAgentTable("WIN-G75RSO0NS01");
        //    string json = EasyUIJsonParser.DataGridJsonParser.DataTableToJson(table);
        //    return json;
        //}
        //[WebMethod]
        //public static string GetLogreaderAgentList()
        //{
        //    DataTable table = ReplicationMonitor.Service.ServerSelector.AgentSelector.GetLogreaderAgentTable("WIN-G75RSO0NS01");
        //    string json = EasyUIJsonParser.DataGridJsonParser.DataTableToJson(table);
        //    return json;
        //}
    }
}