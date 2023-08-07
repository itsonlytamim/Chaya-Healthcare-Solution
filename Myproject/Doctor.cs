using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myproject
{
    class Doctor
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public bool login(string email, string password)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "SELECT * from DOCTOR_DETAILS where EMAIL_ADDR=@email and PASSWRD=@password";
            SqlCommand cmd = new SqlCommand(query, con);


            cmd.Parameters.AddWithValue("@email", email);

            cmd.Parameters.AddWithValue("@password", password);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    con.Close();
                    return true;
                }
            }
            else
            {
                con.Close();
                return false;
            }
            return false;
            
        }
    
    }
}
