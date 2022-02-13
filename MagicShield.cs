using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class MagicShield : ISkill
    {
        public int Chance { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public double ApplySkill(int damage)
        {
            return damage / 2;
        }

        public void ApplySkill()
        {
            throw new NotImplementedException();
        }
    }
}
