<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServerTree.ascx.cs" Inherits="ReplicationMonitor.Web.UI_UserControls.ServersSelector.ServerTree" %>
<div class="easyui-layout" data-options="fit:true,border:false" >
    <div data-options="region:'north',border:true,collapsible:false" style="height: 50px; background-color:#FAFAFA; padding-top:10px;">
        <table>
            <tr>
                <td style="width: 90px; height: 30px;">选择服务器：</td>
                <td style="width: 120px; text-align: left;">
                    <input id="serverName" class="easyui-textbox" style="width: 90px;" readonly="readonly" />                                                                                                                
                </td>
            </tr>
        </table>
    </div>
    <div data-options="region:'center',border:true,collapsible:false">
        <ul id="ServerTree" class="easyui-tree"></ul>
    </div>
</div>
<%--<input id="HiddenField_PageName" style="width: 200px; visibility: hidden;" runat="server" />--%>