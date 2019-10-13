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
        int counter3;
        int countPrimerTrue=0;
        int countPrimerFalse = 0;
        public Form1()
        {
            InitializeComponent();
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            label5.Text = "";
            label3.Text = "";
            label6.Text = "";
            label7.Text = $"Количество правильно решенных примеров:{countPrimerTrue}\n" +
                    $"Количество ошибочных:{countPrimerFalse}";


            for (int i = 10; i<=100;i++) {
                listBox1.Items.Add(i);
            }
            maxDigit = 10;// new Random().Next(10, 100);
            listBox1.SelectedItem =maxDigit;
            GenerationRandom();
            counter = 0;
            timerPrimer.Interval = 1000;
            timerPrimer.Enabled = true;
            timerNextPrimerZadergka.Enabled = false;

            //***********
            counter3 = 0;
            timerPrilozenie.Interval = 1000;
            timerPrilozenie.Enabled = true;
            timerPrilozenie.Start();
            // Hook up timer's tick event handler.  
            this.timerPrilozenie.Tick += new System.EventHandler(this.TimerPrilozenie_Tick);

        }

        private void CheckEnter(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //Console.WriteLine(e.KeyChar);

            if (e.KeyChar == (char)13)
            {

                this.ProverkaOtveta();

            }


        }



        void ProverkaOtveta()
        {
            result = Convert.ToInt32(textBox1.Text) + Convert.ToInt32(textBox2.Text);
            if (Convert.ToInt32(textBox3.Text) == result)
            {
                label3.Text = "Правильно!";
                countPrimerTrue++;
                label7.Text = $"Количество правильно решенных примеров:{countPrimerTrue}\n" +
                    $"Количество ошибочных:{countPrimerFalse}";
                this.label3.ForeColor = System.Drawing.Color.Green;
                timerPrimer.Stop();
                timerPrimer.Enabled = false;
                counter = 0;
                counter2 = 0;
                timerNextPrimerZadergka.Enabled = true;
                timerNextPrimerZadergka.Start();
                
            }
            else
            {
                label3.Text = "Не правильно!";
                countPrimerFalse++;
                this.label3.ForeColor = System.Drawing.Color.Red;
                textBox3.Text = "";
                label7.Text = $"Количество правильно решенных примеров:{countPrimerTrue}\n" +
                    $"Количество ошибочных:{countPrimerFalse}";
               // counter2 = 0;
               // timerNextPrimerZadergka.Enabled = true;
                //timerNextPrimerZadergka.Start();
            }


        }

        void GenerationRandom()
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
            TimerPrimerStart();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            TimerPrimerStart();
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            TimerPrimerStart();
        }


        private void InitializeTimer()
        {
            // Run this procedure in an appropriate event.  
            counter = 0;
            timerPrimer.Interval = 1000;
            timerPrimer.Enabled = true;
            counter2 = 0;
            timerNextPrimerZadergka.Interval = 1000;
            timerNextPrimerZadergka.Enabled = false;
            // Hook up timer's tick event handler.  
            this.timerPrimer.Tick += new System.EventHandler(this.TimerPrimerStart_Tick);
            this.timerNextPrimerZadergka.Tick += new System.EventHandler(this.TimerPrimerNextZadergka_Tick);

        }
        private void TimerPrimerStart_Tick(object sender, EventArgs e)
        {
            counter++;
            label5.Text = $"Время от начала задачи:{counter} сек.";
        }

        void TimerPrimerStart() {
            GenerationRandom();
            textBox3.Focus();
            counter = 0;
           
            timerPrimer.Interval = 1000;
            timerPrimer.Enabled = true;
            timerPrimer.Start();
            //InitializeTimer();

        }

        private void TimerPrimerNextZadergka_Tick(object sender, EventArgs e)
        {
            counter2 = counter2 + 1;
            if (counter2 > 5) {
                label5.Text = $"Новая задача!";
                timerNextPrimerZadergka.Stop();
                timerNextPrimerZadergka.Enabled = false;
                counter2 = 0;
                TimerPrimerStart();
            }
        }
        int minute;
        private void TimerPrilozenie_Tick(object sender, EventArgs e)
        {
            counter3++;
            minute = counter3 / 60/2;
            label6.Text = $"Общее время:{minute} мин. {counter3/2%60} сек.";
        }
    }
}
