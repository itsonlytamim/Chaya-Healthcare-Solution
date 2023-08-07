using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using sijan_ft_project;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Myproject
{
    public partial class DoctorDashboard : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public DoctorDashboard()
        {
            InitializeComponent();
            BindTextMessageGridView();
        }
        string isuue_no;
         string doctor_name;

        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox2.Text != "")
            {
                SqlConnection con = new SqlConnection(cs);
                con.Open();

                SqlCommand cmd = new SqlCommand("update MEMBER_TEXTING set REPLIES=@REPLIES where ISSUE_NO=@ISSUE_NO", con);


                cmd.Parameters.AddWithValue("@REPLIES", richTextBox2.Text);

                cmd.Parameters.AddWithValue("@ISSUE_NO", isuue_no);

                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    MessageBox.Show("Replied successfully", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindTextMessageGridView();
                }
                else
                {
                    MessageBox.Show("Replied not successsfully");
                }
            }
            else
            {
                MessageBox.Show("Type Something!");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
          Form1 f1 = new Form1();
            f1.Show();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            richTextBox1.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            richTextBox2.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();

            isuue_no = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
        }
        
        void BindTextMessageGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand readingCom = new SqlCommand("select name from DOCTOR_DETAILS where email_addr=@email_addr", con);
            readingCom.Parameters.AddWithValue("@email_addr", SignIn.docEmailAdd);
            SqlDataReader dr = readingCom.ExecuteReader();
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    doctor_name = Convert.ToString(dr.GetValue(0));

                }
                dr.Close();
            }

            SqlCommand cmd = new SqlCommand("Select issue_no,member_name ,time_date,texts,replies  from member_texting where doctor_name=@doctor_name order by time_date desc", con);
            cmd.Parameters.AddWithValue("@doctor_name", doctor_name);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 75;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            SqlCommand cmd = new SqlCommand("delete from MEMBER_TEXTING where ISSUE_NO=@ISSUE_NO", con);
            cmd.Parameters.AddWithValue("@ISSUE_NO", isuue_no);
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Deleted successfully", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindTextMessageGridView();
                richTextBox1.Clear();
                richTextBox2.Clear();
            }
            else
            {
                MessageBox.Show("Deleted  not successsfully");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox2.Clear();
        }

        private void ResetControl()
        {
               
        }

        public object ISSUE_NO { get; set; }
    }
}
