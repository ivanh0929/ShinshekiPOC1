using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShinshekiPOC1
{
    internal class Cutscene
    {
        List<Line> lines;
        Dictionary<int, string[]> commentary;
        int commentcount = 0;
        TimeOfDay timeOfDay;
        Weekday weekday;
        Month month;
        int Date;
        string location;

        public Cutscene(
            List<Line> lines, 
            Dictionary<int, string[]> commentary, 
            Weekday weekday, 
            Month month, 
            int date, 
            string location, 
            TimeOfDay timeOfDay)
        {
            this.lines = lines;
            this.commentary = commentary;
            this.timeOfDay = timeOfDay;
            this.month = month;
            this.Date = date;
            this.weekday = weekday;
            this.location = location;
        }
        public void PlayCutscene()
        {
            for (int i = 0; i < lines.Count; i++)
            {
                Console.WriteLine(""+weekday.ToString()+" "+ month.ToString() + " " + Date + " - " + timeOfDay.ToString() + "\n"+location);
                Console.WriteLine(lines[i].ToString());
                if (lines[i].isComment == true)
                {
                    lines[i].Comment(commentary[commentcount]);
                    commentcount = commentcount + 1;
                }
                else
                {
                    Console.ReadLine();
                }
                Console.Clear();
            }
        }


    }
}
