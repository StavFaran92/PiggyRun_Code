using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.StateMachine
{
    public interface IState
    {



         void Enter();

        void TryPerform(PRActions activity);

         void HandleInput();

        void LogicUpdate();

        void PhysicsUpdate();

        void Exit();
    }
}
