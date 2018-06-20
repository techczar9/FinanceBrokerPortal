using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;

namespace FinanceBrokerPortal
{
    public partial class FinanceBrokerPortal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string myobId = txtMyobId.Text;
                SQLData sqlData = new SQLData();

                if (sqlData.CheckPropertyExists("SELECT * FROM Property WHERE pro_myobid  = '" + myobId + "'"))
                {
                    DataSet dataSet = new DataSet();
                    DataTable dataTable = new DataTable();

                    dataTable = sqlData.GetSQLData("SELECT * FROM Property WHERE pro_myobid  = '" + myobId + "'");
                    DataTable fbuDataTable = new DataTable();
                    fbuDataTable = sqlData.GetSQLData("SELECT * FROM FinanceBrokerUpdate");

                    dataSet.Tables.Add(dataTable);
                    dataSet.Tables.Add(fbuDataTable);

                    SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT * FROM Property", sqlData.ConnectionString);
                    SqlCommandBuilder sqlCB = new SqlCommandBuilder(sqlDA);

                    SqlDataAdapter sql_FBU_DA = new SqlDataAdapter("SELECT * FROM FinanceBrokerUpdate", sqlData.ConnectionString);
                    SqlCommandBuilder sql_FBU_CB = new SqlCommandBuilder(sql_FBU_DA);

                    if (AllEmptyDatesCheck(txtLendRecAccByClient.Text, txtFinanceLodged.Text, txtFinanceAssessed.Text, txtConditionalApproval.Text, txtValuationOrdered.Text,
                        txtValuationReturned.Text, txtFinanceDeclined.Text, txtMortgageDocsReturned.Text, txtSettlementDate.Text))
                    {
                        var propertyRecord = dataSet.Tables[0].Rows[0];
                        var existingLenRecAccByClient = dataSet.Tables[0].Rows[0]["pro_lendingrecacceptbyclient"];
                        var existingFinanceLodged = dataSet.Tables[0].Rows[0]["pro_financelodged"];
                        var existingFinanceAssessed = dataSet.Tables[0].Rows[0]["pro_financeassessed"];
                        var existingConditionalApproval = dataSet.Tables[0].Rows[0]["pro_conditionalapproval"];
                        var existingValuationOrdered = dataSet.Tables[0].Rows[0]["pro_valuationordered"];
                        var existingValuationReturned = dataSet.Tables[0].Rows[0]["pro_valuationreturned"];
                        var existingFinanceDeclined = dataSet.Tables[0].Rows[0]["pro_financedeclined"];
                        var existingMortgageDocsReturned = dataSet.Tables[0].Rows[0]["pro_mortgagedocsreturned"];
                        var existingSettlementDate = dataSet.Tables[0].Rows[0]["pro_settlementdate"];

                        propertyRecord.BeginEdit();

                        propertyRecord["pro_lendingrecacceptbyclient"] =
                            (txtLendRecAccByClient.Text == string.Empty) ?
                            (String.IsNullOrEmpty(Convert.ToString(existingLenRecAccByClient)) ? DBNull.Value : (object)DateTime.Parse(existingLenRecAccByClient.ToString()))
                            : (object)DateTime.Parse(txtLendRecAccByClient.Text);

                        propertyRecord["pro_financelodged"] =
                            (txtFinanceLodged.Text == string.Empty) ?
                            (String.IsNullOrEmpty(Convert.ToString(existingFinanceLodged)) ? DBNull.Value : (object)DateTime.Parse(existingFinanceLodged.ToString()))
                            : (object)DateTime.Parse(txtFinanceLodged.Text);

                        propertyRecord["pro_financeassessed"] =
                            (txtFinanceAssessed.Text == string.Empty) ?
                            (String.IsNullOrEmpty(Convert.ToString(existingFinanceAssessed)) ? DBNull.Value : (object)DateTime.Parse(existingFinanceAssessed.ToString()))
                            : (object)DateTime.Parse(txtFinanceAssessed.Text);

                        propertyRecord["pro_conditionalapproval"] =
                            (txtConditionalApproval.Text == string.Empty) ?
                            (String.IsNullOrEmpty(Convert.ToString(existingConditionalApproval)) ? DBNull.Value : (object)DateTime.Parse(existingConditionalApproval.ToString()))
                            : (object)DateTime.Parse(txtConditionalApproval.Text);

                        propertyRecord["pro_valuationordered"] =
                            (txtValuationOrdered.Text == string.Empty) ?
                            (String.IsNullOrEmpty(Convert.ToString(existingValuationOrdered)) ? DBNull.Value : (object)DateTime.Parse(existingValuationOrdered.ToString()))
                            : (object)DateTime.Parse(txtValuationOrdered.Text);

                        propertyRecord["pro_valuationreturned"] =
                            (txtValuationReturned.Text == string.Empty) ?
                            (String.IsNullOrEmpty(Convert.ToString(existingValuationReturned)) ? DBNull.Value : (object)DateTime.Parse(existingValuationReturned.ToString()))
                            : (object)DateTime.Parse(txtValuationReturned.Text);

                        propertyRecord["pro_financedeclined"] =
                            (txtFinanceDeclined.Text == string.Empty) ?
                            (String.IsNullOrEmpty(Convert.ToString(existingFinanceDeclined)) ? DBNull.Value : (object)DateTime.Parse(existingFinanceDeclined.ToString()))
                            : (object)DateTime.Parse(txtFinanceDeclined.Text);

                        propertyRecord["pro_mortgagedocsreturned"] =
                            (txtMortgageDocsReturned.Text == string.Empty) ?
                            (String.IsNullOrEmpty(Convert.ToString(existingMortgageDocsReturned)) ? DBNull.Value : (object)DateTime.Parse(existingMortgageDocsReturned.ToString()))
                            : (object)DateTime.Parse(txtMortgageDocsReturned.Text);

                        propertyRecord["pro_settlementdate"] =
                            (txtSettlementDate.Text == string.Empty) ?
                            (String.IsNullOrEmpty(Convert.ToString(existingSettlementDate)) ? DBNull.Value : (object)DateTime.Parse(existingSettlementDate.ToString()))
                            : (object)DateTime.Parse(txtSettlementDate.Text);

                        propertyRecord.EndEdit();

                        int propertyId = int.Parse(propertyRecord["pro_propertyid"].ToString());

                        //Inserting Updates into the FinanceBrokerUpdate table
                        DataRow dataRow;
                        dataRow = dataSet.Tables[1].NewRow();
                        dataRow["fbu_propertyid"] = propertyId;
                        dataRow["fbu_lendingrecacceptbyclient"] = (txtLendRecAccByClient.Text == string.Empty) ? DBNull.Value : (object)DateTime.Parse(txtLendRecAccByClient.Text);
                        dataRow["fbu_financelodged"] = (txtFinanceLodged.Text == string.Empty) ? DBNull.Value : (object)DateTime.Parse(txtFinanceLodged.Text);
                        dataRow["fbu_financeassessed"] = (txtFinanceAssessed.Text == string.Empty) ? DBNull.Value : (object)DateTime.Parse(txtFinanceAssessed.Text);
                        dataRow["fbu_conditionalapproval"] = (txtConditionalApproval.Text == string.Empty) ? DBNull.Value : (object)DateTime.Parse(txtConditionalApproval.Text);
                        dataRow["fbu_valuationordered"] = (txtValuationOrdered.Text == string.Empty) ? DBNull.Value : (object)DateTime.Parse(txtValuationOrdered.Text);
                        dataRow["fbu_valuationreturned"] = (txtValuationReturned.Text == string.Empty) ? DBNull.Value : (object)DateTime.Parse(txtValuationReturned.Text);
                        dataRow["fbu_financedeclined"] = (txtFinanceDeclined.Text == string.Empty) ? DBNull.Value : (object)DateTime.Parse(txtFinanceDeclined.Text);
                        dataRow["fbu_mortgagedocsreturned"] = (txtMortgageDocsReturned.Text == string.Empty) ? DBNull.Value : (object)DateTime.Parse(txtMortgageDocsReturned.Text);
                        dataRow["fbu_settlementdate"] = (txtSettlementDate.Text == string.Empty) ? DBNull.Value : (object)DateTime.Parse(txtSettlementDate.Text);
                        dataSet.Tables[1].Rows.Add(dataRow);

                        sqlDA.Update(dataSet.Tables[0]);
                        sql_FBU_DA.Update(dataSet.Tables[1]);

                        lblResultMessage.Text = "Success : Finance date(s) for property updated successfully.";
                        lblResultMessage.CssClass = "alert alert-success";
                    }
                    else
                    {
                        lblResultMessage.Text = "Warning : Please enter atleast one property finance date.";
                        lblResultMessage.CssClass = "alert alert-warning";
                    }
                }
                else
                {
                    lblResultMessage.Text = "Matching MYOB ID could not be found; please check the MYOB ID and try again.";
                    lblResultMessage.CssClass = "alert alert-danger";
                }
            }
            catch (Exception ex)
            {
                lblResultMessage.Text = "Oops! Something went wrong. Please try again.";
                lblResultMessage.CssClass = "alert alert-danger";
            }
}

        private bool AllEmptyDatesCheck(string txtLendingRecAccByClient, string txtFinanceLodged, string txtFinanceAssessed, string txtConditionalApproval,
            string txtValuationOrdered, string txtValuationReturned, string txtFinanceDelcined, string txtMortgageDocsReturned, string txtSettlementDate)
        {
            if (txtLendingRecAccByClient != ""
                || txtFinanceLodged != ""
                || txtFinanceAssessed != ""
                || txtConditionalApproval != ""
                || txtValuationOrdered != ""
                || txtValuationReturned != ""
                || txtFinanceDelcined != ""
                || txtMortgageDocsReturned != ""
                || txtSettlementDate != "")
            {
                return true;
            }
            else return false;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtMyobId.Text = "";
            txtLendRecAccByClient.Text = "";
            txtFinanceLodged.Text = "";
            txtFinanceAssessed.Text = "";
            txtConditionalApproval.Text = "";
            txtValuationOrdered.Text = "";
            txtValuationReturned.Text = "";
            txtFinanceDeclined.Text = "";
            txtMortgageDocsReturned.Text = "";
            txtSettlementDate.Text = "";
            lblResultMessage.Text = "";
            lblResultMessage.CssClass = "";
        }
    }
}