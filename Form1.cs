using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        int result;
        public Form1()
        {
            InitializeComponent();
        }

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label3.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label3.Text = "";
        }

        private void CheckEnter(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //Console.WriteLine(e.KeyChar);
            
            if (e.KeyChar == (char)13)
            {
                
                this.Method();

            }


        }

       

        void Method() {
            result = Convert.ToInt32(textBox1.Text) + Convert.ToInt32(textBox2.Text);
            if (Convert.ToInt32(textBox3.Text) == result)
            {
                label3.Text = "Правильно!";
                this.label3.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                label3.Text = "Не правильно!";
                this.label3.ForeColor = System.Drawing.Color.Red;
                textBox3.Text = "";
            }


        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            label3.Text = "";
        }
    }
}
