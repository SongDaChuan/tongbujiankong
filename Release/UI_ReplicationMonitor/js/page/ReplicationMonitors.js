$(document).ready(function () {
    LoadAllMonitorTreeNodes();

    //初始化dialog和 articleTable
    InitialDisplayGrid("first");
    InitialArticleTable();
});
function InitialDisplayGrid(type,mData) {
    if (type === "first") {
        $(".queryAgentInfo").hide();
        $('#gridMain_Display').datagrid({
            columns: [[
                //0 = Transactional 
                // 1 = Snapshot
                // 2 = Merge
             // { field: 'publisher_database_id', title: '发布数据库ID', width: 100 },
              { field: 'publisher', title: '发布', width: 240 },
              { field: 'publication_id', title: '发布ID', width: 40, align: 'left' },
              {
                  field: 'publication_type', title: '发布类型', width: 80, align: 'left', formatter: function (value, row, index) {
                      if (value == 0) { return value = "事物" } else if (value == 1) { return value = "快照" } else if (value == 3) { return value = "合并" }
                  }
              },
              { field: 'description', title: '描述', width: 340, align: 'left' }
            ]],
            rownumbers: true,
            striped: true,
            fit: true,
            fitColumns: true
        });
    } else if (type=="last") {
        $('#gridMain_Display').datagrid('loadData', m_MsgData);    
    }  
}
function InitialArticleTable() {
    $('#tb_Articles').datagrid({
        columns: [[
            { field: 'article', title: '文章', width: 100 },
            { field: 'publisher_db', title: '发布数据库', width: 100 },         
            { field: 'article_id', title: 'ID', width: 40, align: 'left' }
        ]],
        rownumbers: true,
        striped: true,
        fit:true
    });
    //初始化combobox
    $('#combo_AgentClass').combobox({
        valueField: 'value',
        textField: 'label',
        data: [{
            value: 'LogReaderAgent',
            label: '日志读取器代理'
        }, {
            value: 'SnapShotAgent',
            label: '快照代理'
        }],
        onSelect: function (node) {
            var mValue = node.value;
           
            if(mValue=="LogReaderAgent"){
                LoadLogReaderAgentStatus(mValue);
            } else if (mValue == "SnapShotAgent") {
                LoadSnapShotAgentStatus(mValue);
            }
        },
        panelHeight:true
    });
}
function LoadAllMonitorTreeNodes()
{
    $.ajax({
        type: "post",
        url: "ReplicationMonitors.aspx/ServerDataTable",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            m_MsgData = jQuery.parseJSON(msg.d);
            loadContentText(m_MsgData);               
        }
    });
}
var m_linkServer = "";
var m_publicationId = "";
function loadContentText(mData) {
    $("#RepMonitorNodes").tree({
        data: mData,
        animate: true,
        lines: true,
        onDblClick: function (node) {
            m_linkServer = node.linkServer;
            if (node.id.length <= 5) {
                m_publicationId = "";
            } else {
                m_publicationId = node.publicationId;
            }
            $("#serverName").textbox('setText', node.serverName);
            loadPublisherMonitorListDataGrid();           
        },
        onContextMenu: function (e, node) {
            e.preventDefault();
            e.stopPropagation();//阻止冒泡
            // select the node
            $('#RepMonitorNodes').tree('select', node.target);
            // display context menu
            ///
            var item = $('#mm').menu('findItem', '查看项目')
            if (item!=null){
                $('#mm').menu('removeItem', item.target);
            }                          
            $('#mm').menu('appendItem', {
                text: '查看项目',
                // iconCls: 'icon-ok',
                onclick: function () {                  
                    var nodes = node;
                    if (node.publicationId == "") {
                        $.messager.alert('提示','请选择发布节点！');
                    } else {
                        $('#dlg_Articles').dialog('open');   //打开对话框dialog 

                        LoadArticles(node.linkServer, node.publicationId);
                    }                 
                }
            });
            $('#mm').menu('show', {
                left: e.pageX,
                top: e.pageY
            });
        }
    });
    $("#RepMonitorNodes").tree('collapseAll');
}
function LoadArticles(mlinkServer, mpublicattionId) {
  
    $.ajax({
        type: "post",
        url: "ReplicationMonitors.aspx/GetArticlesJson",
        data: '{mlinkServer:"' + mlinkServer + '",mpublicattionId:"' + mpublicattionId + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            m_MsgData = jQuery.parseJSON(msg.d);
            $('#tb_Articles').datagrid('loadData', m_MsgData);
        }
    });
}
function loadPublisherMonitorListDataGrid() {   
    $(".queryAgentInfo").hide();
    InitialDisplayGrid("first");
    $.ajax({
        type: "post",
        url: "ReplicationMonitors.aspx/GetPublisherMonitorListDataGridJson",
        data: '{mlinkServer:"' + m_linkServer + '",mpublicattionId:"' + m_publicationId + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            m_MsgData = jQuery.parseJSON(msg.d);
            InitialDisplayGrid("last", m_MsgData);
        }
    });
}
function loadSubscribeMonitorListDataGrid() {
    $(".queryAgentInfo").hide();
    $('#gridMain_Display').datagrid({
        columns: [[
          { field: 'publisher', title: '发布', width: 240 },
          { field: 'publication_id', title: '发布ID', width: 40, align: 'left' },
          { field: 'subscriber_db', title: '订阅数据库', width: 120, align: 'left' },
          {
              field: 'subscription_type', title: '订阅类型', width: 80, align: 'left', formatter: function (value, row, index) {
                  if (value == 0) { return value = "推送" } else if (value == 1) { return value = "请求" } else if (value == 2) { return value = "匿名" }
              }
          },
          {
              field: 'status', title: '状态', width: 60, align: 'left', formatter: function (value, row, index) {
                  if (value == 0) { return value = "不活动" } else if (value == 1) { return value = "已订阅" } else if (value == 2) { return value = "活动" }
              }
          },
        ]],
        rownumbers: true,
        striped: true,
        fit: true,
        fitColumns: true
    });
    $.ajax({
        type: "post",
        url: "ReplicationMonitors.aspx/GetSubscribeMonitorListDataGridJson",
        data: '{mlinkServer:"' + m_linkServer + '",mpublicattionId:"' + m_publicationId + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            m_MsgData = jQuery.parseJSON(msg.d);
            $('#gridMain_Display').datagrid('loadData', m_MsgData);
        }
    });
}

