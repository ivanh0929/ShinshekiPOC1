using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShinshekiPOC1
{
    enum EmotionState
    {
        Angry,
        Surprise,
        Sad,
        Melancholy,
        Happy,
        Determined,
        Playful,
        None
    }
    enum TimeOfDay
    {
        Morning,
        School,
        Lunch,
        Afternoon,
        Afterschool,
        Evening,
        Sleeping

    }

    enum Month
    {
        January,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }

    enum Weekday
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday,
    }
    enum StatusCond
    {
        None,
        Knocked,
        Dizzy,
        Poison,
        Seal,
        Confusion,
        Enrage,
        Fear,
        Stun,
        Burn,
        Freeze,
        Shock,
        Hunger,
        Lethargy,
        SiSw
    }
    enum SkillType
    {
        Magic,
        Phys,
        MagicAndPhys,
        Heal,
        Status,
        Support,
        Passive,
        ULT
    }

    enum ElementType
    {
        Phys,
        Ice,
        Fire,
        Elec,
        Wind,
        Psy,
        Nuc,
        Bls,
        Cur,
        Alm
    }

    enum ElementResistance
    {
        Normal,
        Weak,
        Resist,
        Block,
        Repel,
        Absorb
    }

    internal class AllEnums
    {
        // This class is only here for organization purposes.
    }
}
