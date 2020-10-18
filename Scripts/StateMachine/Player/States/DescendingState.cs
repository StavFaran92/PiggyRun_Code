using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.StateMachine
{
    public class DescendingState : PlayerState
    {
        

        public DescendingState(ICharacter character, StateMachine stateMachine) : base(character, stateMachine)
        {
            this.player = (Player)character;
        }

        public override void TryPerform(PRActions activity)
        {

            switch (activity)
            {
                case PRActions.TOUCH_SWIPE_UP:
                case PRActions.MOUSE_LEFT_BUTTON:
                    stateMachine.ChangeState(player.ultraJump);
                    break;
                case PRActions.TOUCH_SWIPE_DOWN:
                case PRActions.MOUSE_RIGHT_BUTTON:
                    stateMachine.ChangeState(player.powerDescend);
                    break;
                case PRActions.PLAYER_HIT_GROUND:
                    stateMachine.ChangeState(player.run);
                    break;


            }

        }

        public override void Enter()
        {
            base.Enter();
            player.PlayerEnterDescend();
        }

        public override void HandleInput()
        {
            base.HandleInput();

        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            player.PlayerEnterDescend();
        }
    }
}
