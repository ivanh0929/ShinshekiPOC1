using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShinshekiPOC1
{
    internal class Line
    {
        string charaname;
        EmotionState emotionState;
        string dialogue;
        bool comment;
        public bool isComment { get { return comment; }}
        public Line(string charaname, EmotionState emotionState, string dialogue, bool comment)
        {
            this.charaname = charaname;
            this.emotionState = emotionState;
            this.dialogue = dialogue;
            this.comment = comment;
        }
        public override string ToString()
        {
            string emo;

            switch(emotionState)
            {
                case EmotionState.None:
                    emo = "";
                    break;
                default:
                    emo = "("+emotionState.ToString()+")";
                    break;

            }
            string display = ""+charaname+" " + emo + ": " + dialogue;

            return display;
        }

        public void Comment(string[] commentary)
        {
            int choice = 0;
            switch (commentary.Length)
            {
                case 2:
                    Console.WriteLine(commentary[0]);
                    Console.WriteLine(commentary[1]);
                    Console.WriteLine("Choose the dialogue option you want by number and press enter.");
                    choice = CV.CVNumber("Please enter 1.");
                    Console.WriteLine(commentary[choice - 1]);
                    break;
                case 4:
                    Console.WriteLine(commentary[0]);
                    Console.WriteLine(commentary[1]);
                    choice = CV.CVNumber("Please enter a valid integer between 1 and 2.");
                    Console.WriteLine(commentary[choice - 1]);
                    break;
                case 6:
                    Console.WriteLine(commentary[0]);
                    Console.WriteLine(commentary[1]);
                    Console.WriteLine(commentary[2]);
                    choice = CV.CVNumber("Please enter a valid integer between 1 and 3.");
                    switch(choice)
                    {
                        case 1:
                            Console.WriteLine(commentary[3]);
                            Console.ReadLine();
                            break;
                        case 2:
                            Console.WriteLine(commentary[4]);
                            Console.ReadLine();
                            break;
                        case 3:
                            Console.WriteLine(commentary[5]);
                            Console.ReadLine();
                            break;
                    }
                    break;
            }
        }

    }

}
