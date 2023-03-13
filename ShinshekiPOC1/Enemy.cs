using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShinshekiPOC1
{
    internal class Enemy : BattleParticipant
    {
        int dr;

        public int DR
        {
            get { return dr; }
        }

        public Enemy(int maxHP, int currentHP, int maxSP, int currentSP, int str, int mag, int luck, int agl, int def, int armor, int tempDR, List<Skill> usuableSkills, ElementResistance[] elementAnalysis, int dr) : base(maxHP, currentHP, maxSP, currentSP, str, mag, luck, agl, def, armor, tempDR, usuableSkills, elementAnalysis)
        {
            this.dr = dr;
        }

        public void BattleTurn(Player player)
        {
            if (currentHP > 0)
            {
                // Run through any status affects (make a seperate method in BattleParticipant for this
                Console.WriteLine("Enemy's turn!");
                Random rand = new Random();
                this.TempDR = 0; // Reset any SOT buffs (mainly TempDR in this build)
                bool CanAct = base.CheckStatus(buffturns);
                if (CanAct == false)
                {
                    switch (CurrentStatus)
                    {
                        // Do certain actions depending on the enemy's current status.
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
                            }
                            break;
                    }
                }
                else
                {
                    // Pick a random move based on a Rand.Next

                    int choice1 = rand.Next(1, 4);
                    switch (choice1) // Make a switch for the choice (either Cleave,a magic attack, or a small chance to guard)
                    {
                        case 1: // Cleave
                            double calced1 = usuableSkills[0].CalculateDMG(player, this);
                            player.TakeDamage((int)calced1);
                            switch (usuableSkills[0].SkillType)
                            {
                                case SkillType.Phys:
                                    currentHP = currentHP - (int)maxHP * (usuableSkills[0].Cost / 100);
                                    break;
                                case SkillType.Magic:
                                    currentSP = currentSP - usuableSkills[0].Cost;
                                    break;

                            }
                            break;
                        case 2: // Magic
                                // Calculate the damage and if the attack hits or not

                            double calced2 = usuableSkills[1].CalculateDMG(player, this);
                            int attackhit = rand.Next(0, 101);
                            if (attackhit <= usuableSkills[1].Accuracy)
                            {
                                player.TakeDamage((int)calced2);
                            }
                            else
                            {
                                Console.WriteLine("Attack missed...");
                            }

                            // Remove the cost of the skill from the current HP/SP

                            switch (usuableSkills[1].SkillType)
                            {
                                case SkillType.Phys:
                                    currentHP = currentHP - (int)maxHP * (usuableSkills[1].Cost / 100);
                                    break;
                                case SkillType.Magic:
                                    currentSP = currentSP - usuableSkills[1].Cost;
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
        }

        public override string ToString()
        {
            string ts = ("Enemy - HP"+currentHP+"/"+maxHP);
            return ts;
        }

        public void TakeDamage(int damage)
        {
            currentHP = currentHP - damage;
            Console.WriteLine("The enemy took {0} damage!", damage);
        }
    }
}
