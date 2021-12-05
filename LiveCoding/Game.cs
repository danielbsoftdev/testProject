using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.LiveCoding
{
    class Game : IGame
    {
        private ICharacter attacker;
        private ICharacter defender;
        private readonly int _maxTurns;
        private int _currentTurn;

        public Game(ICharacter erou, ICharacter bestie, int maxTurns)
        {
            _maxTurns = maxTurns;
            _currentTurn = 0;
            if (erou.IsFasterThan(bestie))
            {
                attacker = erou;
                defender = bestie;
            }
            else
            {
                attacker = bestie;
                defender = erou;
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
