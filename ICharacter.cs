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
        int Strength { get; set; }
        int Speed { get; set; }
        int Defense { get; set; }
        int Luck { get; set; }
        int Hand { get; set; }
        List<int> TurnsWhenLuckApplies { get; set; }
        List<Skill> SkillList { get; set; }
        Character Clone();
    }
}
