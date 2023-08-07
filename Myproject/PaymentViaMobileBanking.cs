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

namespace Myproject
{
    public partial class PaymentViaMobileBanking : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public PaymentViaMobileBanking()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserHomePage uhp = new UserHomePage();
            uhp.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ResetControl();
        }

        void ResetControl()
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrontPage f258 = new FrontPage();
            f258.Show();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Focus();
                errorProvider1.SetError(this.textBox1, "EnterTransaction Id Please!");
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
                textBox2.Focus();
                errorProvider2.SetError(this.textBox2, "Enter Phone Number Please!");
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                SqlConnection con = new SqlConnection(cs);
                con.Open();

                SqlCommand cmda = new SqlCommand("insert into BKASH_DETAILS values (@TRANSACTION_ID ,@PHONE_NUMBER,@email_addr)", con);

                cmda.Parameters.AddWithValue("@TRANSACTION_ID", textBox1.Text);
                cmda.Parameters.AddWithValue("@PHONE_NUMBER", textBox2.Text);
                 cmda.Parameters.AddWithValue("@email_addr", Myproject.SignIn.emailAdd);
                
                int a = cmda.ExecuteNonQuery();
                if (a > 0)
                {
                    MessageBox.Show("Successfully Paid !", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    UserHomePage uhp = new UserHomePage();
                    uhp.Show();

                }
            
                else
                {

                    MessageBox.Show("Not Paid !", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
            }
            else
            {
                MessageBox.Show("Please fill the form ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }

        }
    

