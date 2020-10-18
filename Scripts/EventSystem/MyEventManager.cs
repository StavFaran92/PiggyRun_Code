using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public abstract class MyEventManager
    {
        protected Dictionary<string, List<Action<string, ActionParams>>> mEventsMap =
            new Dictionary<string, List<Action<string, ActionParams>>>();

        public void RemoveListenerFromEvent(string eventName, Action<string,ActionParams> action)
        {
            mEventsMap.TryGetValue(eventName, out var actions);

            if (actions == null)
            {
                Debug.Log("Wrong event name specified" + eventName);
                return;
            }

            actions.Remove(action);
        }

        public void ListenToEvent(string eventName, Action<string,ActionParams> action)
        {
            List<Action<string, ActionParams>> actions;

            if (!mEventsMap.ContainsKey(eventName))
            {
                actions = new List<Action<string, ActionParams>>();
                mEventsMap.Add(eventName, actions);
            }
            else
            {
                mEventsMap.TryGetValue(eventName, out actions);

                if (actions == null)
                {
                    Debug.Log("Wrong event name specified: " + eventName);
                    return;
                }
            }

            actions.Add(action);
        }

        public void CallEvent(string eventName, ActionParams actionParams)
        {
            mEventsMap.TryGetValue(eventName, out var actions);

            if (actions != null)
            {
                //--we iterate the list from end to start because invoke removes elements from the list
                for (int i = actions.Count - 1; i >= 0; i--)
                {
                    actions[i]?.Invoke(eventName, actionParams);
                }
            }

            
        }

        
    }
}
