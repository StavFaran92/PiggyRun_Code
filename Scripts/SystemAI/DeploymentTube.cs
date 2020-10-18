using Assets.Scripts.Commands;
using Assets.Scripts.Communication.BluePrints;
using Assets.Scripts.SceneData;
using Assets.Scripts.SequenceCommand;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.SystemAI
{
    internal class DeploymentTube //: ISender
    {
        private int mCapacity = 3; // TODO fix
        SubScene mScene;

        private int mCurrentTask = 0;

        private CommandExecutor mCommandExecutor;

        public DeploymentTube(int index)
        {
            mScene = SubSceneMultiton.Instance.GetInstance(index) ;

            if(!mScene)
            {
                throw new Exception("Scene cannot be null!");
            }

            mCommandExecutor = new CommandExecutor();
        }

        /// <summary>
        /// Apply a new request for the tube
        /// </summary>
        /// <param name="action"></param>
        internal void ApplyRequest(Action<Action> action, IActor actor)
        {
            mCommandExecutor.ApplyRequest(action, actor);
        }

        internal int GetCapacity()
        {
            return mCapacity;
        }

        void SetCapacity(int capacity)
        {
            mCapacity = capacity;
        }

        public void Send(Request request)
        {
            GameManager.GetInstance().SendMessage("AddRequest", request);
        }

    }
}