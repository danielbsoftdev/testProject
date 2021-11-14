using System.Collections.Generic;

namespace Test
{
    public class Skill : ISkill
    {
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
            TurnsWhenSkillApplies = Game.GetTurns(useLikehood, true);
        }
    }
}