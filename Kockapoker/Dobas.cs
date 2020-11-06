using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kockapoker
{
    class Dobas
    {
        int[] kockak = new int[5];
        private string eredmeny;
        private int pont;
        public string Eredmeny
        {
            get
            {
                return eredmeny;
            }
        }
        public int Pont { get { return pont; } }
        public Dobas()
        {

        }
        public Dobas(int k1, int k2, int k3, int k4, int k5)
        {
            kockak[0] = k1;
            kockak[1] = k2;
            kockak[2] = k3;
            kockak[3] = k4;
            kockak[4] = k5;
            eredmeny = Erteke();
        }
        public void EgyDobas()
        {
            Random vel = new Random();
            for (int i = 0; i < kockak.Length; i++)
            {
                kockak[i] = vel.Next(1, 7);
            }
            eredmeny = Erteke();
        }

        public void Kiiras()
        {
            foreach (var i in kockak)
            {
                Console.Write($"{i}");
            }
            Console.WriteLine($"-> {eredmeny}");
        }

        public string Erteke()
        {
            Dictionary<int, int> eredmeny = new Dictionary<int, int>();
            for (int i = 1; i <= 6; i++)
            {
                eredmeny.Add(i, 0);
            }
            foreach (var i in kockak)
            {
                eredmeny[i]++;
            }
            /*A dicból lekérjük az 1 Valuenal nagyobb elemeket!
             * Első eset ha egy elem marad: 5-nagypoker, 4-poker, 3-drill, 2-pár
             * Hanyas (Key) érték határozza meg pl 4 es póker
             * Második eset 2 elem marad
             * Value 3 és 2 --> Full
             * Value 2 és 2 --> Két pár
             * Harmadik eset nem marad egy sem
             * Ha key 6 == 0 kis sor vagy key 1 == 0 nagy sor
             * Minden más --> szemét
             */
            var result = (from e in eredmeny
                          where e.Value > 1
                          orderby e.Value descending
                          select new { Szam = e.Key, Db = e.Value }).ToList();
            Console.WriteLine();
            int db = result.Count;
            if (db == 1)
            {
                switch (result[0].Db)
                {
                    case 5:

                        return $" {result[0].Szam} Nagypóker";
                    case 4:
                        return $"{result[0].Szam} Póker";
                    case 3:
                        return $"{result[0].Szam} Drill";
                    case 2:
                        return $"{result[0].Szam} Pár";
                    default:
                        break;
                }
            }
            //vagy 
            if (db == 1)
            {
                string[] egyes = new string[] { "", "", "Pár", "Drill", "Póker", "Nagypóker" };
                int[] pontok = new int[] { 0, 0, 10, 300, 600, 900 };
                pont = pontok[result[0].Db] + result[0].Szam;

                return $"{result[0].Szam} {egyes[result[0].Db]}";
            }
            else if (db == 2)
            {
                if (result[0].Db == 3 && result[1].Db == 2)
                {
                    if (result[0].Szam > result[1].Szam)
                    {
                        return $"{result[0].Szam}-{result[1].Szam} Full";
                    }
                    else
                    {
                        return $"{result[1].Szam}-{result[0].Szam} Full";
                    }
                }
                else
                {
                    if (result[0].Szam > result[1].Szam)
                    {
                        return $"{result[0].Szam}-{result[1].Szam} Pár";
                    }
                    else
                    {
                        return $"{result[1].Szam}-{result[0].Szam} Pár";
                    }
                }
            }
            else
            {
                if (eredmeny[6] == 0)
                {
                    return "Kis sor";
                }
                else if (eredmeny[1] == 0)
                {
                    return "Nagy sor";
                }
            }
             

            return "Szemét";
        }

        
    }
}
