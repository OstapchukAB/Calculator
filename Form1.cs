//using Iesi.Collections.Generic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace Calculator
{
   


    public partial class Form1 : Form
    {
  
        
        SoundPlayer spFalse;
        SoundPlayer spTrue;


        int maxDigit;
        int counter2;
        int counter3;
        int counterLast=0;
        int countPrimerTrue = 0;
        int countPrimerFalse = 0;
        int timeMax = 30;//сек
        bool lastPrimer = false;
        myClass o;
      
        public Form1()
        {
            InitializeComponent();
        }




        private void Form1_Load(object sender, EventArgs e)
        {

            loads();
        }


        private void loads() {


            String fullAppName = Application.ExecutablePath;
            String fullAppPath = Path.GetDirectoryName(fullAppName);
            String fullFileNameFalse = Path.Combine(fullAppPath, "CriticalStop.wav");
            String fullFileNameTrue = Path.Combine(fullAppPath, "Ring01.wav");
            spTrue = new SoundPlayer(fullFileNameTrue);
            spFalse = new SoundPlayer(fullFileNameFalse);
        
            textBox3.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            label5.Text = "";
            label3.Text = "";
            label6.Text = "";
            label7.Text = $"Количество правильно решенных примеров:{countPrimerTrue}\n" +
                    $"Количество ошибочных:{countPrimerFalse}";
            comboBox1.Items.Add("+");
            comboBox1.Items.Add("-");
            comboBox1.Items.Add("*");
            comboBox1.Items.Add("/");


            for (int i = 2; i <= 9; i += 1)
            {
                comboBox2.Items.Add(i);
            }
            maxDigit = 10;// new Random().Next(10, 100);
            comboBox2.SelectedItem = maxDigit;


            counter3 = 0;
            timerPrilozenie.Interval = 500;
            timerPrilozenie.Tick += new EventHandler(TimerPrilozenie_Tick);



        }

        private void CheckEnter(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)13)
            {

                int i;
                if (int.TryParse(textBox3.Text, out i))
                {
  
                    if (o.Proverka(Convert.ToInt32(textBox3.Text)))
                    {
                        lastPrimer = true;
                        label3.Text = "Правильно!";
                        countPrimerTrue++;
                        this.label3.ForeColor = System.Drawing.Color.Green;
                        counter2 = counter3;
                        do {; }while (o.GenerationRandom());
                        textBox2.Text = Convert.ToString(o.Rnd2);
                        textBox1.Text = Convert.ToString(o.Rnd);
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
                   
                   

                    textBox3.Text = "";
                    textBox1.Refresh();
                    textBox2.Refresh();
                    textBox3.Refresh();
                    textBox3.Focus();
                }
                else {

                  
                    textBox3.Text = "";

                    textBox3.Refresh();
                    textBox3.Focus();
                }
               
                e.Handled = true;
                //e.Supr.SuppressKeyPress = true;
            }
        }

        

        private void TimerPrilozenie_Tick(object sender, EventArgs e)
        {
            
            
            if (lastPrimer & counterLast<=counter3) { 
                counterLast = counter3+2;
                
                textBox3.Text = "";
                label3.Text = "";
                counter2 = counter3;
                lastPrimer = false;
            }
            
            if (counter3/2 >= timeMax)
            {

                timerPrilozenie.Enabled = false;
                textBox3.Enabled = false;
                this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F, System.Drawing.FontStyle.Bold);
                this.label3.ForeColor = System.Drawing.Color.Black;
                label3.Text = "Задание закончено!";
                comboBox2.Enabled = false;
                button1.Focus();
               // button1.Enabled = true;
                spTrue.Play();
                foreach (var valu in o.MySet)
                {
                    Console.Write(valu.X); Console.Write(" "); Console.WriteLine(valu.Y);
                }
            }
            else {
                counter3 +=1;
            }
            label5.Text = $"Время задачи:{counter3/2 - counter2/2}";
            label6.Text = $"Общее время:{counter3 /2/ 60} мин. {counter3/2 % 60} сек.";

        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0 || comboBox2.SelectedIndex < 0) {
                MessageBox.Show("выберите начальные значения"); 
                return;
            }
            
            comboBox2.Visible = false;  
            comboBox1.Visible = false;  
            counter2=0;
            counterLast = 0;
            countPrimerTrue = 0;
            countPrimerFalse = 0;
            counter3 = 0;
            comboBox2.Enabled = false;
            textBox3.Enabled = true;
            button1.Enabled = false;
            
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold);
            label7.Text = $"Количество правильно решенных примеров:{countPrimerTrue}\n" +
                    $"Количество ошибочных:{countPrimerFalse}";
            timerPrilozenie.Enabled = true;

            o = new myClass(comboBox1.SelectedItem.ToString(), Convert.ToInt32(comboBox2.SelectedItem));
            label1.Text = comboBox1.SelectedItem.ToString();
            textBox2.Text = Convert.ToString(o.Rnd2);
            textBox1.Text = Convert.ToString(o.Rnd);
            maxDigit = Convert.ToInt32(comboBox2.SelectedItem);
            textBox3.Focus();
            counter2 = counter3;


        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }





        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            var result = MessageBox.Show("Сбросить программу?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Restart();
                //Environment.Exit(0);
            }
           
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }


    public class myClass
    {
        private int x;
        private int y;
        private string znak;
        private int z;
        private int rnd;
        private int rnd2;
        private myClass ob;

        private int maxDigit;
        private HashSet<myClass> mySet; 

        public myClass(int a, int b) {
            this.x = a;
            this.y = b;
        }
        public myClass(string znak, int maxDigit) {
            mySet = new HashSet<myClass>();
            this.znak = znak;
            this.maxDigit = maxDigit;
            
            GenerationRandom();



        }

        public int Rnd { get => rnd; set => rnd = value; }
        public int Rnd2 { get => rnd2; set => rnd2 = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public HashSet<myClass> MySet { get => mySet; set => mySet = value; }

        public  bool Proverka(int z)
        {
            int rez = 0;
            if (znak.Equals("+"))
            {
                rez = x + y;
            }
            else if (znak.Equals("-"))
            {
                rez = x - y;
            }
            else if (znak.Equals("*"))
            {
                rez = x * y;
            }
            else if (znak.Equals("/"))
            {
                if (y == 0) return false;
                rez = x / y;
            }

            return rez == z;

        }

        public bool GenerationRandom()
           
        {

            Random random = new Random();
            rnd = random.Next(1, maxDigit + 1);

            if (znak.Equals("/"))
            {
                do
                {
                    rnd2 = random.Next(1, maxDigit + 1);
                    rnd = random.Next(1, maxDigit * 10 + 1);

                } while (rnd * rnd2 == 0 || rnd == 1 || rnd2 == 1 || rnd2 >= rnd || rnd % rnd2 > 0 || rnd / rnd2 > 10);
            }
            else if (znak.Equals("-"))
            {
                do
                {
                    rnd2 = random.Next(1, maxDigit + 1);
                    rnd = random.Next(1, maxDigit * 10 + 1);

                } while (rnd * rnd2 == 0 || rnd == 1 || rnd2 == 1 || rnd <= rnd2);
            }
            else
            {
                do
                {
                    rnd2 = random.Next(1, maxDigit + 1);
                    rnd = random.Next(1, maxDigit + 1);

                } while (rnd * rnd2 == 0 || rnd == 1 || rnd2 == 1);
            }

            this.x = rnd;
            this.y = rnd2;
            ob = new myClass(x, y);
            bool r;
            if (mySet.Contains(ob))
            {
                r = true;
            }
            else 
            {
                r = false;
            }
            mySet.Add(ob);
            //Console.Write(ob.X);
            //Console.Write(" ");
            //Console.WriteLine(ob.Y);
            return r;
             }

        public override bool Equals(object obj)
        {
            return obj is myClass @class &&
                   X == @class.X &&
                   Y == @class.Y;
        }

        public override int GetHashCode()
        {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }
    }
}