function loadAgentMonitorListDataGrid() {
    $(".queryAgentInfo").show();
    $('#combo_AgentClass').combobox('setText', "日志读取器代理");
  //  LoadLogReaderAgentStatus("first");
    LoadLogReaderAgentStatus("LogReaderAgent");
}

function LoadLogReaderAgentStatus(myValue) {

      //  urlString = "ReplicationMonitors.aspx/GetSnapShotAgentJson";



    $.ajax({
        type: "post",
        url: "ReplicationMonitors.aspx/GetLogReaderAgentJson",
        data: '{mlinkServer:"' + m_linkServer + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            m_MsgData = jQuery.parseJSON(msg.d);
            LoadLogReaderAgentData("first");
            LoadLogReaderAgentData("last",m_MsgData);           
        }
    });
}
function LoadLogReaderAgentData(type, myData) {
    if(type=="first"){
        $('#gridMain_Display').datagrid({
            columns: [[
              { field: 'publisher_db', title: '发布数据库', width: 140 },
              { field: 'run_requested_date', title: '上次启动时间', width: 140, align: 'left' },
              { field: 'stop_execution_date', title: '停止执行时间', width: 140, align: 'left' },
              { field: 'run_time', title: '运行时间', width: 140, align: 'left' }
        ]],
        rownumbers: true,
        striped: true,
        fit: true
        });    
    }
    else if (type=="last") {
        $('#gridMain_Display').datagrid('loadData', myData);
    }
}

function LoadSnapShotAgentStatus(myValue) {
    $.ajax({
        type: "post",
        url: "ReplicationMonitors.aspx/GetSnapShotAgentJson",
        data: '{mlinkServer:"' + m_linkServer + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            m_MsgData = jQuery.parseJSON(msg.d);
            LoadSnapShotAgentData("first");
            LoadSnapShotAgentData("last", m_MsgData);
        }
    });
}
function LoadSnapShotAgentData(type, myData) {
    if (type == "first") {
        $('#gridMain_Display').datagrid({
            columns: [[
              { field: 'publisher', title: '发布数据库', width: 140 },
              { field: 'run_requested_date', title: '运行请求时间', width: 140, align: 'left' },
               { field: 'start_execution_date', title: '开始执行时间', width: 140 },
              { field: 'last_executed_step_date', title: '最后执行时间', width: 140, align: 'left' },
              {
                  field: 'publication_type', title: '发布类型', width: 140, align: 'left', formatter: function (value,row,index) {
                      if (value == 0) { return value = "事物" } else if (value == 1) { return value = "快照" } else if(value==2){return value="合并"}
                  }
              }
            ]],
            rownumbers: true,
            striped: true,
            fit: true
        });
    }
    else if (type == "last") {
        $('#gridMain_Display').datagrid('loadData', myData);
    }
}

