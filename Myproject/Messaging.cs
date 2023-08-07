using Myproject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using Project;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserPart
{
    public partial class Chatting : Form
    {
        public static string docName;
        string member_name;
        private static readonly Random getrandom = new Random();
        public string issue_number = "IN-AHC" + GetRandomNumber(100, 999);
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom)
            {
                return getrandom.Next(min, max);
            }
        }
        public Chatting()
        {
            InitializeComponent();
        }  
        public bool trackingDoc()
        {
            return true;
        }
        public bool trackingSpecialDoc()
        {
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MessageDashBoard mp = new MessageDashBoard();
            mp.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrontPage f1 = new FrontPage();
            f1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "")
            {
                SqlConnection con = new SqlConnection(cs);

                con.Open();
                SqlCommand readingCom = new SqlCommand("select name from MEMBER_DETAILS where email_addr=@email_addr", con);
                readingCom.Parameters.AddWithValue("@email_addr", SignIn.emailAdd);
                SqlDataReader dr = readingCom.ExecuteReader();
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        member_name = Convert.ToString(dr.GetValue(0));

                    }
                    dr.Close();
                }

                
                SqlCommand cmd = new SqlCommand("Insert into Member_Texting (issue_no,doctor_name,time_date,texts,replies,email_addr,member_name) values(@issue_no,@doctor_name,getdate(),@texts,@replies,@email_addr,@member_name)", con);
                cmd.Parameters.AddWithValue("@issue_no", issue_number);
                cmd.Parameters.AddWithValue("@doctor_name", docName);
                cmd.Parameters.AddWithValue("@texts", richTextBox1.Text);
                cmd.Parameters.AddWithValue("@replies", "-");
                cmd.Parameters.AddWithValue("@email_addr", SignIn.emailAdd);
                cmd.Parameters.AddWithValue("@member_name", member_name);
                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    MessageBox.Show("Text Sent", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    richTextBox1.ReadOnly = true;
                    this.Close();
                    MessageDashBoard message = new MessageDashBoard();
                    message.Show();
                }
                else
                {
                    MessageBox.Show("Text not sent", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Type Something!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.ReadOnly = true;
        }
    }
}

