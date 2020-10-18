using Assets.Scripts.Tutorial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.StateMachine
{
    public class TutorialStateMachine : StateMachine
    {
        private State mStartState;
        private State mPauseState;

        public void Initialize(State startingState)
        {
            base.Initialize(startingState);

            mStartState = startingState;
        }

        public void Start()
        {
            

            Debug.Log("StateMachineTutorial :Start ");
            mPauseState = new PauseState(null, this);

            if (mStartState != null)
            {

                ChangeState(mStartState);
            }
        }

        public void Stop() {
            Debug.Log("StateMachineTutorial :Stop ");
            ChangeState(mPauseState);
        }

        public void Restart()
        {
            Debug.Log("StateMachineTutorial :Restart ");
        }
    }
}
