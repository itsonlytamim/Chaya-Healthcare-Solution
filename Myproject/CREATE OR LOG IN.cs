using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Myproject
{
    public partial class FrontPage : Form
    {
        public FrontPage()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 u = new Form2();
            u.Show();
                

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignIn f1 = new SignIn();
            f1.Show();
            

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void FrontPage_Load(object sender, EventArgs e)
        {

        }
    }
}
