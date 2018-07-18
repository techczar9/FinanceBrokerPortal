using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace FinanceBrokerPortal
{
    public partial class Ajax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        public static string DataTableToJson(string myobId)
        {
            string command = "SELECT * FROM Property WHERE pro_myobid = '" + myobId + "'";
            SQLData dataTable = new SQLData();
            DataTable table = dataTable.GetSQLData(command);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }
    }

}