﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.StateMachine
{
    public class PlayerState : State
    {

        public Player player;

        public PlayerState(ICharacter character, StateMachine stateMachine) : base(character, stateMachine)
        {
            player = (Player)character;
        }

        public override void TryPerform(PRActions activity)
        {
            base.TryPerform(activity);

        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void Exit()
        {
            base.Exit();
            //player.ResetMoveParams();
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            //player.Move(verticalInput * speed, horizontalInput * rotationSpeed);
        }
    }
}
