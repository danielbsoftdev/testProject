using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    interface ICharacter
    {
        string Name { get; set; }
        double Health { get; set; }
        int Speed { get; set; }
        //List<Skill> SkillList { get; set; }

        bool IsFasterThan(ICharacter otherCharacter);
        IAttacker AsAttacker();
        IDefender AsDefender();
    }
}
