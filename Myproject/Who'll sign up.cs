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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrontPage f3 = new FrontPage();
            f3.Show();
            
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            
            if(radioButton1.Checked==true)
            {
                this.Hide();
                 MemberSignUp f3 = new MemberSignUp();
                 f3.Show();

            }
            else if (radioButton2.Checked == true)
            {
                this.Hide();
                Form4 f3 = new Form4(radioButton2.Text);
                f3.Show();

            }

            else if (radioButton3.Checked == true)
            {
                this.Hide();
                Form4 f3 = new Form4(radioButton3.Text);
                f3.Show();

            }




        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }
    }
}
