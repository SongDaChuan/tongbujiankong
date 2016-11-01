<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServersManagement.aspx.cs" Inherits="ReplicationMonitor.Web.UI_ReplicationMonitor.ServersManagement" %>

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
    <script type="text/javascript" src="js/page/ServersManagement.js" charset="utf-8"></script>

</head>
<body>
 <div class="easyui-layout" data-options="fit:true,border:false">
       <div  data-options="region:'center',title:'',border:true, collapsible:true, split:false">
                        <table id="gridMain_ServersManagement"></table>       
           </div>
 </div>
</body>
</html>
