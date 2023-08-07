using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Myproject
{
    public partial class Form5 : Form
    {

        string name, dob, nationality, email_address, password,re_enter_password;
        byte []certificate_image,cv_image,pro_picture;
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        public Form5(string name,string dob,string nationality,string email_address,string password,byte []certificate_image,byte[]cv_image,byte[]pro_picture,string re_enter_password)
        {

            InitializeComponent();
            this.name = name;
            this.dob= dob;
            this.nationality = nationality;
            this.email_address = email_address;
            this.password = password;
            this.re_enter_password = re_enter_password;
            this.certificate_image = certificate_image;
            this.cv_image = cv_image;
            this.pro_picture = pro_picture;
            

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Title = "SELECT IMAGE";
            of.Filter = "SELECT IMAGE (ALL FILES) *.*)|*.*";
            if (of.ShowDialog() == DialogResult.OK)
            {
                pictureBox4.Image = new Bitmap(of.FileName);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f = new Form4("Specialist doctor");
            f.Show();        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.password == this.re_enter_password)
            {
                SqlConnection con = new SqlConnection(cs);
                string datas = "SELECT EMAIL_ADDR  From SPECIALIST_DOCTOR_DETAILS Where EMAIL_ADDR= @EMAIL_ADDR";
                SqlCommand commnd = new SqlCommand(datas, con);
                commnd.Parameters.AddWithValue("@EMAIL_ADDR", Form4.emailOfDoc);
                con.Open();
                SqlDataReader dr = commnd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    MessageBox.Show("Email address is not unique", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Hide();
                    Form4 f = new Form4("Specialist doctor");
                    f.Show();
                    dr.Close();
                }

                else
                {
                    dr.Close();
                    string query = "insert into SPECIALIST_DOCTOR_DETAILS values(@name,@dob,@nationality,@email_address,@password,@certificate_image,@cv_image,@pro_picture,@designation,@masters_higher_degree_certificate_picture)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@name", this.name);
                    cmd.Parameters.AddWithValue("@dob", this.dob);
                    cmd.Parameters.AddWithValue("@nationality", this.nationality);
                    cmd.Parameters.AddWithValue("@email_address", this.email_address);
                    cmd.Parameters.AddWithValue("@password", this.password);
                    cmd.Parameters.AddWithValue("@certificate_image", this.certificate_image);
                    cmd.Parameters.AddWithValue("@pro_picture", this.pro_picture);
                    cmd.Parameters.AddWithValue("@cv_image", this.cv_image);
                    cmd.Parameters.AddWithValue("@designation", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@masters_higher_degree_certificate_picture", SavePhoto());
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        MessageBox.Show(" Sign up successful ", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        SignIn sn = new SignIn();
                        sn.Show();
                    }
                    else
                    {
                        MessageBox.Show(" Sign up not successful", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                con.Close();
            }
            else
            {
                MessageBox.Show("password not matched");
            }        
        }
                
            

        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox4.Image.Save(ms, pictureBox4.Image.RawFormat);
            return ms.GetBuffer();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ResetControl();
        }

        void ResetControl()
        {        
            pictureBox4.Image = Properties.Resources.No_Image_Available;
        }

        
    }
}
