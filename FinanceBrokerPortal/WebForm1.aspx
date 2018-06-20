<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="FinanceBrokerPortal.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" type="text/css"/>
    <link href="Content/CustomStyle.css" rel="stylesheet" type="text/css"/>
</head>
<body>
    <div id="wrapper">
        <div id="header">
            OpenCorp Finance Broker Portal
        </div>
        <div class="panel panel-danger">
            <div class="panel-heading">Important</div>
            <div class="panel-body">
                <ul>
                    <li>Please keep the MYOB ID ready for property finance update</li>
                    <li>For technical issues, please contact Shadab Khan at shadab@opencorp.com.au</li>
                </ul>
            </div>
        </div>
        <div class="container center_div">
            <form method="post" runat="server">
                <div class="row">               
                            <fieldset>
                                <legend>Finance Update Form</legend>
                                <div class="form-group">                        
                                        <label>MYOB ID:</label>
                                        <asp:TextBox ID="txtMyobId" runat="server" CssClass="form-control"></asp:TextBox>
                                 </div>
                            </fieldset>
                </div>
            </form>
        </div>
    </div>
   <%-- <form id="form1" runat="server">
        <div>
        </div>
    </form>--%>
</body>
</html>
