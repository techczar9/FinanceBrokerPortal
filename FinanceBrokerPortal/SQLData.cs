﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FinanceBrokerPortal
{
    public class SQLData
    {
        public string ConnectionString { get; set; } = ConfigurationManager.ConnectionStrings["SQLConString"].ConnectionString;
        private SqlDataAdapter sqlDA;
        private DataTable dataTable;

        public DataTable GetSQLData(string command)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                sqlDA = new SqlDataAdapter(command, sqlCon);
                dataTable = new DataTable();
                sqlDA.Fill(dataTable);
                return dataTable;
            }
        }

        public bool CheckPropertyExists(string command)
        {
            DataTable dataTable = GetSQLData(command);

            return (dataTable.Rows.Count == 0) ? false : true;
        }
    }
}