using System;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    class Program
    {
        public static int turnsPerGame = 20;

        static void Main(string[] args)
        {
            var gameCounter = 0;
            Console.WriteLine("---Welcome---");
            while (true)
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine("Please press 's' to start " + (gameCounter > 0 ? "a new Game" : string.Empty )+ " or 'e' to exit");
                Console.WriteLine("The game ends when one of the players remains without health or the number of turns reaches " + turnsPerGame.ToString() + ".");
                var key = Console.ReadKey(true);
                if (key.Key.ToString().ToLower() == "s")
                {
                    Console.Clear();
                    Console.WriteLine("--Game started--");

                    var Orderus = new Character("Orderus");
                    var Wildbeast = new Character("Wildbeast");

                    Character attacker = null;
                    Character defender = null;
                    Character temp = null;

                    string winner = string.Empty;
                  
                    var turnsCounter = 1;

                    SetCharacters(Orderus, Wildbeast, out attacker, out defender);

                    while (turnsCounter < turnsPerGame + 1 && winner == string.Empty)
                        SimulateTurn(ref attacker, ref defender, ref turnsCounter, ref winner, ref temp);

                    if (winner == string.Empty)
                        winner = Orderus.Health >= Wildbeast.Health ? Orderus.Name : Wildbeast.Name;

                    Console.WriteLine("The winner is: " + winner);
                    Console.WriteLine("--Game finished--");
                    gameCounter += 1;
                }
                else
                {
                    Environment.Exit(0);
                }
            }
          
        }

        static void SetCharacters(Character Orderus, Character Wildbeast, out Character attacker, out Character defender)
        {
            if (Orderus.Speed != Wildbeast.Speed)
            {
                attacker = Orderus.Speed >= Wildbeast.Speed ? Orderus.Clone() : Wildbeast.Clone();
                defender = Orderus.Speed >= Wildbeast.Speed ? Wildbeast.Clone() : Orderus.Clone();
            }
            else
            {
                attacker = Orderus.Luck >= Wildbeast.Luck ? Orderus.Clone() : Wildbeast.Clone();
                defender = Orderus.Luck >= Wildbeast.Luck ? Wildbeast.Clone() : Orderus.Clone();
            }
        }

        static void SimulateTurn(ref Character attacker, ref Character defender, ref int turnsCounter, ref string winner, ref Character temp)
        {
            string skillUsed = string.Empty;
            double damangePercent = 1;
            bool attackTwice = false;
            if (attacker.SkillList != null && attacker.SkillList.Count > 0)
            {
                // for the sake of simplicity we only apply one skill per turn per character
                var skill = attacker.SkillList.FirstOrDefault(s => s.IsAttackSkill);
                if (skill != null && skill.TurnsWhenSkillApplies.Contains(turnsCounter) && skill.Name == "Rapid Strike")
                {
                    attackTwice = true;
                    skillUsed += string.Format(" {0} by {1}.", skill.Name, attacker.Name);
                }
            }

            if (defender.SkillList != null && defender.SkillList.Count > 0)
            {
                // for the sake of simplicity we only apply one skill per turn per character
                var skill = defender.SkillList.FirstOrDefault(s => s.IsAttackSkill == false);
                if (skill != null && skill.TurnsWhenSkillApplies.Contains(turnsCounter) && skill.Name == "Magic Shield")
                {
                    damangePercent = 0.5;
                    skillUsed += string.Format(" {0} by {1}.", skill.Name, defender.Name);
                }
            }

            double damage = 0;
            double damage2 = 0;

            if (!defender.TurnsWhenLuckApplies.Contains(turnsCounter))
            {
                damage = (attacker.Strength - defender.Defense) * damangePercent;
                defender.Health = defender.Health - damage <= 0 ? 0 : defender.Health - damage;

                if (attackTwice)
                {
                    damage2 = attacker.Strength - defender.Defense;
                    defender.Health = defender.Health - damage2 <= 0 ? 0 : defender.Health - damage2;
                }
            }

            Console.WriteLine("Turn " + turnsCounter.ToString());
            Console.WriteLine(string.Format(" {0} attacked, {1} defended. ", attacker.Name, defender.Name));
            Console.WriteLine(" Skills used: " + (skillUsed == string.Empty ? "none" : skillUsed));
            Console.WriteLine(string.Format(" Damage done: {0}. Defender's health left: {1}.", (Math.Round(damage + damage2, 0)).ToString(), defender.Health));
            Console.WriteLine(string.Empty);

            if (defender.Health < 1)
            {
                winner = attacker.Name;
            }
            else
            {
                temp = attacker.Clone();
                attacker = defender.Clone();
                defender = temp.Clone();
            }

            turnsCounter += 1;
        }

        static public List<int> GetTurns(int useLikehood, bool isSkillRelated = false)
        {
            var rnd = new Random();
            var list = new List<int>();

            var multiplicator = 1;
            if (isSkillRelated)
            {
                // divided by 2 because a skill is applied only while attacking or while defending; 
                // that means the percent refers not to the total turns of the game, but only to half of them
                multiplicator = 2;
            }
          
            int turnNumber = turnsPerGame * useLikehood / 100 / multiplicator;
            int generatedTurns = 0;
            while (generatedTurns < turnNumber)
            {
                var generated = rnd.Next(1, turnsPerGame + 1);
                if (!list.Contains(generated))
                {
                    list.Add(generated);
                    generatedTurns += 1;
                }
            }

            return list;
        }
    }

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
        public List<int> TurnsWhenLuckApplies { get; set; }
        public List<Skill> SkillList { get; set; }

        public Character(string name)
        {
            Name = name;
            var rnd = new Random();
            if (name == "Orderus")
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
            }
            else
            {
                Health = rnd.Next(60, 91);
                Strength = rnd.Next(60, 91);
                Defense = rnd.Next(40, 61);
                Speed = rnd.Next(40, 61);
                Luck = rnd.Next(25, 41);
            }
            TurnsWhenLuckApplies = Program.GetTurns(Luck);
        }

        public Character Clone()
        {
            return (Character)MemberwiseClone();
        }
    }
   
    public class Skill{
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAttackSkill { get; set; }
        public int UseLikehood { get; set; }
        public List<int> TurnsWhenSkillApplies { get; set; }
        public Skill(string name, string description, bool isAttackSkill, int useLikehood)
        {
            Name = name;
            Description = description;
            IsAttackSkill = isAttackSkill;
            UseLikehood = useLikehood;
            TurnsWhenSkillApplies = Program.GetTurns(useLikehood, true);
        }
    }

    
}