using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.LiveCoding
{
    class Character : IAttacker, IDefender
    {
        //defender
        public double Health { get; set; }
        public int Defense { get; set; }
        public int Luck { get; set; }
        public List<int> TurnsWhenLuckApplies { get; set; }
        private int AttackReceivedCounter = 0;
        private int ReceivedDamage = 0;
        public MagicShield MagicShield;

        //atacker
        public int Strength { get; set; }
        public RapidStrike RapidStrike;

        //common
        public string Name { get; set; }
        public int Speed { get; set; }
        public List<ISkill> SkillList { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Character(string name, int defense, int luck, int strength, int health) {
            Name = name;
            Defense = defense;
            Luck = luck;
            Strength = strength;
            Health = health;
        }

        private int GetDamage()
        {
            return Strength;
        }

        public IAttacker AsAttacker() {
            return this;
        }

        public IDefender AsDefender()
        {
            return this;
        }

        public void Attack(IDefender defender)
        {
            defender.ReceiveDamage(GetDamage());
        }

        public bool IsFasterThan(ICharacter otherCharacter)
        {
            return Speed > otherCharacter.Speed;
        }

        public bool IsLuckyThisTurn(int turn)
        {

            var turn1 = new Random();
            var generated = turn1.Next(100);

            return generated < Luck;
        }

        public bool IsStillAlive()
        {
            return Health > 0;
        }

        public void ReceiveDamage(int strengts)
        {
            ReceivedDamage = strengts;
            AttackReceivedCounter++;
            if(!IsLuckyThisTurn(AttackReceivedCounter)){
                Health = ComputeNewHealth(); 
            }
        }

        private bool IsSkillApply(ISkill skill)
        {
            return false;
        }

        private int ComputeNewHealth()
        {
            var generated = new Random().Next(30);

            foreach (var skill in SkillList)
            {
                if (IsSkillApply(skill))
                {
                    skill.ApplySkill();
                }
                else
                {
                    Health - (strengts - Defense);
                }

            }
           
            return 
        }

    }
}
