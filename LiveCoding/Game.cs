using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.LiveCoding
{
    class Game : IGame
    {
        private IAttacker attacker;
        private IDefender defender;
        private readonly int _maxTurns;
        private int _currentTurn;

        public Game(ICharacter erou, ICharacter bestie, int maxTurns)
        {
            _maxTurns = maxTurns;
            _currentTurn = 0;
            if (erou.IsFasterThan(bestie))
            {
                attacker = erou.AsAttacker();
                defender = bestie.AsDefender();
            }
            else
            {
                attacker = bestie.AsAttacker();
                defender = erou.AsDefender();
            }
        }

        public void Play()
        {

            while (IsRunning())
            {
                attacker.Attack(defender);
                _currentTurn++;
                if (!IsRunning())
                {
                    break;
                }
                SwapRoles();
            }

            DeclareWinner();
          
        }

        private void SwapRoles()
        {
            var newDefender = attacker.AsDefender();
            var newAttacker = defender.AsAttacker();
            attacker = newAttacker;
            defender = newDefender;
        }

        private bool IsRunning()
        {
            return defender.IsStillAlive() || _currentTurn < 20;
        }

        private void DeclareWinner()
        {
            
        }
    }
}
