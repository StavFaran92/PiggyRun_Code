using Assets.Scripts.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Tutorial
{
    class PauseState : State
    {
        public PauseState(ICharacter character, TutorialStateMachine stateMachine) : base(character, stateMachine)
        {
        }
    }
}
