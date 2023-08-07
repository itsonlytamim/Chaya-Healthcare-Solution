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
    public partial class UserEditProfile : Form
    {        
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public UserEditProfile()
        {
            InitializeComponent();
            BindGridView();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
           // string query = "select * from MEMBER_DETAILS where email_addr=@email_addr";
            SqlCommand cmd = new SqlCommand("select name,age,addr,mobile_number,picture from MEMBER_DETAILS where email_addr=@email_addr", con);
            cmd.Parameters.AddWithValue("@email_addr",SignIn.emailAdd);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;
            DataGridViewImageColumn dgvic = new DataGridViewImageColumn();
            dgvic = (DataGridViewImageColumn)dataGridView1.Columns[4];
            dgvic.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 100;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Myproject.UserHomePage homepg = new Myproject.UserHomePage();
            homepg.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrontPage f1 = new FrontPage();
            f1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ResetControl();
        }
        void ResetControl()
        {
            textBox1.Clear();
            numericUpDown1.Value = 0;
            textBox2.Clear();
            textBox4.Clear();         
            pictureBox1.Image = Properties.Resources.No_Image_Available;
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = (string)dataGridView1.SelectedRows[0].Cells[0].Value;
            numericUpDown1.Value = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value);
            textBox2.Text = (string)dataGridView1.SelectedRows[0].Cells[2].Value;
            textBox4.Text= dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            pictureBox1.Image = GetPhoto((byte[])dataGridView1.SelectedRows[0].Cells[4].Value);
        }
        private Image GetPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }
        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd = new SqlCommand("update MEMBER_DETAILS set NAME=@NAME,AGE=@AGE,ADDR=@ADDR,MOBILE_NUMBER=@MOBILE_NUMBER,PICTURE=@PICTURE where email_addr=@email_addr", con);
            cmd.Parameters.AddWithValue("@email_addr", SignIn.emailAdd);
            cmd.Parameters.AddWithValue("@NAME", textBox1.Text);
            cmd.Parameters.AddWithValue("@AGE", numericUpDown1.Value);
            cmd.Parameters.AddWithValue("@ADDR", textBox2.Text);
            cmd.Parameters.AddWithValue("@mobile_number", textBox4.Text);
            cmd.Parameters.AddWithValue("@PICTURE", SavePhoto());
            
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Data Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindGridView();
                ResetControl();
            }
            else
            {
                MessageBox.Show("Data not Updated", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Image";
            ofd.Filter = "Image FILE(All files)*.*|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(ofd.FileName);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete from MEMBER_DETAILS where email_addr=@email_addr", con);
            cmd.Parameters.AddWithValue("@email_addr", SignIn.emailAdd);

            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Account Deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                FrontPage fp = new FrontPage();
                fp.Show();
              //  BindGridView();
               // ResetControl();
            }
            else
            {
                MessageBox.Show("Account Not Deleted", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditProfile_Load(object sender, EventArgs e)
        {

        }
    }
}
