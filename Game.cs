using System;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    class Game : IGame
    {
        private int turnsPerGame = 20;

        public void Play()
        {
            
            var gameCounter = 0;
            Console.WriteLine("---Welcome---");
            while (true)
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine("Please press 's' to start " + (gameCounter > 0 ? "a new Game" : string.Empty) + " or 'e' to exit");
                Console.WriteLine("The game ends when one of the players remains without health or the number of turns reaches " + turnsPerGame.ToString() + ".");
                var key = Console.ReadKey(true);
                if (key.Key.ToString().ToLower() == "s")
                {
                    Console.Clear();
                    Console.WriteLine("--Game started--");

                    ICharacter Orderus = new Character("Orderus");
                    ICharacter Vasilica = new Character("Vasilica");
                    ICharacter Wildbeast = new Character("Wildbeast");

                    ICharacter temp = null;

                    string winner = string.Empty;

                    var turnsCounter = 1;

                    SetCharacters(Orderus, Wildbeast, out ICharacter attacker, out ICharacter defender);

                    while (turnsCounter < turnsPerGame + 1 && winner == string.Empty)
                        SimulateTurn(ref attacker, ref defender, ref Vasilica, ref turnsCounter, ref winner, ref temp);

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

        void SetCharacters(ICharacter Orderus, ICharacter Wildbeast, out ICharacter attacker, out ICharacter defender)
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

        static void SimulateTurn(ref ICharacter attacker, ref ICharacter defender, ref ICharacter Vasilica, ref int turnsCounter, ref string winner, ref ICharacter temp)
        {
            string skillUsed = string.Empty;
            double damangePercent = 1;
            bool attackTwice = false;
            bool isVasilicaAttacking = false;
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

            if (attacker.Name == "Orderus" && Vasilica.Hand > 0)
            {
                isVasilicaAttacking = true;
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
            double damage3 = 0;

            if (!defender.TurnsWhenLuckApplies.Contains(turnsCounter))
            {
                damage = (attacker.Strength - defender.Defense) * damangePercent;
                defender.Health = defender.Health - damage <= 0 ? 0 : defender.Health - damage;

                if (attackTwice)
                {
                    damage2 = attacker.Strength - defender.Defense;
                    defender.Health = defender.Health - damage2 <= 0 ? 0 : defender.Health - damage2;
                }

                if (isVasilicaAttacking)
                {
                    damage3 = Vasilica.Strength * 2;
                    Vasilica.Hand = Vasilica.Hand - Vasilica.Strength / 4;
                    defender.Health = defender.Health - damage3 <= 0 ? 0 : defender.Health - damage3;
                }
            }

            Console.WriteLine("Turn " + turnsCounter.ToString());
            Console.WriteLine(string.Format(" {0} attacked, {1} defended. ", attacker.Name, defender.Name));
            Console.WriteLine(" Skills used: " + (skillUsed == string.Empty ? "none" : skillUsed));
            Console.WriteLine(string.Format(" Damage done: {0}. Defender's health left: {1}.", (Math.Round(damage + damage2 + damage3, 1)).ToString(), defender.Health));
            Console.WriteLine(string.Empty);

            if (defender.Health <= 0)
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

            int turnNumber = turnsPerGame * useLikehood / 100 ;
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

}

