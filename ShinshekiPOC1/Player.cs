using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShinshekiPOC1
{
    internal class Player : BattleParticipant
    {
        int wp;
        
        Random rand = new Random();

        public int WP
        {
            get { return wp; }
        }

        public Player(
            int maxHP, 
            int currentHP, 
            int maxSP, 
            int currentSP, 
            int str, 
            int mag, 
            int luck, 
            int agl, 
            int def, 
            int armor, 
            int tempDR, 
            List<Skill> usuableSkills, 
            ElementResistance[] elementAnalysis, 
            int wp)
            : base(maxHP, currentHP, maxSP, currentSP, str, mag, luck, agl, def, armor, tempDR, usuableSkills, elementAnalysis)
        {
            this.wp = wp;
        }

        public void TakeDamage(int damage)
        {
            currentHP = currentHP - damage;
            Console.WriteLine("Sho took {0} damage!", damage);
        }

        public void BattleTurn(List<Enemy> enemies)
        {
            if (currentHP > 0)
            {
                // Run through any status affects (make a seperate method in BattleParticipant for this
                Console.WriteLine("Sho's turn!\nCurrent HP - {0}/{1}\nCurrent SP - {2}/{3}",currentHP,maxHP,currentSP,maxSP);
                this.TempDR = 0; // Reset any SOT buffs (mainly TempDR in this build)
                bool CanAct = base.CheckStatus(buffturns);
                if (CanAct == false)
                {
                    switch (CurrentStatus)
                    {
                        // Do certain actions depending on the player's current status.
                        case StatusCond.Confusion:
                            int decision = rand.Next(0, 3);
                            switch (decision)
                            {
                                case 0:
                                    Console.WriteLine("It hurt itself in its confusion!");
                                    currentHP = currentHP - (int)(maxHP * 0.03);
                                    break;
                                case 1:
                                    Console.WriteLine("Too confused to act!");
                                    break;
                                case 2:
                                    Console.WriteLine("You hit an enemy with a 1/2 power melee attack!");
                                    int randomenemy = rand.Next(0, enemies.Count + 1);
                                    double calced = Calculations.FightCommand(this, enemies[randomenemy]) / 2;
                                    enemies[randomenemy].TakeDamage((int)calced);
                                    break;
                            }
                            break;
                        case StatusCond.Enrage:
                            Console.WriteLine("Attacked a random enemy with a powered up melee attack!");
                            int randomenemy2 = rand.Next(0, enemies.Count + 1);
                            double calced2 = Calculations.FightCommand(this, enemies[randomenemy2]) / 2;
                            enemies[randomenemy2].TakeDamage((int)calced2);
                            break;
                    }
                }
                else
                {
                    // Prompt the player for their choice (Fight,Echo, or Guard in this build)
                    Console.WriteLine("What do you want to do? \n1) Fight \n2) Echo \n3) Guard");
                    int choice1 = CV.CVNumber("Please enter a number from 1 to 3.");
                    switch (choice1)
                    {
                        case 1: // For Fight, prompt which enemy to attack and then run the fight calc
                            Console.WriteLine("Which enemy do you want to attack? (Enemies are zero-indexed!)");
                            foreach (Enemy enemy in enemies)
                            {
                                Console.WriteLine(enemy.ToString());
                            }
                            int choice2 = CV.CVNumberAndZero("Please enter a valid number.");
                            double calced1 = 0;
                            try
                            {
                                if (enemies.Count == 1)
                                {
                                    calced1 = Calculations.FightCommand(this, enemies[0]);
                                }
                                else
                                {
                                    calced1 = Calculations.FightCommand(this, enemies[choice2]);
                                }
                                
                            }
                            catch
                            {
                                
                            }

                            if (enemies.Count == 1)
                            {
                                enemies[0].TakeDamage((int)calced1);
                            }
                            else
                            {
                                enemies[choice2].TakeDamage((int)calced1);
                            }

                            break;
                        case 2: // For Echo, prompt for which skill they want to use
                            Console.WriteLine("Which skill do you want to use? (Skills are zero-indexed!)");
                            foreach (Skill skill in usuableSkills)
                            {
                                Console.WriteLine(skill.ToString());
                                Console.WriteLine();
                            }
                            int choice3 = CV.CVNumberAndZero("Please enter a valid number.");

                            Console.WriteLine("Which enemy do you want to attack? (Enemies are zero-indexed!)"); // Then prompt who they want to fight and run the proper command
                            foreach (Enemy enemy in enemies)
                            {
                                Console.WriteLine(enemy.ToString());
                            }
                            int choice4 = CV.CVNumberAndZero("Please enter a valid number.");

                            // Calculate the damage and if the attack hits or not
                            double calced2  = 0;
                            try
                            {
                                if(enemies.Count == 1)
                                {
                                    calced2 = usuableSkills[choice3].CalculateDMG(this, enemies[0]);
                                }
                                else
                                {
                                    calced2 = usuableSkills[choice3].CalculateDMG(this, enemies[choice4]);
                                }
                            }
                            catch
                            {
                                
                            }
                            int attackhit = rand.Next(0, 101);
                            if (attackhit <= usuableSkills[choice3].Accuracy)
                            {
                                if(enemies.Count == 1)
                                {
                                    enemies[0].TakeDamage((int)calced2);
                                }
                                else
                                {
                                    enemies[choice4].TakeDamage((int)calced2);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Attack missed...");
                            }

                            // Remove the cost of the skill from the current HP/SP

                            switch (usuableSkills[choice3].SkillType)
                            {
                                case SkillType.Phys:
                                    currentHP = currentHP - (int)maxHP * (usuableSkills[choice3].Cost / 100);
                                    break;
                                case SkillType.Magic:
                                    currentSP = currentSP - usuableSkills[choice3].Cost;
                                    break;

                            }
                            break;
                        case 3:
                            // For Guard, add 20% to the TempDR
                            Console.WriteLine("Guarding!");
                            tempDR = 20;
                            break;
                    }

                    // Increment buffturns 

                    if (currentStatus != StatusCond.None)
                    {
                        buffturns = buffturns + 1;
                    }

                }
            }
            else
            {
                Console.WriteLine("You collapse to the floor...\nGAME OVER\nPress enter to exit the program.");
                Console.ReadLine();
                System.Environment.Exit(1);
            }
        }
    }
}
