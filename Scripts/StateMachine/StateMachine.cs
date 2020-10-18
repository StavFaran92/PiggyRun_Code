using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.StateMachine
{
    public class StateMachine
    {
        private IState CurrentState { get; set; }

        public void Initialize(IState startingState)
        {
            CurrentState = startingState;
            startingState.Enter();
        }

        public void UpdateState()
        {
            CurrentState.LogicUpdate();
            CurrentState.PhysicsUpdate();
        }

        public void TryPerform(PRActions activity)
        {
            CurrentState.TryPerform(activity);
        }

        public void ChangeState(IState newState)
        {
            CurrentState.Exit();

            CurrentState = newState;
            newState.Enter();
        }

        internal IState GetState()
        {
            return CurrentState;
        }
    }
}
