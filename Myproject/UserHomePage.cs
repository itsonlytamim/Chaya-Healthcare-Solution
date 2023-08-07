using Myproject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using Project;
using UserPart;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//using Myproject.Properties;

namespace Myproject
{
    public partial class UserHomePage : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public bool isPremium=false;
        public UserHomePage()
        {
            InitializeComponent();
            BindUserGridView();
            
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) AS RowCnt FROM BKASH_DETAILS where email_addr=@email_addr", con);
            cmd.Parameters.AddWithValue("@email_addr", Myproject.SignIn.emailAdd);       
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                int RowCnt = dr.GetInt32(0);
                if (RowCnt == 0)
                {
                    isPremium = true;
                    break;
                }
            }
            con.Close();
            if (!isPremium)
            {
                label3.Visible = true;
                comboBox1.Visible = true;
                button1.Visible = true;
               // groupBox1.Visible = true;
                this.pictureBox1.Visible = false;

            }
        }
        void BindUserGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd = new SqlCommand("select picture from MEMBER_DETAILS where email_addr=@email_addr", con);           
            cmd.Parameters.AddWithValue("@email_addr", Myproject.SignIn.emailAdd);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;
            DataGridViewImageColumn dgvic = new DataGridViewImageColumn();
            dgvic = (DataGridViewImageColumn)dataGridView1.Columns[0];
            dgvic.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height =111;
            SqlCommand readingCom = new SqlCommand("select name from MEMBER_DETAILS where email_addr=@email_addr", con);
            readingCom.Parameters.AddWithValue("@email_addr", Myproject.SignIn.emailAdd);
            SqlDataReader dr = readingCom.ExecuteReader();
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    label1.Text = Convert.ToString(dr.GetValue(0));

                }
                dr.Close();
            }
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            
            if (!isPremium)
            {
                MessageBox.Show("You're Already A premium member", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
               // groupBox1.Visible = true;
            }
            else
            {
                this.Hide();
                PaymentViaMobileBanking pmb = new PaymentViaMobileBanking();
                pmb.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Not Yet Completed");
            this.Hide();
            Myproject.UserEditProfile ep= new Myproject.UserEditProfile();
            ep.Show();
        }
        private void button4_Click_2(object sender, EventArgs e)
        {
            this.Hide();
            TalkToSkinSpecialist form2_1 = new TalkToSkinSpecialist();
            form2_1.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Gynecologists form3 = new Gynecologists();
            form3.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            this.Hide();
            Medicine form4 = new Medicine();
            form4.Show();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            MessageDashBoard msdb = new MessageDashBoard();
            msdb.Tracker();
            msdb.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrontPage f1 = new FrontPage();
            f1.Show();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            Chatting chtt = new Chatting();
            chtt.trackingSpecialDoc();
            if (comboBox1.Text != ""&& comboBox1.Text== "Skin Problem")
            {
                this.Hide();
                TalkToSkinSpecialist derma = new TalkToSkinSpecialist();
                derma.Show();
            }
            else if (comboBox1.Text != "" && comboBox1.Text == "Medicine")
            {
                this.Hide();
                Medicine med = new Medicine();
                med.Show();                
            }
            else if (comboBox1.Text != "" && comboBox1.Text == "Gynecology")
            {
                this.Hide();
                Gynecologists gyno = new Gynecologists();
                gyno.Show();
            }
            else
            {
                MessageBox.Show("Select From the Drop down!");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_3(object sender, EventArgs e)
        {
            this.Hide();
            Chatting chTrack = new Chatting();
            chTrack.trackingDoc();         
            TalkToDoctor tlk = new TalkToDoctor();
            tlk.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            SignIn f25 = new SignIn();
            f25.Show();

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
  }

