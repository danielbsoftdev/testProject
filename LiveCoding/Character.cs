using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.LiveCoding
{
    class Character : ICharacter
    {

        //defender
        public double Health { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Defense { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Luck { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<int> TurnsWhenLuckApplies { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //atacker
        public int Strength { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Speed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<Skill> SkillList { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }



        //public int Mana { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private int GetDamage()
        {
            return 0;
        }

        public void Attack(IDefender defender)
        {
            defender.ReceiveDamage(GetDamage());
        }

        public Test.Character Clone()
        {
            throw new NotImplementedException();
        }

        public bool IsFasterThan(ICharacter otherCharacter)
        {
            return Speed > otherCharacter.Speed;
        }

        public bool IsLuckyThisTurn(int turn)
        {
            throw new NotImplementedException();
        }

        public bool IsStillAlive()
        {
            return Health > 0;
        }

        public void ReceiveDamage(int strengts)
        {
            //check skill
          
            if(!IsLuckyThisTurn()){
                Health = Health - (strengts - Defense);
            }
        }
    }
}
