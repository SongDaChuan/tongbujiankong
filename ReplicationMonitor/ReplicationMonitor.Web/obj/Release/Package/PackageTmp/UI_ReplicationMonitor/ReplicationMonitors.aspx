<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReplicationMonitors.aspx.cs" Inherits="ReplicationMonitor01.UI_ReplicationMonitor.ReplicationMonitors" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" type="text/css" href="/lib/ealib/themes/gray/easyui.css" />
    <link rel="stylesheet" type="text/css" href="/lib/ealib/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="/lib/extlib/themes/syExtIcon.css" />

    <script type="text/javascript" src="/lib/ealib/jquery.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="/lib/ealib/jquery.easyui.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="/lib/ealib/easyui-lang-zh_CN.js" charset="utf-8"></script>

      <!--[if lt IE 8 ]><script type="text/javascript" src="/js/common/json2.min.js"></script><![endif]-->
   
     <script  type="text/javascript" src="js/page/ReplicationMonitors.js" charset="utf-8"></script>
</head>
<body>
     <div class="easyui-layout" data-options="fit:true,border:false">
            <div data-options="region:'west',split:true"title="复制监视器"style="width: 240px;">             
                <div class="easyui-layout" data-options="fit:true,border:false" >
                    <div data-options="region:'north',border:true,collapsible:false" style="height: 50px; background-color:#FAFAFA; padding-top:10px;">
                        <table>
                            <tr>
                                <td style="width: 100px; height: 30px;">选择服务器：</td>
                                <td style="width: 120px; text-align: left;">
                                    <input id="serverName" class="easyui-textbox" style="width: 90px;" readonly="readonly" />                                                                                                                
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div data-options="region:'center',border:true,collapsible:false">
                        <ul id="RepMonitorNodes" ></ul> 
                    </div>
                </div>
            </div>

            <div  data-options="region:'center',title:'',border:true, collapsible:true, split:false">
                <!--服务器点击触发-->
                 <div class="easyui-layout" data-options="fit:true,border:false" >
                    <div data-options="region:'north',border:false,collapsible:false" style="height:45px;padding:10px;">
                            <table>
                            <tr>
                                <td >
                                    <%--<input id="Button1" type="button" value="发布监视列表" onclick=""/>--%>

                                    <a href="javascript:void(0);" class="easyui-linkbutton" data-options="plain:true" onclick="loadPublisherMonitorListDataGrid()">发布监视列表</a>
                                </td>
                                <td>
                                    <a href="javascript:void(0);" class="easyui-linkbutton" data-options="plain:true" onclick="loadSubscribeMonitorListDataGrid()">订阅监视列表</a>

                                </td>
                                <td >
                                    <a href="javascript:void(0);" class="easyui-linkbutton" data-options="plain:true" onclick="loadAgentMonitorListDataGrid()">代理列表</a>
                                </td>                              
                              </tr>
                          </table>   
                                                                                      
                    </div> 
                      <div data-options="region:'center',border:false,collapsible:false">
                            <table class="queryAgentInfo">
                             <tr class="queryAgentInfo">
                                <td class="queryAgentInfo" style="width:60px"> 代理类型：</td>
                                <td class="queryAgentInfo">
                                    <input id="combo_AgentClass" class="easyui-combobox" style="width:150px"  data-options="panelHeight:true"/> 
                                </td>
                             </tr>
                          </table>    
                         <table id="gridMain_Display"></table>                          
                      </div>                                                                                                                       
                   </div>         
                </div>             
                    <%--右键菜单--%>
                    <div id="mm" class="easyui-menu" style="width:120px;" ></div> 
                    <%--dialog显示--%>
                <div id="dlg_Articles" class="easyui-dialog" title="发布项目" style="width:340px;height:320px;" 
                               data-options="resizable:true,modal:true,cache:false,closed:true"> 
                         <div class="easyui-layout" data-options="fit:true,border:false">
                                       <table id="tb_Articles"></table>
                         </div>
                </div>
            </div>
</body>
</html>
