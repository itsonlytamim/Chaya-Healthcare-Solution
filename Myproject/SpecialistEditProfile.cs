using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using Myproject;

namespace sijan_ft_project
{
    public partial class Form9 : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        string password_spdoc;
        public Form9()
        {
            InitializeComponent();
            BindGridView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form6 f6 = new Form6();
            f6.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select photos";
            ofd.Filter = "Image FILE(ALL files)*.*)|*.*";
            //ofd.ShowDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(ofd.FileName);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand readingCom = new SqlCommand("select passwrd from SPECIALIST_DOCTOR_DETAILS where email_addr=@email_addr", con);
            readingCom.Parameters.AddWithValue("@email_addr", SignIn.docEmailAdd);
            SqlDataReader dr = readingCom.ExecuteReader();
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    password_spdoc = Convert.ToString(dr.GetValue(0));
                }
                dr.Close();
            }
            dr.Close();
            if (textBox1.Text == password_spdoc && textBox2.Text == textBox4.Text && textBox4.Text!="")
            {               
                SqlCommand cmd = new SqlCommand("update SPECIALIST_DOCTOR_DETAILS set passwrd=@passwrd,NATIONALITY=@NATIONALITY,PRO_PICTURE=@PRO_PICTURE where email_addr=@email_addr", con);
                cmd.Parameters.AddWithValue("@email_addr", SignIn.docEmailAdd);
                cmd.Parameters.AddWithValue("@passwrd", textBox4.Text);
                cmd.Parameters.AddWithValue("@nationality", textBox5.Text);
                cmd.Parameters.AddWithValue("pro_picture", SavePhoto());

                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    MessageBox.Show("ACCOUNT UPDATED", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindGridView();
                    ResetControl();
                }
                else
                {
                    MessageBox.Show("ACCOUNT NOT UPDATED");
                }
            }
            else
            {
                MessageBox.Show("Password Not Matched");
            }
        }

        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();

        }
        void BindGridView()
        {
           SqlConnection con = new SqlConnection(cs);
           con.Open();
            //string query = "select * from ";
           SqlCommand cmd = new SqlCommand("select name,dob,nationality,pro_picture from SPECIALIST_DOCTOR_DETAILS where email_addr=@email_addr", con);
           cmd.Parameters.AddWithValue("@email_addr", SignIn.docEmailAdd);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;
            DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            dgv = (DataGridViewImageColumn)dataGridView1.Columns[3];
            dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 80;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ResetControl();
        }

        private void ResetControl()
        {
            textBox4.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox5.Clear();
            pictureBox1.Image = Myproject.Properties.Resources.No_Image_Available;
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            pictureBox1.Image = GetPhoto((byte[])dataGridView1.SelectedRows[0].Cells[3].Value);
        }

        private Image GetPhoto(byte[] p)
        {
            MemoryStream ms = new MemoryStream(p);
            return Image.FromStream(ms);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form6 F9 = new Form6();
            F9.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("Delete from SPECIALIST_DOCTOR_DETAILS where email_addr=@email_addr", con);
            cmd.Parameters.AddWithValue("@email_addr", SignIn.docEmailAdd);
            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("ACCOUNT DELETED", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindGridView();
                ResetControl();
                this.Hide();
                SignIn s = new SignIn();
                s.Show();

            }
            else
            {
                MessageBox.Show("ACCOUNT NOT deleted!");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool status = checkBox1.Checked;
            switch (status)
            {
                case true:
                    textBox1.UseSystemPasswordChar = false;
                    break;
                default:
                    textBox1.UseSystemPasswordChar = true;
                    break;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            bool status = checkBox2.Checked;
            switch (status)
            {
                case true:
                    textBox2.UseSystemPasswordChar = false;
                    break;
                default:
                    textBox2.UseSystemPasswordChar = true;
                    break;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            bool status = checkBox3.Checked;
            switch (status)
            {
                case true:
                    textBox4.UseSystemPasswordChar = false;
                    break;
                default:
                    textBox4.UseSystemPasswordChar = true;
                    break;
            }
        }
    }
}
    


