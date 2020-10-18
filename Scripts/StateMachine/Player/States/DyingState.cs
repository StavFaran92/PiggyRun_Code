using Assets.Scripts.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using UnityEngine;

namespace Assets.Scripts.StateMachine
{
    public class DyingState : PlayerState
    {
        const float mDuration = 1f;
        float mTimer = 0;

        public DyingState(ICharacter character, StateMachine stateMachine) : base(character, stateMachine)
        {
            this.player = (Player)character;
        }

        public override void TryPerform(PRActions activity)
        {
            //switch(activity)
            //{
            //    case PRActions.TOUCH_SWIPE_UP:
            //    case PRActions.MOUSE_LEFT_BUTTON:
            //        player.PlayerExitSlide();
            //        stateMachine.ChangeState(player.jump);
            //        break;
            //    case PRActions.PLAYER_SLIDE_OVER:
            //        player.PlayerExitSlide();
            //        stateMachine.ChangeState(player.run);
            //        break;
            //}

        }

        public override void Enter()
        {
            base.Enter();

            //player.PlayerEnterDie();
        
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
