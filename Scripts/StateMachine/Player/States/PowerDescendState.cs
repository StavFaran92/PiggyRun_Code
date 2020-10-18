using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.StateMachine
{
    public class PowerDescendState : PlayerState
    {
        

        public PowerDescendState(ICharacter character, StateMachine stateMachine) : base(character, stateMachine)
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


            }

        }

        public override void Enter()
        {
            base.Enter();
            player.PlayerEnterPowerDescend();
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
