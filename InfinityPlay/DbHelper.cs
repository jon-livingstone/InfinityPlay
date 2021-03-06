﻿using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace InfinityPlay
{
    public class DbHelper
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["InfinityPlay"].ConnectionString;

        public static List<DataRow> Query(string queryText)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryText, connection))
                {
                    connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    da.Fill(dataTable);

                    return dataTable.Rows.OfType<DataRow>().ToList();
                }
            }
        }
    }
}