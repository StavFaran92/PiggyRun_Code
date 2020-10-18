using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SequenceCommand
{
    class CommandExecutor
    {

        private Queue<KeyValuePair<IActor, Action<Action>>> mJobs;

        private IActor mChachedActor;

        /// <summary>
        /// Constructor
        /// </summary>
        public CommandExecutor()
        {
            mJobs = new Queue<KeyValuePair<IActor, Action<Action>>>();
        }


        /// <summary>
        /// This function is used to apply a request to the tube, it can either triiger it's operate function
        /// or, if there are other functions prior, adds it's action to the job list
        /// </summary>
        /// <param name="action"></param>
        internal void ApplyRequest(Action<Action> action, IActor actor)
        {
            Debug.Log("CommandExecutor: ApplyRequest");

            bool shouldOperate = false;
            //-- If this is the first task given then the tube should start operate
            if (mJobs.Count == 0)
            {
                shouldOperate = true;
            }

            KeyValuePair<IActor, Action<Action>> job = new KeyValuePair<IActor, Action<Action>>(actor, action);

            mJobs.Enqueue(job);

            if (shouldOperate)
            {
                Operate();
            }
        }

        /// <summary>
        /// Operate is the core functionality of this instance, it is called first when the first job arrives
        /// and then it is called as a callback for each finishd job, when it has no more jobs available it
        /// cleans the list , resets the iterator and waits for the nexr batch
        /// </summary>
        void Operate()
        {
            Debug.Log("CommandExecutor: Operate");
            //I should inform here the actor that is waiting
            //action.Invoke();

            if (mJobs.Count != 0)
            {
                KeyValuePair<IActor, Action<Action>> job = mJobs.Peek();
                mChachedActor = job.Key;
                Action<Action> action = job.Value;
                if (mChachedActor != null && action != null)
                {
                    //--Invoke the next job
                    action.Invoke(OnComplete);
                }
            }
        }

        /// <summary>
        /// This function is called on complete of a job, it can be directly called after execution or 
        /// later as a callback called by the user
        /// </summary>
        void OnComplete()
        {
            Debug.Log("CommandExecutor: OnComplete");

            if (mChachedActor == null)
            {
                Debug.Log("Actor cannot be null");
                return;
            }

            mChachedActor.notifyMe();

            mJobs.Dequeue();

            Operate();
        }
    }
}
