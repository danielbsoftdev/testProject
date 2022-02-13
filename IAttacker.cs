using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    interface IAttacker: ICharacter
    {
        int Strength { get; set; }
       // List<Skill> SkillList { get; set; }

        void Attack(IDefender defender);
    }
}
