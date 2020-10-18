using Assets.Scripts.StateMachine;
using Assets.Scripts.SystemAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.StateMachine
{
    public class NeutralState : SystemAIState
    {
       

        public NeutralState(ICharacter character, StateMachine stateMachine) : base(character, stateMachine)
        {
            this.AI = (SystemAIManager)character;
        }

        public override void TryPerform(PRActions activity)
        {
            //switch(activity)
            //{
            //    case SystemAIActivity.:
            //        stateMachine.ChangeState(AI.charge);
            //        break;
            //    case PlayerActivity.Leaping:
            //        stateMachine.ChangeState(AI.descend);
            //        break;
            //}

        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void HandleInput()
        {
            base.HandleInput();

        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }
    }
}
