using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Myproject;

namespace sijan_ft_project
{
    public partial class Form6 : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Form6()
        {
            InitializeComponent();
            BindSpecialistGridView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            SpecialistDoctorDashboard sp = new SpecialistDoctorDashboard();
            sp.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form9 F9 = new Form9();
            F9.Show();
        }
        void BindSpecialistGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd = new SqlCommand("select pro_picture from SPECIALIST_DOCTOR_DETAILS where email_addr=@email_addr", con);
            cmd.Parameters.AddWithValue("@email_addr", SignIn.docEmailAdd);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;
            DataGridViewImageColumn dgvic = new DataGridViewImageColumn();
            dgvic = (DataGridViewImageColumn)dataGridView1.Columns[0];
            dgvic.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 111;
            SqlCommand readingCom = new SqlCommand("select name from SPECIALIST_DOCTOR_DETAILS where email_addr=@email_addr", con);
            readingCom.Parameters.AddWithValue("@email_addr", SignIn.docEmailAdd);
            SqlDataReader dr = readingCom.ExecuteReader();
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    label2.Text = Convert.ToString(dr.GetValue(0));

                }
                dr.Close();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            FrontPage f1 = new FrontPage();
            f1.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
