﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinanceBrokerPortal.aspx.cs" Inherits="FinanceBrokerPortal.FinanceBrokerPortal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" type="text/css"/>
    <link href="Content/CustomStyle.css" rel="stylesheet" type="text/css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="Scripts/moment.js"></script>
    <script src="Scripts/bootstrap-datetimepicker.js"></script>
    <script>
        $(document).ready(function () {
            
            $("#txtMyobId").blur(function () {
                var myobId = $(this).val();
                $.ajax({
                    type: "POST",
                    url: 'Ajax.aspx/DataTableToJson',
                    data: '{ "myobId" : "' + myobId + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: onSuccess,
                    crossdomain: true,
                    failure: function (response) { alert("Something went wrong this time! Please try again in sometime."); }
                    });
                function onSuccess(response) {
                    if (($.parseJSON(response.d)).length == 0) {
                        alert('Client ID not found; please try again.');
                    }
                    else {
                        var data = response.d;
                        data = $.parseJSON(data);
                        console.log(data.length);
                        $('#txtLendRecAccByClient').val((data[0].pro_lendingrecacceptbyclient).substring(0, 10));
                        $('#txtFinanceLodged').val((data[0].pro_financelodged).substring(0, 10));
                        $('#txtFinanceAssessed').val((data[0].pro_financeassessed).substring(0, 10));
                        $('#txtConditionalApproval').val((data[0].pro_conditionalapproval).substring(0, 10));
                        $('#txtValuationOrdered').val((data[0].pro_valuationordered).substring(0, 10));
                        $('#txtValuationReturned').val((data[0].pro_valuationreturned).substring(0, 10));
                        $('#txtFinanceDeclined').val((data[0].pro_financedeclined).substring(0, 10));
                        $('#txtMortgageDocsReturned').val((data[0].pro_mortgagedocsreturned).substring(0, 10));
                    }
                }
            });
        });
    </script>
</head>
<body>
    <div id="container">
        <div style="margin-bottom: 10px;">

        </div>
        <div id="header" class="row center_div">
            OpenCorp.<sup>&reg;</sup> Finance Broker Portal
        </div>
        <div style="margin-bottom: 10px;">

        </div>
        <div class="row center_div panel panel-danger">
            <div class="panel-heading"><span class="headingFont">Important</span></div>
            <div class="panel-body">
                <ul>
                    <li>Please keep the <b>CLIENT ID [Format : ABC1234 ]</b> ready for finance updates.</li>
                    <li>Dates to be <strong>NOT</strong> entered manually; please use the calendar icon <span class="glyphicon glyphicon-calendar"></span> to select an appropriate date.</li>
                    <li>For technical issues, contact Shadab Khan at <b>shadab@opencorp.com.au</b></li>
                </ul>
            </div>
        </div>
        <br />
        <div class="row center_div">
            <form runat="server">              
                <fieldset>
                    <legend>Finance Update Form</legend>
                    <asp:Label ID="lblResultMessage" runat="server" Text=""></asp:Label>
                    <div class="form-group" style="margin-top: 20px;">                        
                            <label>Client ID:</label>
                            <asp:TextBox ID="txtMyobId" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                    <div class="form-group">                        
                            <label>Lending Recommendation Accepted By Client:</label>            
                            <div class='input-group date' id='landRecAccByClient'>
                                <asp:TextBox ID="txtLendRecAccByClient" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                    </div>
                    <div class="form-group">                        
                            <label>Finance Lodged</label>            
                            <div class='input-group date' id='financeLodged'>
                                <asp:TextBox ID="txtFinanceLodged" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                    </div>
                    <div class="form-group">                        
                            <label>Finance Assessed</label>            
                            <div class='input-group date' id='financeAssessed'>
                                <asp:TextBox ID="txtFinanceAssessed" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                    </div>
                    <div class="form-group">                        
                            <label>Conditional Approval</label>           
                            <div class='input-group date' id='conditionalApproval'>
                                <asp:TextBox ID="txtConditionalApproval" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                    </div>
                    <div class="form-group">                        
                            <label>Valuation Ordered</label>            
                            <div class='input-group date' id='valuationOrdered'>
                                <asp:TextBox ID="txtValuationOrdered" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                    </div>
                    <div class="form-group">                        
                            <label>Valuation Returned</label>            
                            <div class='input-group date' id='valuationReturned'>
                                <asp:TextBox ID="txtValuationReturned" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                    </div>
                    <%--<div class="form-group">                        
                            <label>Formal Approval</label>            
                            <div class='input-group date' id='formalApproval'>
                                <asp:TextBox ID="txtFormalApproval" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                    </div>--%>
                    <div class="form-group">                        
                            <label>Finance Declined</label>            
                            <div class='input-group date' id='financeDeclined'>
                                <asp:TextBox ID="txtFinanceDeclined" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                    <div class="form-group">                        
                            <label>Mortgage Docs Returned</label>            
                            <div class='input-group date' id='mortgageDocumentsReturned'>
                                <asp:TextBox ID="txtMortgageDocsReturned" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                    </div>
                    <%--<div class="form-group">                        
                            <label>Settlement Date</label>
                            <div class='input-group date' id='settlementDate'>
                                <asp:TextBox ID="txtSettlementDate" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                    </div>--%>
                    <div class="form-group">
                        <label>Notes</label>
                        <asp:TextBox ID="txtFinanceNotes" runat="server" CssClass="form-control" TextMode="MultiLine" Height="100px"></asp:TextBox>
                    </div>
                    <div class="form-group">                        
                            <asp:Button ID="btnSubmit" runat="server" Text="Save Changes" CssClass="btn btn-success" OnClick="btnSubmit_Click" />
                            <span style="margin-right: 20px;"></span>
                            <asp:Button ID="btnClear" runat="server" Text="Clear Form" CssClass="btn btn-warning" OnClick="btnClear_Click" />
                    </div>
                </fieldset>                    
            </form>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $('#landRecAccByClient').datetimepicker({ format: 'YYYY-MM-DD' });
            $('#financeLodged').datetimepicker({ format: 'YYYY-MM-DD' });
            $('#financeAssessed').datetimepicker({ format: 'YYYY-MM-DD' });
            $('#conditionalApproval').datetimepicker({ format: 'YYYY-MM-DD' });
            $('#valuationOrdered').datetimepicker({ format: 'YYYY-MM-DD' });
            $('#valuationReturned').datetimepicker({ format: 'YYYY-MM-DD' });
            //$('#formalApproval').datetimepicker({ format: 'YYYY-MM-DD'});
            $('#financeDeclined').datetimepicker({ format: 'YYYY-MM-DD' });
            $('#mortgageDocumentsReturned').datetimepicker({ format: 'YYYY-MM-DD' });
            //$('#settlementDate').datetimepicker({ format: 'YYYY-MM-DD' });
        });
    </script>
</body>
</html>
