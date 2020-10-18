//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEngine;

//namespace Assets.Scripts.StateMachine
//{
//    public class ChargingState : PlayerState
//    {
//        public ChargingState(ICharacter character, StateMachine stateMachine) : base(character, stateMachine)
//        {
//        }

//        public override void TryPerform(int activity)
//        {

//            switch (activity)
//            {
//                case PlayerActivity.TouchReleased:
//                    stateMachine.ChangeState(player.jump);
//                    break;
//                case PlayerActivity.ReachMaxCharge:
//                    stateMachine.ChangeState(player.jump);
//                    break;
//            }

//        }

//        public override void Enter()
//        {
//            base.Enter();

//            player.m_PlayerCharge = 0;
//        }

//        public override void HandleInput()
//        {
//            base.HandleInput();

//        }

//        public override void LogicUpdate()
//        {
//            base.LogicUpdate();

//            player.m_PlayerCharge += player.m_ChargeAmount * Time.deltaTime; 

//            if (player.m_PlayerCharge >= 1)
//            {
//                TryPerform(PlayerActivity.ReachMaxCharge);
//            }
//        }
//    }
//}
