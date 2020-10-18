using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.StateMachine
{
    class ScriptableState : ScriptableObject, IState
    {
        public virtual void Enter()
        {
            throw new NotImplementedException();
        }

        public virtual void Exit()
        {
            throw new NotImplementedException();
        }

        public virtual void HandleInput()
        {
            throw new NotImplementedException();
        }

        public virtual void LogicUpdate()
        {
            throw new NotImplementedException();
        }

        public virtual void PhysicsUpdate()
        {
            throw new NotImplementedException();
        }

        public virtual void TryPerform(PRActions activity)
        {
            throw new NotImplementedException();
        }
    }
}
