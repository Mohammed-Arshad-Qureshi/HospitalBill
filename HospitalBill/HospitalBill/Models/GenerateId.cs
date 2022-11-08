using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HospitalBill.Models
{
    public static class GenerateId
    {
        public static int getIdData()
        {
            int billnumber = GetDataValue();
            if (billnumber == 0)
            {
                billnumber = 1;
                return billnumber;
            }
            else
            {
                billnumber = billnumber + 1;
                return billnumber;
            }
        }


        public static int GetDataValue()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionStringData"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            int patid = 0;
            using (connection)
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("GetMaxId");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection;
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            patid = reader["BillNumber"] == DBNull.Value ? default(Int32) : Convert.ToInt32(reader["BillNumber"]);
                        }
                        return patid;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}