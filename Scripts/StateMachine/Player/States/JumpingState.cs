using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.StateMachine
{
    public class JumpingState : PlayerState
    {
        

        public JumpingState(ICharacter character, StateMachine stateMachine) : base(character, stateMachine)
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
                case PRActions.PLAYER_REACH_MAX_ALT:
                    stateMachine.ChangeState(player.descend);
                    break;
            }

        }

        public override void Enter()
        {
            base.Enter();
            player.PlayerEnterJump();
            
        }

        public override void HandleInput()
        {
            base.HandleInput();

        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (player.mRigidBody2D.velocity.y < 0)
            {
                TryPerform(PRActions.PLAYER_REACH_MAX_ALT);
            }

        }
    }
}
