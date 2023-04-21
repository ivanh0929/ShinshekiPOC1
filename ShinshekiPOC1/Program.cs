using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShinshekiPOC1 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                BattleManager.InitSkills();
                CutsceneManager.Init();
                Player Sho = BattleManager.CreatePlayer("Skills/Player.csv");
                List<Enemy> enemies = new List<Enemy>();
                enemies.Add(BattleManager.CreateEnemy("Skills/Enemy1.csv"));
                enemies.Add(BattleManager.CreateEnemy("Skills/Enemy2.csv"));
                for (int i = 0; i < 12; i++)
                {
                    CutsceneManager.PlaySpecific(i);
                }

                BattleManager.PlayBattle(Sho, enemies);
                CutsceneManager.PlaySpecific(13);
                CutsceneManager.PlaySpecific(14);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
            

        }
    }
}