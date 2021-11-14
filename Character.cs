using System;
using System.Collections.Generic;

namespace Test
{
    // another approach would have been to declare a separate class for each character type
    // but in that case, switching between attacker and defender would also require a different handling 
    public class Character
    {
        public string Name { get; set; }
        public double Health { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }
        public int Luck { get; set; }
        public int Hand { get; set; }
        public List<int> TurnsWhenLuckApplies { get; set; }
        public List<Skill> SkillList { get; set; }

        public Character(string name)
        {
            Name = name;
            var rnd = new Random();
            if (name == "Orderus" || name == "Vasilica")
            {
                Health = rnd.Next(70, 101);
                Strength = rnd.Next(70, 81);
                Defense = rnd.Next(45, 56);
                Speed = rnd.Next(40, 51);
                Luck = rnd.Next(10, 31);
                SkillList = new List<Skill>()
                {
                    new Skill("Rapid Strike", "Strike twice while attaking", true, 10),
                    new Skill("Magic Shield", "Takes half of damage when defending", false, 20)
                };

                if (name == "Vasilica")
                {
                    Hand = 100;
                }
            }
            else
            {
                Health = rnd.Next(60, 91);
                Strength = rnd.Next(60, 91);
                Defense = rnd.Next(40, 61);
                Speed = rnd.Next(40, 61);
                Luck = rnd.Next(25, 41);
            }
            TurnsWhenLuckApplies = Game.GetTurns(Luck);
        }

        public Character Clone()
        {
            return (Character)MemberwiseClone();
        }
    }
}
