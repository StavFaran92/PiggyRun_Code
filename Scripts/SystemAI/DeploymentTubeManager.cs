using Assets.Scripts.SequenceCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.SystemAI
{
    public class DeploymentTubeManager
    {
        public static DeploymentTubeManager Instance = new DeploymentTubeManager();

        //public delegate void TestDelegate(); // This defines what type of method you're going to call.
        //public TestDelegate m_methodToCall; // This is the variable holding the method you're going to call.

        //private List<IListener> listeners;

        private DeploymentTube[] deploymentTubes;

        public DeploymentTubeManager()
        {
            deploymentTubes = new DeploymentTube[ApplicationConstants.NumberOfScenes];

            //m_methodToCall = OnComplete;

            //listeners = new List<IListener>();
        }

        public void Init()
        {
            //This should be done here because Deployment tube will look for a scene which is a monobehavour
            for (int i = 0; i < ApplicationConstants.NumberOfScenes; i++)
            {
                deploymentTubes[i] = new DeploymentTube(i);
            }

            //--Handle the event firing
            GameEventManager.Instance.CallEvent(ApplicationConstants.EVENT_DEPLOYMENT_TUBE_MANAGER_FINISH_INIT, ActionParams.EmptyParams);
        }

        public void ApplyRequestToTube(int i, Action<Action> action, IActor actor)
        {
            DeploymentTube tube = deploymentTubes[i];
            if (tube == null)
            {
                Debug.Log("Tube " + i + " cannot be found");
                return;
            }
            tube.ApplyRequest(action, actor);
        }

        //public void ApplyRequestToTubeAsync(int i)
        //{
        //    DeploymentTube tube = deploymentTubes[i];
        //    if (tube == null)
        //    {
        //        Debug.Log("Tube " + i + " cannot be found");
        //        return;
        //    }
        //    tube.ApplyRequest();
        //}

        //    public void ApplyRequest(List<GameEvent>[] gameEventsToDeploy, Action callback)
        //    {
        //        CountDownRoot holder = new CountDownRoot(callback);
        //        float timeDelay = 0;


        //        for (int i=0; i< gameEventsToDeploy.Length; i++ )
        //        {

        //            if (deploymentTubes.Length < i)
        //            {
        //                throw new Exception("Illegal tube index was specified");
        //            }

        //            deploymentTubes[i].ApplyRequest(gameEventsToDeploy[i], holder, ref timeDelay);
        //        }


        //        //foreach (GameEvent gEvent in gameEventsToDeploy)
        //        //{
        //        //    int index = gEvent.GetSceneIndex();

        //        //    if(deploymentTubes.Length < index)
        //        //    {
        //        //        throw new Exception("Illegal tube index was specified");
        //        //    }


        //        //    //Something smart should be done here


        //        //    deploymentTubes[index].ApplyRequest(gEvent);
        //        //}
        //    }

        //    //private void AddListener(IListener listener)
        //    //{
        //    //    listeners.Add(listener);
        //    //}

        //    //private void OnComplete()
        //    //{
        //    //    foreach(IListener listener in listeners)
        //    //    {
        //    //        listener.Dispatch();
        //    //    }
        //    //    listeners.Clear();
        //    //}
        //}

    }
}
