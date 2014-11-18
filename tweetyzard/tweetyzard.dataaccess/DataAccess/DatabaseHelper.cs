using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace tweetyzard.dataaccess.DataAccess
{
    public static class DatabaseHelper
    {
        public static bool SqlBulkInsert(string tableName, DataTable dataTable)
        {
            bool isSuccess = false;
            string errorMessage = string.Empty;
            try
            {
                string connectionString = System.Configuration.ConfigurationSettings.AppSettings["DbConnectionString"].ToString();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))  
                    {
                        bulkCopy.DestinationTableName = tableName;
                        bulkCopy.WriteToServer(dataTable);
                    }

                    isSuccess = true;
                }
            }
            catch (SqlException ex)
            {
                isSuccess = false;
                errorMessage = ex.Message;
                throw;
            }

            return isSuccess;
        }
    }
}
