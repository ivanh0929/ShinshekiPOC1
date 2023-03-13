using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShinshekiPOC1
{
    internal abstract class BattleParticipant
    {
        // Fields

        protected int maxHP;

        protected int buffturns;

        public int MaxHP
        {
            get { return maxHP; }
            set {
                if (value <= 0)
                {
                    Console.WriteLine("Max HP cannot be lower than 1. Setting it to 1.");
                    maxHP = 1;
                }
                else
                {
                    maxHP = value;
                }
            }
        }

        protected int currentHP;

        public int CurrentHP
        {
            get { return currentHP; }
            set
            {
                if (value <= -1)
                {
                    Console.WriteLine("Current HP cannot be lower than 0. Setting it to 0.");
                    currentHP = 0;
                }
                else
                {
                    currentHP = value;
                }
            }
        }

        protected int maxSP;

        public int MaxSP
        {
            get { return maxSP; }
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("Max SP cannot be lower than 1. Setting it to 1.");
                    maxSP = 1;
                }
                else
                {
                    maxSP = value;
                }
            }
        }

        protected int currentSP;

        public int CurrentSP
        {
            get { return currentSP; }
            set
            {
                if (value <= -1)
                {
                    Console.WriteLine("Current SP cannot be lower than 0. Setting it to 0.");
                    currentSP = 0;
                }
                else
                {
                    currentSP = value;
                }
            }
        }

        protected int str;
        public int Strength
        {
            get { return str; }
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("Strength stat cannot be lower than 1. Setting it to 1.");
                    str = 1;
                }
                else
                {
                    str = value;
                }
            }
        }

        protected int mag;

        public int Magic
        {
            get { return mag; }
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("Magic stat cannot be lower than 1. Setting it to 1.");
                    mag = 1;
                }
                else
                {
                    mag = value;
                }
            }
        }

        protected int luck;

        public int Luck
        {
            get { return luck; }
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("Luck stat cannot be lower than 1. Setting it to 1.");
                    luck = 1;
                }
                else
                {
                    luck = value;
                }
            }
        }

        protected int agl;

        public int Agility
        {
            get { return agl; }
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("Agility stat cannot be lower than 1. Setting it to 1.");
                    agl = 1;
                }
                else
                {
                    agl = value;
                }
            }
        }

        protected int def;

        public int Defense
        {
            get { return def; }
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("Defense stat cannot be lower than 1. Setting it to 1.");
                    def = 1;
                }
                else
                {
                    def = value;
                }
            }
        }

        protected int armor;

        public int Armor
        {
            get { return armor; }
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("Armor stat cannot be lower than 1. Setting it to 1.");
                    armor = 1;
                }
                else
                {
                    armor = value;
                }
            }
        }

        protected int tempDR;

        public int TempDR
        {
            get { return tempDR; }
            set { tempDR = value; }
        }

        protected List<Skill> usuableSkills = new List<Skill>();

        public List<Skill> UsuableSkills
        {
            get { return usuableSkills; }
        }

        protected ElementResistance[] elementAnalysis = new ElementResistance[10]; //10 is every element including Almighty

        public ElementResistance[] ElementAnalysis
        {
            get { return elementAnalysis; }
        }

        protected StatusCond currentStatus = StatusCond.None;

        public StatusCond CurrentStatus
        {
            get { return currentStatus; }
            set { currentStatus = value; }
        }

        public BattleParticipant(int maxHP, int currentHP, int maxSP, int currentSP,int str, int mag, int luck, int agl, int def, int armor, int tempDR, List<Skill> usuableSkills, ElementResistance[] elementAnalysis)
        {
            this.maxHP = maxHP;
            this.currentHP = currentHP;
            this.maxSP = maxSP;
            this.currentSP = currentSP;
            this.str = str;
            this.mag = mag;
            this.luck = luck;
            this.agl = agl;
            this.def = def;
            this.armor = armor;
            this.tempDR = tempDR;
            this.usuableSkills = usuableSkills;
            this.elementAnalysis = elementAnalysis;
        }
        public bool CheckStatus(int TurnsPassed)
        {
            switch(currentStatus)
            {
                case StatusCond.None:
                    return true;
                    break;
                case StatusCond.Knocked:
                    Console.WriteLine("Recovered from being knocked!");
                    currentStatus = StatusCond.None;
                    return true;
                    break;
                case StatusCond.Dizzy:
                    if(TurnsPassed == 0)
                    {
                        Console.WriteLine("Turn skipped due to being dizzy.");
                    }
                    else
                    {
                        Console.WriteLine("Recovered from being dizzy!");
                        currentStatus = StatusCond.None;
                        buffturns = 0;
                    }
                    return false;
                    break;
                case StatusCond.Poison:
                    if (TurnsPassed <= 2)
                    {
                        Console.WriteLine("Damaged by poison!");
                        currentHP = currentHP - (int)(maxHP * 0.05);
                    }
                    else
                    {
                        Console.WriteLine("Recovered from being dizzy!");
                        currentStatus = StatusCond.None;
                        buffturns = 0;
                    }
                    return true;
                    break;
                case StatusCond.Seal:
                    if (TurnsPassed <= 2)
                    {
                        Console.WriteLine("Can't use Echo moves this turn!");
                    }
                    else
                    {
                        Console.WriteLine("Recovered from being sealed!");
                        currentStatus = StatusCond.None;
                        buffturns = 0;
                    }
                    return true;
                    break;
                case StatusCond.Confusion:
                    if (TurnsPassed <= 2)
                    {
                        Console.WriteLine("Can't act due to being confused!");
                    }
                    else
                    {
                        Console.WriteLine("Recovered from being confused!");
                        currentStatus = StatusCond.None;
                        buffturns = 0;
                    }
                    return false;
                    break;
                case StatusCond.Enrage:
                    if (TurnsPassed <= 2)
                    {
                        Console.WriteLine("Can't act due to being enraged!");
                    }
                    else
                    {
                        Console.WriteLine("Recovered from being enraged!");
                        currentStatus = StatusCond.None;
                        buffturns = 0;
                    }
                    return false;
                    break;
                case StatusCond.Fear:
                    if (TurnsPassed <= 2)
                    {
                        Console.WriteLine("Can't act due to being fearful!");
                    }
                    else
                    {
                        Console.WriteLine("Recovered from being fearful!");
                        currentStatus = StatusCond.None;
                        buffturns = 0;
                    }
                    return false;
                    break;
                case StatusCond.Stun:
                    if (TurnsPassed <= 2)
                    {
                        Console.WriteLine("Can't act due to being stunned!");
                    }
                    else
                    {
                        Console.WriteLine("Recovered from being stunned!");
                        currentStatus = StatusCond.None;
                        buffturns = 0;
                    }
                    return false;
                    break;
                case StatusCond.Burn:
                    if (TurnsPassed <= 4)
                    {
                        Console.WriteLine("Damaged by burn!");
                        currentHP = currentHP - (int)(maxHP * 0.03);
                    }
                    else
                    {
                        Console.WriteLine("Recovered from being burned!");
                        currentStatus = StatusCond.None;
                        buffturns = 0;
                    }
                    return true;
                    break;
                case StatusCond.Freeze:
                    if (TurnsPassed <= 2)
                    {
                        Console.WriteLine("Can't act due to being frozen!");
                    }
                    else
                    {
                        Console.WriteLine("Recovered from being frozen!");
                        currentStatus = StatusCond.None;
                        buffturns = 0;
                    }
                    return false;
                    break;
                case StatusCond.Shock:
                    if (TurnsPassed <= 2)
                    {
                        Console.WriteLine("Can't act due to being shocked!");
                    }
                    else
                    {
                        Console.WriteLine("Recovered from being shocked!");
                        currentStatus = StatusCond.None;
                        buffturns = 0;
                    }
                    return false;
                    break;
                case StatusCond.Hunger:
                    if (TurnsPassed <= 4)
                    {
                        Console.WriteLine("Attack lowered and skill cost doubled due to hunger...");
                    }
                    else
                    {
                        Console.WriteLine("Recovered from being hungry!");
                        currentStatus = StatusCond.None;
                        buffturns = 0;
                    }
                    return true;
                    break;
                case StatusCond.Lethargy:
                    if (TurnsPassed <= 4)
                    {
                        Console.WriteLine("SP drained from lethargy!");
                        currentSP = currentSP - (int)(maxSP * 0.05);
                    }
                    else
                    {
                        Console.WriteLine("Recovered from lethargy!");
                        currentStatus = StatusCond.None;
                        buffturns = 0;
                    }
                    return true;
                    break;
                case StatusCond.SiSw:
                    if (TurnsPassed <= 2)
                    {
                        Console.WriteLine("Healed from Sickly Sweet!");
                        currentSP = currentSP + (int)(maxSP * 0.05);
                        currentHP = currentHP + (int)(maxHP * 0.03);

                    }
                    else
                    {
                        Console.WriteLine("Damaged by sickly sweetness!");
                        currentSP = currentSP - (int)(maxSP * 0.20);
                        currentHP = currentHP - (int)(maxHP * 0.12);
                        currentStatus = StatusCond.None;
                        buffturns = 0;
                    }
                    return true;
                    break;
            }

            return true;
        }

        
    }
}
