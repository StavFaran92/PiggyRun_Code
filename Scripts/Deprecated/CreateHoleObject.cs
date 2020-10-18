using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Commands;
using Assets.Scripts.StateMachine;
using UnityEngine;

namespace Assets.Scripts.WorldObjects
{
    class CreateHoleObject : WorldFiniteObject// , ISender
    {
        private static GameManager _gameManager;

        public new void Start()
        {
            base.Start();
            _gameManager = GameManager.GetInstance();
        }

        //public IReceiver GetReceiver(ReceiverId receiver)
        //{
        //    return m_Scene;
        //}

        public void Send(Request request)
        {
            _gameManager.SendMessage("AddRequest", request);

        }

        //private Request GenerateRequest()
        //{
        //    ICommand command = CommandFactory.CreateCommandBehaviour()
        //        .WithAction(PRActions.SCENE_CREATE_HOLE)
        //        .Build();

        //    Request request = Request.CreateRequest()
        //        .WithReceiver(m_Scene)
        //        .WithCommand(command)
        //        .Build();

        //    return request;
        //}

        public new void Update()
        {
            base.Update();

            float width = m_Scene.mScreenWidthInWorld;

            if (Utils.IsObjectLeftOfRightBound(transform, width))
            {
                //Send(GenerateRequest());

                m_Scene.GetEventManager().CallEvent(ApplicationConstants.EVENT_CREATE_HOLE, ActionParams.EmptyParams);

                base.DestroyObject();

            }
        }
    }
}
