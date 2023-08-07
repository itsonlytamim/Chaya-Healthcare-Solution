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

namespace Myproject
{
    public partial class MemberSignUp : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public MemberSignUp()
        {
            InitializeComponent();
           // BindGridView();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
             if (textBox8.Text == textBox9.Text && textBox1.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "")
            {
                SqlConnection connection = new SqlConnection(cs);

 

                string query = "SELECT EMAIL_ADDR  From MEMBER_DETAILS Where EMAIL_ADDR= @EMAIL_ADDR";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@EMAIL_ADDR", textBox6.Text);

 

                connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                 {
                     MessageBox.Show("Email address is not unique", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dr.Close();

 

                }

 

                else
                {
                    
                    dr.Close();
                    string insert = "Insert into MEMBER_DETAILS (Name,AGE,GENDER,DOB,ADDR,EMAIL_ADDR,MOBILE_NUMBER,PASSWRD,PICTURE) values (@Name,@AGE,@GENDER,@DOB,@ADDR,@EMAIL_ADDR,@MOBILE_NUMBER,@PASSWRD,@PICTURE)";
                    
                    SqlCommand ins = new SqlCommand(insert, connection);
                    ins.Parameters.AddWithValue("@Name",textBox1.Text);
                    ins.Parameters.AddWithValue("@AGE", numericUpDown1.Value);
                    ins.Parameters.AddWithValue("@GENDER", textBox3.Text);
                    ins.Parameters.AddWithValue("@DOB", textBox4.Text);
                    ins.Parameters.AddWithValue("@ADDR", textBox5.Text);
                    ins.Parameters.AddWithValue("@EMAIL_ADDR", textBox6.Text);
                    ins.Parameters.AddWithValue("@MOBILE_NUMBER", textBox7.Text);
                    ins.Parameters.AddWithValue("@PASSWRD",textBox8.Text);
                    ins.Parameters.AddWithValue("@PICTURE", SavePhoto());

 

                    int a = ins.ExecuteNonQuery();
                    MessageBox.Show("Successfully added", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    SignIn sgn = new SignIn();
                    sgn.Show();
                }

                connection.Close();
            }
            else
            {
                MessageBox.Show("please fill all the field", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f25 = new Form2();
            f25.Show();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) == true)
            {
                textBox1.Focus();
                errorProvider1.SetError(this.textBox1, "Enter Name!");

            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void numericUpDown1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(numericUpDown1.Text) == true)
            {
                numericUpDown1.Focus();
                errorProvider2.SetError(this.numericUpDown1, "Enter Age!");

            }
            else
            {
                
                errorProvider2.Clear();
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text) == true)
            {
                textBox3.Focus();
                errorProvider3.SetError(this.textBox3, "Enter Gender!");

            }
            else
            {
                errorProvider3.Clear();
            }
        }

        

        private void textBox4_Leave_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text) == true)
            {
                textBox4.Focus();
                errorProvider4.SetError(this.textBox4, "Enter Date-of-birth!");

            }
            else
            {
                errorProvider4.Clear();
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox5.Text) == true)
            {
                textBox5.Focus();
                errorProvider5.SetError(this.textBox5, "Enter Address!");

            }
            else
            {
                errorProvider5.Clear();
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox6.Text) == true)
            {
                textBox6.Focus();
                errorProvider6.SetError(this.textBox6, "Enter email address!");

            }
            else
            {
                errorProvider6.Clear();
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox7.Text) == true)
            {
                textBox7.Focus();
                errorProvider7.SetError(this.textBox7, "Enter mobile number!");

            }
            else
            {
                errorProvider7.Clear();
            }
        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox8.Text) == true)
            {
                textBox8.Focus();
                errorProvider8.SetError(this.textBox8, "Enter password!");

            }
            else
            {
                errorProvider8.Clear();
            }
        }

        private void textBox9_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox9.Text) == true)
            {
                textBox9.Focus();
                errorProvider9.SetError(this.textBox9, "Re-Enter password !");

            }
            else
            {
                errorProvider9.Clear();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool status = checkBox1.Checked;
            switch (status)
            {
                case true:
                    textBox8.UseSystemPasswordChar = false;
                    break;
                default:
                    textBox8.UseSystemPasswordChar = true;
                    break;

            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            bool status = checkBox2.Checked;
            switch (status)
            {
                case true:
                    textBox9.UseSystemPasswordChar = false;
                    break;
                default:
                    textBox9.UseSystemPasswordChar = true;
                    break;

            }
        }

        
        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            ResetControl();
        }

         void ResetControl()
        {
            textBox1.Clear();
            
            numericUpDown1.Value = 0;
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            pictureBox1.Image = Properties.Resources.No_Image_Available;

        }

         private void button4_Click(object sender, EventArgs e)
         {
             OpenFileDialog of = new OpenFileDialog();
             of.Title = "SELECT IMAGE";
             of.Filter = "SELECT IMAGE (ALL FILES) *.*)|*.*";
             if (of.ShowDialog() == DialogResult.OK)
             {
                 pictureBox1.Image = new Bitmap(of.FileName);
             }
         }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }

       
    }





            
