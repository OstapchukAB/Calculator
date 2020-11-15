using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class myGenaration
    {
        private int x;
        private int y;
        private string znak;
       // private int z;

        private myGenaration ob;

        private int maxDigit;
        private HashSet<myGenaration> mySet;

        public myGenaration(int a, int b)
        {
            this.x = a;
            this.y = b;
        }
        public myGenaration(string znak, int maxDigit)
        {
            mySet = new HashSet<myGenaration>();
            this.znak = znak;
            this.maxDigit = maxDigit;

            GenerationRandom();



        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public HashSet<myGenaration> MySet { get => mySet; set => mySet = value; }

        public bool Proverka(int z)
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
 
            if (znak.Equals("/"))
            {
                do
                {
                    x = random.Next(2, maxDigit * 10 + 1);
                    y = random.Next(2, maxDigit + 1);
                    

                } while ( y >= x || x % y > 0 || x / y > 10 || x>10*y);
            }
            else if (znak.Equals("-") || znak.Equals("+"))
            {
                do
                {
                    x = random.Next(2, maxDigit * 10 + 1);
                    y = random.Next(2, maxDigit + 1);
                   

                } while ( x <= y);
                
            }
            else
            {
                
                    y = random.Next(2, maxDigit + 1);
                    x = random.Next(2, maxDigit + 1);

            }
            if ((znak.Equals("*") || znak.Equals("/")) && mySet.Count == Convert.ToInt32(Math.Pow(maxDigit-1, 2))) {

                mySet.Clear();
            }

            



            ob = new myGenaration(x, y);
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
            //PrintHashSet();
            return r;
        }

        public void PrintHashSet( ) {
            Console.WriteLine("-*************----");
            Console.WriteLine($"Count=\t{MySet.Count()}");
            foreach (var valu in MySet)
            {
                Console.WriteLine($"X=\t{valu.X}, Y= \t{valu.Y}");
            }

        }

        public override bool Equals(object obj)
        {
            return obj is myGenaration @class &&
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
