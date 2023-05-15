using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShinshekiPOC1
{
    
    internal class Skill
    {
        // Fields

        SkillType skillType; // Type of Skill

        public SkillType SkillType { get { return skillType; } }

        ElementType ElementType; // Elemental type
        StatusCond StatusCond; // Appliable status condition
        int sp; // Skill Power
        int acc; // Accuracy
        int ail; // Ailment Chance
        int crit; // Crit chance
        int cost; // Cost

        public int Accuracy
        { get { return acc; } }
        public int Cost
        { get { return cost; } }

        string desc; // Description
        string name; // Skill Name

        // Constructor

        public Skill(string name, string desc, int sp, int acc, int crit, int cost, ElementType elementType, SkillType skillType, int ail, StatusCond statusCond)
        {
            this.sp = sp;
            this.acc = acc;
            this.ail = ail;
            this.crit = crit;
            this.cost = cost;
            this.name = name;
            this.desc = desc;
            this.name = name;
            this.ElementType = elementType;
            this.skillType = skillType;
            this.StatusCond = statusCond;
        }

        // Methods

        // Make an event that runs on the turn

        public int CalculateDMG(Player player, Enemy enemy)
        {
            double Calced = 0;
            Random rand = new Random();

            switch (this.skillType)
            {
                case (SkillType.Phys):
                    Calced = (this.sp * (Math.Sqrt(player.CurrentStrength)) / (Math.Sqrt((enemy.CurrentDefense * 8) + enemy.Armor)));
                    break;
                case (SkillType.Magic):
                    Calced = (this.sp + (this.sp * (player.CurrentMagic / 30))) / (Math.Sqrt((enemy.CurrentDefense * 8) + enemy.Armor));
                    break;
            }

            Calculations.Crit(Calced, rand, this.crit);
            Calced = Calculations.Variance(rand);
            Calced = Calculations.EnemyDR(Calced, enemy.DR + enemy.TempDR);
            int final = (int)Math.Round(Calced);
            this.ChangeStatusCond(enemy);
            return final;

        }

        public int CalculateEnemyDMG(Player player, Enemy enemy)
        {
            double Calced = 0;
            Random rand = new Random();

            switch (this.skillType)
            {
                case (SkillType.Phys):
                    Calced = (this.sp * (Math.Sqrt(enemy.CurrentStrength)) / (Math.Sqrt((player.CurrentDefense * 8) + player.Armor)));
                    break;
                case (SkillType.Magic):
                    Calced = (this.sp + (this.sp * (enemy.CurrentMagic / 30))) / (Math.Sqrt((player.CurrentDefense * 8) + player.Armor));
                    break;
            }

            Calculations.Crit(Calced, rand, this.crit);
            Calced = Calculations.Variance(rand);
            Calced = Calculations.EnemyDR(Calced, player.TempDR);
            int final = (int)Math.Round(Calced);
            this.ChangeStatusCond(player);
            return final;

        }

        public void ChangeStatusCond(Enemy enemy)
        {
            bool apply = this.CalculateAilChance();
            if (apply == true)
            {
                enemy.CurrentStatus = this.StatusCond;
            }
        }

        public void ChangeStatusCond(Player player)
        {
            bool apply = this.CalculateAilChance();
            if (apply == true)
            {
                player.CurrentStatus = this.StatusCond;
            }
        }

        public bool CalculateAilChance()
        {
            Random rand = new Random();
            int chance = rand.Next(0, 101);
            if(chance <= this.ail)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public override string ToString()
        {
            string ET_ts = this.ElementType.ToString();
            string ts = "Name - " + this.name + "\nDescription - " + this.desc + "\nSkill Power - " + this.sp + "\nCritical Chance - " + this.crit + "%\nCost - " + this.cost + "(HP if Phys, SP if Elemental)\nElemental Typing - " + ET_ts;
            return ts;
        }

    }
}
