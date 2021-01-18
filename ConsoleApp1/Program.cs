using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    class Program
    {
        #region Functions
        static bool CheckPalind(String str)
        {
            for (int i = 0; i <= str.Length / 2; i++)
                if (str[i] != str[str.Length - 1 - i]) //comparing one character from beginning one from end of string
                    return false;
            return true;
        }
        static int GCD(int p, int q)//find greatest common divisor
        {
            if (q == 0)
            {
                return p;
            }

            int r = p % q;

            return GCD(q, r);
        }
        static string  FindRatio(int num1, int num2)
        {

            string oran = "";
            int gcd;
            int quotient = 0;
            while (num1 >= num2)
            {
                num1 = num1 - num2;
                quotient++;
            }

            gcd = GCD(num1, num2);

            //without using division finding ration of num1 i1
            int i1 = 1;
            while (gcd*i1 != num1)
            {
                i1++;
            }
            //without using division finding ration of num1 i2
            int i2 = 1;
            while (gcd * i2 != num2)
            {
                i2++;
            }

            oran = string.Concat(quotient, " ", i1,"/",i2);
            return oran;
        }
        #endregion


        static void Main(string[] args)
        {

            var text = File.ReadAllText("file.txt").ToLower();
            //regex for words
            var match = Regex.Match(text, @"(\w+)");

            //to keep word and its frequency
            Dictionary<string, int> freq = new Dictionary<string, int>();

            //to keep palendrom words and their freq
            Dictionary<string, int> palendroms = new Dictionary<string, int>();

            //fill the freq dictionary
            while (match.Success)
            {
                string word = match.Value;
                if (freq.ContainsKey(word))
                {
                    freq[word]++;
                }
                else
                {
                    freq.Add(word, 1);
                }

                match = match.NextMatch();
            }

            /*----------#1 Print most occured 3.word -------*/
            Console.WriteLine("Rank. Word-Frequency, most occured 3.word");
            int rank = 1;
            foreach (var elem in freq.OrderByDescending(a => a.Value).Take(3))
            {
                if (rank == 3)
                    Console.WriteLine("#{0}.  {1} - {2} times", rank, elem.Key, elem.Value);
                rank++;
            }
            Console.WriteLine(); Console.WriteLine();

            /*----------#2 find palendrom word and print them if their count is bigger than 3--------*/
            Console.WriteLine("Palendrom word with their frequency");
            foreach (var elem in freq)
            {
                if (CheckPalind(elem.Key))
                {
                    palendroms.Add(elem.Key, elem.Value);
                }
            }

            if (palendroms.Count > 3)
            {
                foreach (var elem in palendroms)
                {
                    Console.WriteLine("{0} - {1}",elem.Value, elem.Key);
                }
            }
            Console.WriteLine(); Console.WriteLine();

            /*----------#3 ration of palendrom strings in txt without using divsion--------*/
            Console.WriteLine("Ration of palendrom strings in txt");
            Console.WriteLine("{0}({1}/{2}) : {3}", "Ration of ",palendroms.Count, freq.Count,FindRatio(palendroms.Count, freq.Count));

            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
