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
using UserPart;
using Project;

namespace Myproject
{
    public partial class TalkToDoctor : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public TalkToDoctor()
        {
            InitializeComponent();
            BindDoctorGridView();
        }
        void BindDoctorGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            // string query = "select * from MEMBER_DETAILS where email_addr=@email_addr";
            SqlCommand cmd = new SqlCommand("select name,dob,pro_picture from DOCTOR_DETAILS", con);
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

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserHomePage hpg = new UserHomePage();
            hpg.Show();
        }

        private void button9_Click(object sender, EventArgs e)
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
