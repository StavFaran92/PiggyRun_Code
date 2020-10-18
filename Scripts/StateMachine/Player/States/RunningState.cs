using Assets.Scripts.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.StateMachine
{
    public class RunningState : PlayerState
    {
       

        public RunningState(ICharacter character, StateMachine stateMachine) : base(character, stateMachine)
        {
            this.player = (Player)character;
        }

        public override void TryPerform(PRActions activity)
        {
            switch(activity)
            {
                case PRActions.TOUCH_SWIPE_UP:
                case PRActions.MOUSE_LEFT_BUTTON:
                    stateMachine.ChangeState(player.jump);
                    break;
                case PRActions.TOUCH_SWIPE_DOWN:
                case PRActions.MOUSE_RIGHT_BUTTON:
                    stateMachine.ChangeState(player.slide);
                    break;
                case PRActions.PLAYER_LEAPING:
                    stateMachine.ChangeState(player.descend);
                    break;
            }

        }

        public override void Enter()
        {
            base.Enter();
            player.PlayerEnterRun();
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
