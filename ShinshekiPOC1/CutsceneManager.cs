using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShinshekiPOC1
{
    internal class CutsceneManager
    {
        public static Dictionary<int, Cutscene> Cutscenes = new Dictionary<int, Cutscene>();

        public static void Init()
        {
            string[] cutscenedirs = Directory.GetFiles("Cutscenes/");
            string[] commentarydirs = Directory.GetFiles("Cutscene Commentary/");
            string cutscenetimesdir = "0.txt";
            string[,] times = GetTimes(cutscenetimesdir);
            List<Line> tempLines;
            Dictionary<int, string[]> tempComments;

            for (int i = 0; i < cutscenedirs.Length; i++)
            {
                tempLines = GetLines(cutscenedirs[i]);
                tempComments = GetCommentary(commentarydirs[i]);
                Cutscene cutscene = new Cutscene(
                    tempLines,
                    tempComments,
                    (Weekday)Enum.Parse(typeof(Weekday), times[i, 0]),
                    (Month)Enum.Parse(typeof(Month), times[i, 1]),
                    int.Parse(times[i, 2]),
                    times[i,3],
                    (TimeOfDay)Enum.Parse(typeof(TimeOfDay), times[i, 4]));

                Cutscenes[i] = cutscene;

            }
        }
        private static List<Line> GetLines(string dir)
        {
            StreamReader sr = new StreamReader(dir);
            List<string> Values = new List<string>();
            List<Line> lines = new List<Line>();
            EmotionState tempType;
            bool tempcomment;
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                Values.Add(line);
            }

            for(int i = 0; i < Values.Count; i++)
            {
                string[] split = Values[i].Split(",,");
                tempType = (EmotionState)Enum.Parse(typeof(EmotionState), split[1]);
                tempcomment = bool.Parse(split[3]);
                
                lines.Add(new Line(
                    split[0], 
                    tempType, 
                    split[2], 
                    tempcomment));
            }
            return lines;
        }

        private static Dictionary<int, string[]> GetCommentary(string dir)
        {
            StreamReader sr = new StreamReader(dir);
            List<string> Values = new List<string>();
            Dictionary<int, string[]> commentary = new Dictionary<int, string[]>();
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                Values.Add(line);
            }

            if(Values.Count > 0)
            {
                 for (int i = 0; i < Values.Count; i++)
                 {
                    string[] split = Values[i].Split(",,");
                    commentary[i] = split;

                 }
            }

            return commentary;
        }

        private static string[,] GetTimes(string dir)
        {
            StreamReader sr = new StreamReader(dir);
            List<string> Values = new List<string>();
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                Values.Add(line);
            }

            string[,] times = new string[Values.Count,5];

            for (int i = 0; i < Values.Count; i++)
            {
                string[] split = Values[i].Split(",,");
                times[i,0] = split[0];
                times[i,1] = split[1];
                times[i,2] = split[2];
                times[i,3] = split[3];
                times[i,4] = split[4];
            }
            return times;
        }

        public static void PlayAll()
        {
            for (int i = 0; i < Cutscenes.Count; i++)
            {
                Cutscenes[i].PlayCutscene();
            }
        }

        public static void PlaySpecific()
        {
            Console.WriteLine("Enter the number of the cutscene you want to play.");
            int choice = CV.CVNumber("Please enter a valid integer.");
            Cutscenes[choice].PlayCutscene();
        }

        public static void PlaySpecific(int num)
        {
            Cutscenes[num].PlayCutscene();
        }
    }
}
