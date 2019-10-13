using System;
using System.Timers;
using System.Windows.Forms;

namespace Calculator
{



    public partial class Form1 : Form
    {
        int result;
        Random random1;
        Random random2;
        int rnd;
        int maxDigit;
        int counter;
        int counter2;
        public Form1()
        {
            InitializeComponent();
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            label5.Text = "";
            label3.Text = "";
            
            for (int i = 10; i<=100;i++) {
                listBox1.Items.Add(i);
            }
            maxDigit = 10;// new Random().Next(10, 100);
            listBox1.SelectedItem =maxDigit;
            Method2();
            counter = 0;
            timer1.Interval = 1000;
            timer1.Enabled = true;
            timer2.Enabled = false;
            // InitializeTimer();
        }

        private void CheckEnter(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //Console.WriteLine(e.KeyChar);

            if (e.KeyChar == (char)13)
            {

                this.Method();

            }


        }



        void Method()
        {
            result = Convert.ToInt32(textBox1.Text) + Convert.ToInt32(textBox2.Text);
            if (Convert.ToInt32(textBox3.Text) == result)
            {
                label3.Text = "Правильно!";
                this.label3.ForeColor = System.Drawing.Color.Green;
                timer1.Stop();
                timer1.Enabled = false;
                counter = 0;
                counter2 = 0;
                timer2.Enabled = true;
                timer2.Start();
                
            }
            else
            {
                label3.Text = "Не правильно!";
                this.label3.ForeColor = System.Drawing.Color.Red;
                textBox3.Text = "";
            }


        }

        void Method2()
        {
            textBox3.Text = "";
            label3.Text = "";
            maxDigit = Convert.ToInt32(listBox1.SelectedItem);
            random1 = new Random();
            rnd = random1.Next(1, maxDigit+1);
            textBox1.Text = Convert.ToString(rnd);
            random2 = new Random();
            rnd = random1.Next(1, maxDigit+1);
            textBox2.Text = Convert.ToString(rnd);
            


        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            Method3();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            Method3();
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            Method3();
        }


        private void InitializeTimer()
        {
            // Run this procedure in an appropriate event.  
            counter = 0;
            timer1.Interval = 1000;
            timer1.Enabled = true;
            counter2 = 0;
            timer2.Interval = 1000;
            timer2.Enabled = false;
            // Hook up timer's tick event handler.  
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            counter = counter + 1;
            label5.Text = $"Время от начала задачи:\n{counter} сек.";
        }

        void Method3() {
            Method2();
            textBox3.Focus();
            counter = 0;
           
            timer1.Interval = 1000;
            timer1.Enabled = true;
            timer1.Start();
            //InitializeTimer();

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            counter2 = counter2 + 1;
            if (counter2 > 20) {
                label5.Text = $"Новая задача!";
                timer2.Stop();
                timer2.Enabled = false;
                counter2 = 0;
                Method3();
            }
        }
    }
}
