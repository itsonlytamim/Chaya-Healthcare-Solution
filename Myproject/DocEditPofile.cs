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
    public partial class Form3 : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        string password_doc;
        public Form3()
        {
            InitializeComponent();
            BindGridView();
        }

       /* private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }*/

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select photos";
            ofd.Filter = "Image FILE(ALL files)*.*)|*.*";
           // ofd.ShowDialog();
           if(ofd.ShowDialog() ==DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(ofd.FileName);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Would you like to delete your account","Sure");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ResetControl();
        }

        private void ResetControl()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            pictureBox1.Image = Myproject.Properties.Resources.No_Image_Available;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text))
            {
               errorProvider1.SetError(this.textBox1, "please enter your name");
            }
            else
            {
                errorProvider1.Clear();
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           if (string.IsNullOrEmpty(textBox2.Text))
            {
                errorProvider2.SetError(this.textBox2, "please enter your nationality");
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand readingCom = new SqlCommand("select passwrd from DOCTOR_DETAILS where email_addr=@email_addr", con);
            readingCom.Parameters.AddWithValue("@email_addr", SignIn.docEmailAdd);
            SqlDataReader dr = readingCom.ExecuteReader();
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    password_doc = Convert.ToString(dr.GetValue(0));

                }
                dr.Close();
            }
            dr.Close();
            if (textBox1.Text == password_doc && textBox2.Text == textBox4.Text&& textBox4.Text!="")
            {
                // string query = "update  DOCTOR_DETAILS set id=@id,name=@name,age=@age,picture=@img where id=@id";
                SqlCommand cmd = new SqlCommand("update DOCTOR_DETAILS set passwrd=@passwrd,NATIONALITY=@NATIONALITY,PRO_PICTURE=@PRO_PICTURE where email_addr=@email_addr", con);
                cmd.Parameters.AddWithValue("@email_addr", SignIn.docEmailAdd);
                cmd.Parameters.AddWithValue("@passwrd", textBox4.Text);
                cmd.Parameters.AddWithValue("@nationality", textBox3.Text);
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
           
           
                //string query = "select * from edit_profile";
                SqlCommand cmd = new SqlCommand("select name,dob,nationality,pro_picture from DOCTOR_DETAILS where email_addr=@email_addr", con);
                cmd.Parameters.AddWithValue("@email_addr", SignIn.docEmailAdd);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                DataTable data = new DataTable();
                sda.Fill(data);
                dataGridView1.DataSource = data;

                ///Image Column
                DataGridViewImageColumn dgv = new DataGridViewImageColumn();
                dgv = (DataGridViewImageColumn)dataGridView1.Columns[3];
                dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;
                //AutoSize
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                //Height
                dataGridView1.RowTemplate.Height = 80;
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
           // string query = "delete from edit_profile where address=@address";
            SqlCommand cmd = new SqlCommand("Delete from DOCTOR_DETAILS where email_addr=@email_addr", con);
            cmd.Parameters.AddWithValue("@email_addr", SignIn.docEmailAdd);
            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("ACCOUNT DELETED", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindGridView();
                ResetControl();
                this.Hide();
                SignIn sIn = new SignIn();
                sIn.Show();
            }
            else
            {
                MessageBox.Show("ACCOUNT NOT deleted!");
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();

            //numericUpDown1.Value = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[2].Value);
            pictureBox1.Image = GetPhoto((byte[])dataGridView1.SelectedRows[0].Cells[3].Value);
        }

        private Image GetPhoto(byte[] p)
        {
            MemoryStream ms = new MemoryStream(p);
            return Image.FromStream(ms);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void dataGridView1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            pictureBox1.Image = GetPhoto((byte[])dataGridView1.SelectedRows[0].Cells[3].Value);
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
