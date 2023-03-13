using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShinshekiPOC1
{
    internal static class BattleManager
    {
        static Dictionary<string, Skill> skillList = new Dictionary<string, Skill>(); // All Skills

        public static Dictionary<string, Skill> SkillList
        {
            get { return skillList; }
            set { SkillList = value; }
        }

        public static void InitSkills()
        {
            //Load in all skills

            {
                try
                {
                    string[] temp = new string[7]; // 3, 4, 5, 6, and 9 need to be parsed as ints and 7 and 8 needs to be parsed as enum
                    int[] tempnums = new int[5];
                    ElementType tempType;
                    SkillType tempskillType;
                    StatusCond tempCond;

                    List<string> Values = CV.ReadIn("Skills/BasicSkills.csv");

                    for (int i = 0; i < Values.Count; i++)
                    {
                        temp = Values[i].Split(',');
                        tempnums[0] = int.Parse(temp[2]);
                        tempnums[1] = int.Parse(temp[3]);
                        tempnums[2] = int.Parse(temp[4]);
                        tempnums[3] = int.Parse(temp[5]);
                        tempType = (ElementType)Enum.Parse(typeof(ElementType), temp[6]);
                        tempskillType = (SkillType)Enum.Parse(typeof(SkillType), temp[7]);
                        tempCond = (StatusCond)Enum.Parse(typeof(StatusCond), temp[8]);
                        tempnums[4] = int.Parse(temp[4]);
                        skillList[temp[0]] = (new Skill(temp[0], temp[1], tempnums[0], tempnums[1], tempnums[2], tempnums[3], tempType, tempskillType, tempnums[4], tempCond));
                    }
                }
                catch (Exception vinny)
                {
                    Console.WriteLine("Vinny error\n" + vinny);
                }
            }   
        }

        public static Player CreatePlayer(string path)
        {
            string[] temp = new string[14];
            int[] tempnums = new int[12];
            ElementResistance[] elementResistances = new ElementResistance[10];
            List<Skill> skills = new List<Skill>();
            Player player;
            List<string> Values = CV.ReadIn(path);
                temp = Values[0].Split(",,");
                tempnums[0] = int.Parse(temp[0]);
                tempnums[1] = int.Parse(temp[1]);
                tempnums[2] = int.Parse(temp[2]);
                tempnums[3] = int.Parse(temp[3]);
                tempnums[4] = int.Parse(temp[4]);
                tempnums[5] = int.Parse(temp[5]);
                tempnums[6] = int.Parse(temp[6]);
                tempnums[7] = int.Parse(temp[7]);
                tempnums[8] = int.Parse(temp[8]);
                tempnums[9] = int.Parse(temp[9]);
                tempnums[10] = int.Parse(temp[10]);
                tempnums[11] = int.Parse(temp[13]);

                string[] tempSkills = temp[11].Split(',');
                for (int j = 0; j < tempSkills.Length; j++)
                {
                    skills.Add(SkillList[tempSkills[j]]);
                }

                string[] tempresists = temp[12].Split(',');
                for (int g = 0; g < tempresists.Length; g++)
                {
                    elementResistances[g] = (ElementResistance)Enum.Parse(typeof(ElementResistance), tempresists[g]);
                }

                tempnums[4] = int.Parse(temp[4]);
                player = (new Player(
                    tempnums[0],
                    tempnums[1],
                    tempnums[2],
                    tempnums[3],
                    tempnums[4],
                    tempnums[5],
                    tempnums[6],
                    tempnums[7],
                    tempnums[8],
                    tempnums[9],
                    tempnums[10],
                    skills,
                    elementResistances,
                    tempnums[11]
                    ));
            return player;
        }

        public static Enemy CreateEnemy(string path)
        {
            string[] temp = new string[14];
            int[] tempnums = new int[12];
            ElementResistance[] elementResistances = new ElementResistance[10];
            List<Skill> skills = new List<Skill>();
            Enemy enemy;
            List<string> Values = CV.ReadIn(path);
            temp = Values[0].Split(",,");
            tempnums[0] = int.Parse(temp[0]);
            tempnums[1] = int.Parse(temp[1]);
            tempnums[2] = int.Parse(temp[2]);
            tempnums[3] = int.Parse(temp[3]);
            tempnums[4] = int.Parse(temp[4]);
            tempnums[5] = int.Parse(temp[5]);
            tempnums[6] = int.Parse(temp[6]);
            tempnums[7] = int.Parse(temp[7]);
            tempnums[8] = int.Parse(temp[8]);
            tempnums[9] = int.Parse(temp[9]);
            tempnums[10] = int.Parse(temp[10]);
            tempnums[11] = int.Parse(temp[13]);

            string[] tempSkills = temp[11].Split(',');
            for (int j = 0; j < tempSkills.Length; j++)
            {
                skills.Add(SkillList[tempSkills[j]]);
            }

            string[] tempresists = temp[12].Split(',');
            for (int g = 0; g < tempresists.Length; g++)
            {
                elementResistances[g] = (ElementResistance)Enum.Parse(typeof(ElementResistance), tempresists[g]);
            }

            tempnums[4] = int.Parse(temp[4]);
            enemy = (new Enemy(
                tempnums[0],
                tempnums[1],
                tempnums[2],
                tempnums[3],
                tempnums[4],
                tempnums[5],
                tempnums[6],
                tempnums[7],
                tempnums[8],
                tempnums[9],
                tempnums[10],
                skills,
                elementResistances,
                tempnums[11]
                ));
            return enemy;
        }

        public static void PlayBattle(Player player, List<Enemy> enemies)
        {
            while(ContinueBattle(enemies) == false)
            {
                player.BattleTurn(enemies);
                foreach (Enemy enemy in enemies)
                {
                    enemy.BattleTurn(player);
                    Console.ReadLine();
                }
                Console.Clear();
            }
             Console.WriteLine("You won the battle! Press enter to continue.");
            Console.ReadLine();
        }

        public static bool ContinueBattle(List<Enemy> enemies)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].CurrentHP <= 0)
                {
                    Console.WriteLine(enemies[i].ToString() + " was defeated!");
                    enemies.Remove(enemies[i]);
                }
            }
            if (enemies.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}
