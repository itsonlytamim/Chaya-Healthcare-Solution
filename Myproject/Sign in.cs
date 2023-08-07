using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sijan_ft_project;
using System.Windows.Forms;

namespace Myproject
{
    public partial class SignIn : Form
    {
        public static string emailAdd,docEmailAdd;
        public SignIn()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                errorProvider1.SetError(this.textBox1, "Please enter your email!");
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
                errorProvider2.SetError(this.textBox2, "Please enter your password!");
            }

            else
            {
                errorProvider2.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrontPage f25 = new FrontPage();
            f25.Show();

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) == true)
            {
                textBox1.Focus();
                errorProvider1.SetError(this.textBox1, "Enter email!");

            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text) == true)
            {
                textBox2.Focus();
                errorProvider2.SetError(this.textBox2, "Enter password!");

            }
            else
            {
                errorProvider2.Clear();
            }
        }



        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            bool status = checkBox1.Checked;
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != ""&& comboBox1.Text!=""&& comboBox1.Text=="Member")
            {
                Member m= new Member();
                if(m.login(textBox1.Text,textBox2.Text))
                {
                    MessageBox.Show("LOG IN SUCCESSFUL");
                    
                    emailAdd = textBox1.Text;
                    
                    this.Hide();
                    UserHomePage userr = new UserHomePage();
                    userr.Show();


                }

                else
                {
                    MessageBox.Show("INVALID INFORMATION");
                }



            }

        else if (textBox1.Text != "" && textBox2.Text != ""&& comboBox1.Text!=""&& comboBox1.Text=="Doctor")
            {
            Doctor d= new Doctor();
                if(d.login(textBox1.Text,textBox2.Text))
                {
                    MessageBox.Show("LOG IN SUCCESSFUL");
                    docEmailAdd = textBox1.Text;
                    this.Hide();
                    Form1 f1 = new Form1();
                    f1.Show();
                }
                else
                {
                    MessageBox.Show("INVALID INFORMATION");
                }
            }

            else if (textBox1.Text != "" && textBox2.Text != "" && comboBox1.Text != "" && comboBox1.Text == "Specialist_doctor")
            {
                Specialist_doctor s = new Specialist_doctor();
                if (s.login(textBox1.Text, textBox2.Text))
                {
                    MessageBox.Show("LOG IN SUCCESSFUL");
                    docEmailAdd = textBox1.Text;
                    this.Hide();
                    Form6 f6 = new Form6();
                    f6.Show();
                }
                else
                {
                    MessageBox.Show("INVALID INFORMATION");
                }
            }


            

            else
            {
                MessageBox.Show("Please fill the form", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);


            }
            
        }
        }
    }


