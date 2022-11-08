using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HospitalBill.Hospital
{
    public partial class Hospital : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionStringData"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            var data = DropDownList2.SelectedValue.ToString();
            int fees = Fees.Price(data);
            if (fees != 0)
            {
                TextBox5.Text = fees.ToString();
            }

            if (!IsPostBack)
            {
                try
                {
                    using (con)
                    {
                        var pid = Convert.ToInt32(Session["id"]);
                        var billDate = string.Empty;
                        var patient = string.Empty;
                        var gender = string.Empty;
                        var dob = string.Empty;
                        var address = string.Empty;
                        var email = string.Empty;
                        long mobile = 0;

                        if (pid != 0)
                        {
                            SqlCommand cmd1 = new SqlCommand("GetPatientById");
                            cmd1.Connection = con;
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.Parameters.AddWithValue("@id", pid);
                            SqlDataReader reader = cmd1.ExecuteReader();
                            while (reader.Read())
                            {
                                billDate = reader["BillDate"].ToString();
                                patient = reader["PatientName"].ToString();
                                gender = reader["Gender"].ToString();
                                dob = reader["DOB"].ToString();
                                address = reader["Address"].ToString();
                                email = reader["Email"].ToString();
                                mobile = Convert.ToInt64(reader["Mobile"]);
                            }
                            TextBox1.Text = pid.ToString();
                            TextBox2.Text = billDate;
                            TextBox3.Text = patient;
                            DropDownList1.SelectedValue = gender;
                            Date1.Value = dob;
                            TextArea1.Value = address;
                            TextBox7.Text = email;
                            TextBox8.Text = mobile.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBox1.Text == "" && TextBox2.Text == "")
                {
                    string value = Convert.ToString(GenerateId.getIdData());
                    TextBox1.Text = value;
                    string date = DateTime.Now.ToString();
                    TextBox2.Text = date;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionStringData"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlConnection con = new SqlConnection(connectionString);
            if (TextBox1.Text != "" && TextBox2.Text != "")
            {
                int id = 0;
                var data = DropDownList2.SelectedValue.ToString();
                int fees = Fees.Price(data);
                if (fees != 0)
                {
                    TextBox5.Text = fees.ToString();
                }

                using (connection)
                {
                    try
                    {
                        if (data != "-- Select --")
                        {
                            connection.Open();
                            SqlCommand cmd = new SqlCommand("AddPatient");
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = connection;
                            cmd.Parameters.AddWithValue("@BillNumber", TextBox1.Text);
                            cmd.Parameters.AddWithValue("@BillDate", TextBox2.Text);
                            cmd.Parameters.AddWithValue("@PatientName", TextBox3.Text);
                            cmd.Parameters.AddWithValue("@Gender", DropDownList1.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@DOB", Date1.Value);
                            cmd.Parameters.AddWithValue("@Address", TextArea1.Value);
                            cmd.Parameters.AddWithValue("@Email", TextBox7.Text);
                            cmd.Parameters.AddWithValue("@Mobile", TextBox8.Text);
                            cmd.Parameters.AddWithValue("@disease", DropDownList2.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@Fees", TextBox5.Text);
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                id = Convert.ToInt32(reader["BillNumber"]);
                            }
                            connection.Close();
                            reader.Close();
                            Session["id"] = Convert.ToInt32(TextBox1.Text);
                            Page.Response.RedirectPermanent("~/Hospital/Hospital.aspx");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextArea1.Value = "";
            TextBox5.Text = "";
            Date1.Value = "dd-mm-yy";
            TextBox7.Text = "";
            TextBox8.Text = "";
            DropDownList1.SelectedValue = "Male";
            DropDownList2.SelectedValue = "-- Select --";
            Session["id"] = 0;
            Page.Response.RedirectPermanent("~/Hospital/Hospital.aspx");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionStringData"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            try
            {
                using (connection)
                {
                    StringBuilder sb = new StringBuilder();
                    string csv = ", ";
                    var pid = Convert.ToInt32(Session["id"]);
                    var gender = DropDownList1.SelectedValue.ToString();
                    string mobile = TextBox8.Text;
                    var data = pid + csv + TextBox2.Text + csv + TextBox3.Text + csv + gender + csv + Date1.Value + csv + TextArea1.Value + csv + TextBox7.Text + csv + mobile + "\r\n\n\n";
                    sb.Append(data);

                    SqlCommand cmd1 = new SqlCommand("GetProblemData");
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Connection = connection;
                    cmd1.Parameters.AddWithValue("@id", pid);
                    SqlDataReader reader = cmd1.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            sb.Append((Convert.ToInt32(reader["Sno"])) + csv);
                            sb.Append((reader["Problem"].ToString()) + csv);
                            sb.Append(reader["Fees"].ToString());
                            sb.Append("\r\n");
                        }
                    }

                    string path = @"C:\Users\DELL\Desktop\Arshad_4\PatientBill\PatientBill\Data.csv";
                    StreamWriter sw = new StreamWriter(path);
                    sw.WriteLine(sb.ToString());
                    sw.Close();

                    string alertdata = "<script> alert('Data Exported SuccessFully')</script>";
                    Page.RegisterClientScriptBlock(default(string), alertdata);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void Button4_Click(object sender, EventArgs e)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionStringData"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            try
            {
                using (con)
                {
                    if (TextBox1.Text != "" && TextBox2.Text != "" && TextBox3.Text != "" && Date1.Value != "")
                    {
                        SqlCommand cmd = new SqlCommand("UpdatePatient");
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BillNumber", TextBox1.Text);
                        cmd.Parameters.AddWithValue("@BillData", TextBox2.Text);
                        cmd.Parameters.AddWithValue("@PatientName", TextBox3.Text);
                        cmd.Parameters.AddWithValue("@Gender", DropDownList1.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@DOB", Date1.Value);
                        cmd.Parameters.AddWithValue("@Address", TextArea1.Value);
                        cmd.Parameters.AddWithValue("@Email", TextBox7.Text);
                        cmd.Parameters.AddWithValue("@Mobile", TextBox8.Text);
                        cmd.ExecuteNonQuery();

                        string data = "<script> alert('Data Saved SuccessFully')</script>";
                        Page.RegisterClientScriptBlock(default(string), data);

                        TextBox1.Text = "";
                        TextBox2.Text = "";
                        TextBox3.Text = "";
                        TextArea1.Value = "";
                        TextBox5.Text = "";
                        Date1.Value = "dd-mm-yy";
                        TextBox7.Text = "";
                        TextBox8.Text = "";
                        DropDownList1.SelectedValue = "Male";
                        DropDownList2.SelectedValue = "-- Select --";
                        Session["id"] = 0;
                    }
                    else
                    {
                        string data1 = "<script> alert('Fill the data')</script>";
                        Page.RegisterClientScriptBlock(default(string), data1);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionStringData"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            try
            {
                using (con)
                {
                    var pid = Convert.ToInt32(Session["id"]);
                    int id = GenerateId.GetDataValue();
                    var billDate = string.Empty;
                    var patient = string.Empty;
                    var gender = string.Empty;
                    var dob = string.Empty;
                    var address = string.Empty;
                    var email = string.Empty;
                    long mobile = 0;

                    SqlCommand cmd1 = new SqlCommand("GetPatientById");
                    cmd1.Connection = con;
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd1.ExecuteReader();
                    while (reader.Read())
                    {
                        billDate = reader["BillDate"].ToString();
                        patient = reader["PatientName"].ToString();
                        gender = reader["Gender"].ToString();
                        dob = reader["DOB"].ToString();
                        address = reader["Address"].ToString();
                        email = reader["Email"].ToString();
                        mobile = Convert.ToInt64(reader["Mobile"]);
                    }
                    con.Close();
                    if (id != 0)
                    {
                        TextBox1.Text = id.ToString();
                        TextBox2.Text = billDate;
                        TextBox3.Text = patient;
                        DropDownList1.SelectedValue = gender;
                        Date1.Value = dob;
                        TextArea1.Value = address;
                        TextBox7.Text = email;
                        TextBox8.Text = mobile.ToString();
                        Session["id"] = id;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Button9_Click(object sender, EventArgs e)
        {

        }

        //protected void Button8_Click(object sender, EventArgs e)
        //{
        //    var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionStringData"].ConnectionString;
        //    SqlConnection con = new SqlConnection(connectionString);
        //    string email = "roshni@gmail.com";
        //    int num = 3;
        //    con.Open();
        //    SqlCommand cmd1 = new SqlCommand("TESTINGEMAIL");
        //    cmd1.Connection = con;
        //    cmd1.CommandType = CommandType.StoredProcedure;
        //    cmd1.Parameters.AddWithValue("@ID", num);
        //    cmd1.Parameters.AddWithValue("@EMAIL", email);
        //    cmd1.Parameters.Add("@ONE", SqlDbType.Int, 0);
        //    cmd1.Parameters.Add("@TWO", SqlDbType.VarChar, 40);

        //    cmd1.Parameters["@ONE"].Direction = ParameterDirection.Output;
        //    cmd1.Parameters["@TWO"].Direction = ParameterDirection.Output;

        //    cmd1.ExecuteNonQuery();
        //    int value1 = Convert.ToInt32(cmd1.Parameters["@ONE"].Value);
        //    string value2 = (cmd1.Parameters["@TWO"].Value).ToString();

        //    if(value1 > 1)
        //    {
        //        value2 = "YES " + value2;
        //    }
        //    else
        //    {
        //        value2 = "NO " + value2;
        //    }
        //}
    }
}