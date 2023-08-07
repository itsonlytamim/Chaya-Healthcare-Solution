using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using UserPart;
using Myproject;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Project
{
    public partial class MessageDashBoard : Form
    {

        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public MessageDashBoard()
          {
              InitializeComponent();
              BindTextMessageGridView();                  
        }
        public void Tracker()
        {
            button11.Visible = false;
            button1.Visible = false;

        }
        public void ChangeLabel(string s)
        {
            label2.Text = s;
        }
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            label4.Visible = true;
            label4.Text = (string)dataGridView1.SelectedRows[0].Cells[0].Value;
            richTextBox1.Text= (string)dataGridView1.SelectedRows[0].Cells[3].Value;
            richTextBox2.Text = (string)dataGridView1.SelectedRows[0].Cells[4].Value;  
            label2.Text= (string)dataGridView1.SelectedRows[0].Cells[1].Value;
        }
        void BindTextMessageGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            SqlCommand cmd = new SqlCommand("Select issue_no,doctor_name ,time_date,texts,replies  from member_texting where email_addr=@email_addr order by time_date desc", con);
            cmd.Parameters.AddWithValue("@email_addr", SignIn.emailAdd);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;          
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 75;
        }      
        private void button1_Click_1(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete from MEMBER_TEXTING where issue_no=@issue_no", con);
            cmd.Parameters.AddWithValue("@issue_no", (string)dataGridView1.SelectedRows[0].Cells[0].Value);
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Inquiry Closed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindTextMessageGridView();
                richTextBox1.Clear();
                richTextBox2.Clear();
                label4.Visible = false;
            }
            else
            {
                MessageBox.Show("Inquiry Not Closed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button11_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Chatting ch = new Chatting();
            ch.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserHomePage uhp = new UserHomePage();
            uhp.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
