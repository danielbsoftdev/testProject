using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    interface IDefender: ICharacter
    {
        double Health { get; set; }
        int Defense { get; set; }
        int Luck { get; set; }
        List<ISkill> SkillList { get; set; }

        bool IsStillAlive();
        bool IsLuckyThisTurn(int turn);
        void ReceiveDamage(int damage);
    }
}
