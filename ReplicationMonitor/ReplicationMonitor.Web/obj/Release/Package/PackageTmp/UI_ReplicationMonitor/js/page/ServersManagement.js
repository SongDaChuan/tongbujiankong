$(document).ready(function () {
    LoadserverName();
});
function LoadserverName() {

    $("#gridMain_ServersManagement").datagrid({
        columns: [[
           { field: 'name', title: '服务器', width: 100 },
           { field: 'server_name', title: '服务器名', width: 120 },
           { field: 'ip', title: 'IP', width: 140, align: 'left' },
               { field: 'log_in', title: '登陆名', width: 100 },
                   { field: 'password', title: '密码', width: 100 }
        ]],
        rownumbers: true,
        striped: true,
        fit: true
    });

    $.ajax({
        type: "post",
        url: "ServersManagement.aspx/GetServerDetailsTableJson",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            m_MsgData = jQuery.parseJSON(msg.d);
            $("#gridMain_ServersManagement").datagrid('loadData', m_MsgData);
        }
    });
}