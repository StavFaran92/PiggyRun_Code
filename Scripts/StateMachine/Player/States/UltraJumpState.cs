using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.StateMachine
{
    public class UltraJumpState : PlayerState
    {
        

        public UltraJumpState(ICharacter character, StateMachine stateMachine) : base(character, stateMachine)
        {
            this.player = (Player)character;
        }

        public override void TryPerform(PRActions activity)
        {
            switch (activity)
            {
                case PRActions.PLAYER_HIT_GROUND:
                    stateMachine.ChangeState(player.run);
                    break;
                case PRActions.TOUCH_SWIPE_DOWN:
                case PRActions.MOUSE_RIGHT_BUTTON:
                    stateMachine.ChangeState(player.powerDescend);
                    break;
            }

        }

        public override void Enter()
        {
            base.Enter();
            player.PlayerEnterUltraJump();
        }

        public override void HandleInput()
        {
            base.HandleInput();

        }

        public override void LogicUpdate()
        {
            player.UltraJumpDecorate();
        }
    }
}
