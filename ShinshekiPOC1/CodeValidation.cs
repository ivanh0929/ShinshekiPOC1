using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShinshekiPOC1
{
    internal class CV
    {

        // Check is a string is null or empty
        public static bool CVString(string entry, string error)
        {
            bool validity;
            if (String.IsNullOrEmpty(entry))
            {
                Console.WriteLine(error);
                validity = false;
            }
            else
            {
                validity = true;
            }
            return validity;
        }
        public static string CVString(string error)
        {
            string Answer;
            do
            {

                Answer = Console.ReadLine().Trim().ToUpper();

                if (String.IsNullOrEmpty(Answer))
                {
                    Console.WriteLine(error);
                }

            }
            while (String.IsNullOrEmpty(Answer));
            return Answer;
        }

        // Return a number from a string w/error checking
        public static int CVNumber(string error)
        {
            int realnum;
            bool parse;
            string valid;
            do
            {
                valid = Console.ReadLine().Trim();
                parse = int.TryParse(valid.Trim(), out realnum);
                if (parse == false)
                {
                    Console.WriteLine(error);
                }
            }
            while (realnum <= 0 || parse == false);
            return realnum;
        }

        // Return a number from a string w/error checking
        public static int CVNumberAndZero(string error)
        {
            int realnum;
            bool parse;
            string valid;
            do
            {
                valid = Console.ReadLine().Trim();
                parse = int.TryParse(valid.Trim(), out realnum);
                if (parse == false)
                {
                    Console.WriteLine(error);
                }
            }
            while (realnum <= -1 || parse == false);
            return realnum;
        }

        // Check for Y or N

        public static string CVYORN(string error)
        {
            string Answer;
            do
            {

                Answer = Console.ReadLine().Trim().ToUpper();

                if (String.IsNullOrEmpty(Answer))
                {
                    Console.WriteLine(error);
                }

                if (Answer.Substring(0) != "Y" & Answer.Substring(0) != "N")
                {
                    Console.WriteLine(error);
                }

            }
            while (String.IsNullOrEmpty(Answer) || Answer.Substring(0) != "Y" & Answer.Substring(0) != "N");
            return Answer;
        }

        public static List<string> ReadIn(string path)
        {
            List<string> list = new List<string>();
            StreamReader sr = new StreamReader(path);
            List<string> Values = new List<string>();
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                Values.Add(line);
            }
            sr.Close();
            return Values;
        }
    }


}
