using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShinshekiPOC1
{
    internal class Calculations
    {
        public static double FightCommand(Player player,Enemy enemy)
        {
            double Calced = ((Math.Sqrt(player.WP) * (Math.Sqrt(player.CurrentStrength)) / (Math.Sqrt((enemy.CurrentDefense * 8) + enemy.Armor))));
            return Calced;
        }

        public static double EnemyFightCommand(Player player, Enemy enemy)
        {
            double Calced = ((Math.Sqrt(enemy.CurrentStrength)) / (Math.Sqrt((player.CurrentDefense * 8) + player.Armor)));
            return Calced;
        }

        public static double Crit(double Calced, Random CritRoll, int crit)
        {
            // Check for how many ATK buffs and add them to the SOT buffs
            int CritChance = 0;
            CritChance = CritRoll.Next(1, 101);
            if (CritChance <= crit)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The attack was a critical hit!");
                Console.ForegroundColor = ConsoleColor.White;
                Calced = Calced * 1.3;
            }

            return Calced;
        }

        public static double Variance(Random VarianceRoll)
        {
            double rand = VarianceRoll.NextDouble() * (1.05 - .95) + .95;
            rand = Math.Round(rand, 2);
            return rand;
        }

        public static double EnemyDR(double Calced, int DR)
        {
            if (DR >= 1)
            {
                DR = (100 - DR) / 10;
                Calced = Calced * DR;
            }
            return Calced;


        }
    }
}
