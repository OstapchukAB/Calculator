﻿using System;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace Calculator
{



    public partial class Form1 : Form
    {
        int result;
        Random random;
        SoundPlayer spFalse;
        SoundPlayer spTrue;

        int rnd;
        int rnd2;
        int maxDigit;
        int counter2;
        int counter3;
        int counterLast=0;
        int countPrimerTrue = 0;
        int countPrimerFalse = 0;
        int timeMax = 180;//сек
        bool lastPrimer = false;
        string znak="";
        public Form1()
        {
            InitializeComponent();
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            String fullAppName = Application.ExecutablePath;
            String fullAppPath = Path.GetDirectoryName(fullAppName);
            String fullFileNameFalse = Path.Combine(fullAppPath, "CriticalStop.wav");
            String fullFileNameTrue = Path.Combine(fullAppPath, "Ring01.wav");
            spTrue = new SoundPlayer(fullFileNameTrue);
            spFalse = new SoundPlayer(fullFileNameFalse);
            random = new Random();
            textBox3.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            label5.Text = "";
            label3.Text = "";
            label6.Text = "";
            label7.Text = $"Количество правильно решенных примеров:{countPrimerTrue}\n" +
                    $"Количество ошибочных:{countPrimerFalse}";


            for (int i = 10; i <= 100; i += 10)
            {
                if (i % 20 == 0 || i == 10)
                    listBox1.Items.Add(i);
            }
            maxDigit = 10;// new Random().Next(10, 100);
            listBox1.SelectedItem = maxDigit;

            counter3 = 0;
            timerPrilozenie.Interval = 1000;
            timerPrilozenie.Tick += new EventHandler(TimerPrilozenie_Tick);


        }

        private void CheckEnter(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)13)
            {

                this.ProverkaOtveta();
                e.Handled = true;
                //e.Supr.SuppressKeyPress = true;

            }


        }



        void ProverkaOtveta()
        {
            if (znak.Equals("+")) {
                result = Convert.ToInt32(textBox1.Text) + Convert.ToInt32(textBox2.Text);
            }
            else {
                result = Convert.ToInt32(textBox1.Text) - Convert.ToInt32(textBox2.Text);
            }
            if (Convert.ToInt32(textBox3.Text) == result)
            {
                lastPrimer = true;
                label3.Text = "Правильно!";
                countPrimerTrue++;
                this.label3.ForeColor = System.Drawing.Color.Green;
                counter2 = counter3;
                //spTrue.Play();


            }
            else
            {
                lastPrimer = false;
                label3.Text = "Не правильно!";
                countPrimerFalse++;
                this.label3.ForeColor = System.Drawing.Color.Red;
                textBox3.Text = "";
                spFalse.Play();

            }
            label7.Text = $"Количество правильно решенных примеров:{countPrimerTrue}\n" +
                   $"Количество ошибочных:{countPrimerFalse}";

            


        }

        void GenerationRandom()
        {
            textBox3.Text = "";
            label3.Text = "";
            maxDigit = Convert.ToInt32(listBox1.SelectedItem);

            znak = (random.Next(0, 99) % 2 == 0) ? "+" : znak = "-";
            label1.Text = znak;
          
            rnd2 = random.Next(1, maxDigit + 1);
            textBox2.Text = Convert.ToString(rnd2);


            if (znak.Equals("-"))
            {
                do
                {
                    rnd = random.Next(1, maxDigit + 1);
                    textBox1.Text = Convert.ToString(rnd);
                } while (rnd2 > rnd);
            }
            else {
                rnd = random.Next(1, maxDigit + 1);
                textBox1.Text = Convert.ToString(rnd);
            }



            textBox3.Focus();
            counter2 = counter3;
        }


        private void TimerPrilozenie_Tick(object sender, EventArgs e)
        {
            if (lastPrimer & counterLast<=counter3) { 
                counterLast = counter3+2;
                GenerationRandom();
                lastPrimer = false;
            }
            
            if (counter3 >= timeMax)
            {

                timerPrilozenie.Enabled = false;
                textBox3.Enabled = false;
                this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F, System.Drawing.FontStyle.Bold);
                this.label3.ForeColor = System.Drawing.Color.Black;
                label3.Text = "Задание закончено!";
                listBox1.Enabled = false;
                button1.Enabled = true;
                spTrue.Play();

            }
            else {
                counter3 +=1;
            }
            label5.Text = $"Время задачи:{counter3 - counter2}";
            label6.Text = $"Общее время:{counter3 / 60} мин. {counter3 % 60} сек.";

        }



        private void button1_Click(object sender, EventArgs e)
        {
            counter2=0;
            counterLast = 0;
            countPrimerTrue = 0;
            countPrimerFalse = 0;
            counter3 = 0;
            listBox1.Enabled = false;
            textBox3.Enabled = true;
            button1.Enabled = false;
            
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold);
            label7.Text = $"Количество правильно решенных примеров:{countPrimerTrue}\n" +
                    $"Количество ошибочных:{countPrimerFalse}";
            timerPrilozenie.Enabled = true;
            GenerationRandom();
            
        }
    }
}
