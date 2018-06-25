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
using System.Globalization;

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

                    var propertyRecord = dataSet.Tables[0].Rows[0];
                    var existingLenRecAccByClient = dataSet.Tables[0].Rows[0]["pro_lendingrecacceptbyclient"].ToString();
                    var existingFinanceLodged = dataSet.Tables[0].Rows[0]["pro_financelodged"].ToString();
                    var existingFinanceAssessed = dataSet.Tables[0].Rows[0]["pro_financeassessed"].ToString();
                    var existingConditionalApproval = dataSet.Tables[0].Rows[0]["pro_conditionalapproval"].ToString();
                    var existingValuationOrdered = dataSet.Tables[0].Rows[0]["pro_valuationordered"].ToString();
                    var existingValuationReturned = dataSet.Tables[0].Rows[0]["pro_valuationreturned"].ToString();
                    var existingFormalApproval = dataSet.Tables[0].Rows[0]["pro_financeapproveddate"].ToString();
                    var existingFinanceDeclined = dataSet.Tables[0].Rows[0]["pro_financedeclined"].ToString();
                    var existingMortgageDocsReturned = dataSet.Tables[0].Rows[0]["pro_mortgagedocsreturned"].ToString();
                    var existingSettlementDate = dataSet.Tables[0].Rows[0]["pro_settlementdate"].ToString();
                    var existingFinanceNotes = dataSet.Tables[0].Rows[0]["pro_financenotes"].ToString();

                    propertyRecord.BeginEdit();

                    propertyRecord["pro_lendingrecacceptbyclient"] =
                        (txtLendRecAccByClient.Text == string.Empty) ?
                        (String.IsNullOrEmpty(existingLenRecAccByClient) ? (object)DBNull.Value : (object)DateTime.Parse(existingLenRecAccByClient))
                        : (object)DateTime.Parse(txtLendRecAccByClient.Text);

                    propertyRecord["pro_financelodged"] =
                        (txtFinanceLodged.Text == string.Empty) ?
                        (String.IsNullOrEmpty(existingFinanceLodged) ? (object)DBNull.Value : (object)DateTime.Parse(existingFinanceLodged))
                        : (object)DateTime.Parse(txtFinanceLodged.Text);

                    propertyRecord["pro_financeassessed"] =
                        (txtFinanceAssessed.Text == string.Empty) ?
                        (String.IsNullOrEmpty(existingFinanceAssessed) ? (object)DBNull.Value : (object)DateTime.Parse(existingFinanceAssessed))
                        : (object)DateTime.Parse(txtFinanceAssessed.Text);

                    propertyRecord["pro_conditionalapproval"] =
                        (txtConditionalApproval.Text == string.Empty) ?
                        (String.IsNullOrEmpty(existingConditionalApproval) ? (object)DBNull.Value : (object)DateTime.Parse(existingConditionalApproval))
                        : (object)DateTime.Parse(txtConditionalApproval.Text);

                    propertyRecord["pro_valuationordered"] =
                        (txtValuationOrdered.Text == string.Empty) ?
                        (String.IsNullOrEmpty(existingValuationOrdered) ? (object)DBNull.Value : (object)DateTime.Parse(existingValuationOrdered))
                        : (object)DateTime.Parse(txtValuationOrdered.Text);

                    propertyRecord["pro_valuationreturned"] =
                        (txtValuationReturned.Text == string.Empty) ?
                        (String.IsNullOrEmpty(existingValuationReturned) ? (object)DBNull.Value : (object)DateTime.Parse(existingValuationReturned))
                        : (object)DateTime.Parse(txtValuationReturned.Text);

                    propertyRecord["pro_financeapproveddate"] =
                        (txtFormalApproval.Text == string.Empty) ?
                        (String.IsNullOrEmpty(existingFormalApproval) ? (object)DBNull.Value : (object)DateTime.Parse(existingFormalApproval))
                        : (object)DateTime.Parse(txtFormalApproval.Text);

                    propertyRecord["pro_financedeclined"] =
                        (txtFinanceDeclined.Text == string.Empty) ?
                        (String.IsNullOrEmpty(existingFinanceDeclined) ? (object)DBNull.Value : (object)DateTime.Parse(existingFinanceDeclined))
                        : (object)DateTime.Parse(txtFinanceDeclined.Text);

                    propertyRecord["pro_mortgagedocsreturned"] =
                        (txtMortgageDocsReturned.Text == string.Empty) ?
                        (String.IsNullOrEmpty(existingMortgageDocsReturned) ? (object)DBNull.Value : (object)DateTime.Parse(existingMortgageDocsReturned))
                        : (object)DateTime.Parse(txtMortgageDocsReturned.Text);

                    propertyRecord["pro_settlementdate"] =
                        (txtSettlementDate.Text == string.Empty) ?
                        (String.IsNullOrEmpty(existingSettlementDate) ? (object)DBNull.Value : (object)DateTime.Parse(existingSettlementDate))
                        : (object)DateTime.Parse(txtSettlementDate.Text);

                    propertyRecord["pro_financenotes"] =
                        (string.IsNullOrEmpty(existingFinanceNotes) && string.IsNullOrEmpty(txtFinanceNotes.Text)) ?
                        (object)DBNull.Value :
                        (string.IsNullOrEmpty(txtFinanceNotes.Text) ? existingFinanceNotes : DateTime.Now.ToString("dd-MM-yyyy") + " :" + txtFinanceNotes.Text + Environment.NewLine + existingFinanceNotes);


                    propertyRecord.EndEdit();

                    int propertyId = int.Parse(propertyRecord["pro_propertyid"].ToString());

                    //Inserting Updates into the FinanceBrokerUpdate table
                    DataRow dataRow;
                    dataRow = dataSet.Tables[1].NewRow();
                    dataRow["fbu_propertyid"] = propertyId;
                    dataRow["fbu_lendingrecacceptbyclient"] = (txtLendRecAccByClient.Text == string.Empty) ? (object)DBNull.Value : (object)DateTime.Parse(txtLendRecAccByClient.Text);
                    dataRow["fbu_financelodged"] = (txtFinanceLodged.Text == string.Empty) ? (object)DBNull.Value : (object)DateTime.Parse(txtFinanceLodged.Text);
                    dataRow["fbu_financeassessed"] = (txtFinanceAssessed.Text == string.Empty) ? (object)DBNull.Value : (object)DateTime.Parse(txtFinanceAssessed.Text);
                    dataRow["fbu_conditionalapproval"] = (txtConditionalApproval.Text == string.Empty) ? (object)DBNull.Value : (object)DateTime.Parse(txtConditionalApproval.Text);
                    dataRow["fbu_valuationordered"] = (txtValuationOrdered.Text == string.Empty) ? (object)DBNull.Value : (object)DateTime.Parse(txtValuationOrdered.Text);
                    dataRow["fbu_valuationreturned"] = (txtValuationReturned.Text == string.Empty) ? (object)DBNull.Value : (object)DateTime.Parse(txtValuationReturned.Text);
                    dataRow["fbu_financeapproveddate"] = (txtFormalApproval.Text == string.Empty) ? (object)DBNull.Value : (object)DateTime.Parse(txtFormalApproval.Text);
                    dataRow["fbu_financedeclined"] = (txtFinanceDeclined.Text == string.Empty) ? (object)DBNull.Value : (object)DateTime.Parse(txtFinanceDeclined.Text);
                    dataRow["fbu_mortgagedocsreturned"] = (txtMortgageDocsReturned.Text == string.Empty) ? (object)DBNull.Value : (object)DateTime.Parse(txtMortgageDocsReturned.Text);
                    dataRow["fbu_settlementdate"] = (txtSettlementDate.Text == string.Empty) ? (object)DBNull.Value : (object)DateTime.Parse(txtSettlementDate.Text);
                    dataRow["fbu_largefinancenotes"] = (txtFinanceNotes.Text == string.Empty) ? (object)DBNull.Value : txtFinanceNotes.Text;
                    dataRow["fbu_createddate"] = DateTime.Now;
                    dataSet.Tables[1].Rows.Add(dataRow);

                    sqlDA.Update(dataSet.Tables[0]);
                    sql_FBU_DA.Update(dataSet.Tables[1]);

                    lblResultMessage.Text = "Success : Finance date(s) for property updated successfully.";
                    lblResultMessage.CssClass = "alert alert-success";
                }
                else
                {
                    lblResultMessage.Text = "Matching MYOB ID could not be found; please check the MYOB ID and try again.";
                    lblResultMessage.CssClass = "alert alert-danger";
                }
            }
            catch (Exception ex)
            {
                lblResultMessage.Text = "Oops! Something went wrong. Please try again." + ex.Message;
                lblResultMessage.CssClass = "alert alert-danger";
            }
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
            txtFormalApproval.Text = "";
            txtFinanceDeclined.Text = "";
            txtMortgageDocsReturned.Text = "";
            txtSettlementDate.Text = "";
            txtFinanceNotes.Text = "";
            lblResultMessage.Text = "";
            lblResultMessage.CssClass = "";
        }
    }
}