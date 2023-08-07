﻿using Myproject;
using Project;
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
using UserPart;

namespace Myproject
{
    public partial class Medicine : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Medicine()
        {
            InitializeComponent();
            BindMedSpecialistGridView();
        }
        void BindMedSpecialistGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd = new SqlCommand("select name,dob,pro_picture from SPECIALIST_DOCTOR_DETAILS where designation=@designation", con);
            cmd.Parameters.AddWithValue("@designation", "MEDICINE SPECIALIST");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;
            DataGridViewImageColumn dgvic = new DataGridViewImageColumn();
            dgvic = (DataGridViewImageColumn)dataGridView1.Columns[2];
            dgvic.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 100;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserHomePage form1 = new UserHomePage();
            form1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrontPage f1 = new FrontPage();
            f1.Show();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Chatting.docName = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            this.Hide();
            MessageDashBoard nPage = new MessageDashBoard();
            nPage.ChangeLabel(this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
            nPage.Show();
        }
    }
}
