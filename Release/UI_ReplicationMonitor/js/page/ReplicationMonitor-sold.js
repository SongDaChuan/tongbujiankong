$(document).ready(function () {
  //  LoadDatagridTree("first");
    LoadTreeData();
});
function LoadDatagridTree(type, myData)
{
    if(type=="first"){
    
      $("#RepMonitorNodes").tree({
        animate: true,
        idField: 'displayIndex',
        treeField:'name',
        lines: true,
        collapsible: false,
        fitColumns: true,
        singleSelect: true,
        //striped: true,
        //data: [],
        onDblClick: function (node) {
            var levelcode = node.id;

            if (levelcode.length == 5) {

                LoadArticle(node.ServerName);
            }
        }
        });
    } else if (type == "last") {
        $("#RepMonitorNodes").tree('loadData',myData);
    }
}
var mlinkName = "";
function LoadTreeData()
{
    $.ajax({
        type: "post",
        url: "ReplicationMonitors.aspx/ServerDataTable",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            //if (msg.d == "[]") {
            //    alert("没有查询的数据");
            //}
            //else {
                m_MsgData = jQuery.parseJSON(msg.d);
            $("#RepMonitorNodes").tree({
                data: m_MsgData.rows,
                animate: true,
                lines: true,
                onDblClick: function (node) {
                    $('#serverName').textbox('setText', node.text);
                    mlinkName = node.linkName;
                    LoadArticle(mlinkName);
                }
            });
          }
    });
}
function LoadArticle(LinkServerName) {
    $.ajax({
        type: "post",
        url: "ReplicationMonitors.aspx/GetReplicationMonitor",
        data: '{serverName:"' + LinkServerName + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            m_MsgData = jQuery.parseJSON(msg.d);
            $("#RepMonitorArticles").tree({
                        data: m_MsgData,
                        animate: true,
                        lines: true,
                        onDblClick: function (node) {
                        }
            });
            $('#RepMonitorArticles').tree('collapseAll');
        }
    });

}