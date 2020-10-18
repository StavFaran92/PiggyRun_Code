using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Services
{
    class DependencyLoader
    {

        private List<Action> mOnLoadActions;

        public void Awake()
        {
            mOnLoadActions = new List<Action>();
        }

        public void Run()
        {
            foreach (Action action in mOnLoadActions)
            {
                action.Invoke();
            }
            mOnLoadActions.Clear();
        }

        public void AddAction(Action action)
        {
            mOnLoadActions.Add(action);
        }
    }
}
