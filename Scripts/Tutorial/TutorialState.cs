using Assets.Scripts.Communication.BluePrints;
using Assets.Scripts.SceneData;
using Assets.Scripts.SequenceCommand;
using Assets.Scripts.StateMachine;
using Assets.Scripts.SystemAI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Tutorial
{
    [CreateAssetMenu(fileName = "New Tutorial State", menuName = "Tutorial/New Tutorial State")]
    internal class TutorialState : ScriptableState, IActor
    {

        CountDownRoot holder2;

        private enum PlayerActionEnum
        {
            Jump,
            Slide,
            DoubleJump
        }

        [SerializeField]
        private PlayerActionEnum playerAction;

        [SerializeField]
        private Sprite TutorialImage;

        [SerializeField]
        private PREvent[] Events;

        [SerializeField]
        private int PrimaryScene;

        [SerializeField]
        private float delay;

        private string waitForEvent;

        private void Init()
        {
            if (playerAction.Equals(PlayerActionEnum.Jump))
            {
                waitForEvent = ApplicationConstants.EVENT_PLAYER_JUMP;
            }
            else if (playerAction.Equals(PlayerActionEnum.Slide))
            {
                waitForEvent = ApplicationConstants.EVENT_PLAYER_SLIDE;
            }
            else if (playerAction.Equals(PlayerActionEnum.DoubleJump))
            {
                waitForEvent = ApplicationConstants.EVENT_PLAYER_DOUBLE_JUMP;
            }
        }



        public override void Enter()
        {
            Init();

            //myEvent =new UnityEvent( GameEventManager.Instance.onPlayerJumpEvent);

            //--Display the fatui image
            //StartCoroutine(ExecuteAfterTime(10))
            TutorialSystem.Instance.DisplayImageOnPlaceHolder(TutorialImage, PrimaryScene, waitForEvent, delay);

            //--Instantiate the constructs using the json data

            //--Create a new list
            List<GameEvent>[] gameEvents = new List<GameEvent>[ApplicationConstants.NumberOfScenes];

            //--Initialize the lists
            for (int i = 0; i < ApplicationConstants.NumberOfScenes; i++)
            {
                gameEvents[i] = new List<GameEvent>();
            }

            //--Populate the lists using the PREvent data
            foreach (PREvent e in Events)
            {
                gameEvents[e.scene].Add(GameEventFactory.GetIntance().GenerateOne(e.type, e.level, e.index));
            }

            holder2 = new CountDownRoot(WakeUp);

            for (int i = 0; i < gameEvents.Length; i++)
            {
                //--If there are game events to deploy
                if (gameEvents[i].Count != 0)
                {


                    DeploymentTubeManager.Instance.ApplyRequestToTube(i, (Action callback) =>
                    {
                        Debug.Log("EventSelector: ApplyRequestToTube");

                        CountDownRoot holder = new CountDownRoot(callback);

                        holder2.IncreaseByOne();

                        //BluePrintConstruct bluePrint = LevelConstructUtil.ConvertFromLevelConstructToBluePrint(gameEvents[i]);

                        SubSceneMultiton.Instance.GetInstance(i).GetLocalLevelBuilder().Build(gameEvents[i], Vector3.zero, holder);
                    }, this);
                }
            }

            ////--Deploy request to tube.
            //for (int i = 0; i < ApplicationConstants.NumberOfScenes; i++)
            //{
            //    BluePrintConstruct bluePrint = LevelConstructUtil.ConvertFromLevelConstructToBluePrint(gameEvents[i]);

            //    DeploymentTubeManager.Instance.ApplyRequestToTube(i, TutorialSystem.Instance.MoveToNexTStep, this);
            //}

            
        }

        void WakeUp()
        {
            TutorialSystem.Instance.MoveToNexTStep();
        }

        

        IEnumerator ExecuteAfterTime(float time)
        {
            yield return new WaitForSeconds(time);

            // Code to execute after the delay
        }

        public override void Exit()
        {
            //throw new NotImplementedException();
        }

        public override void HandleInput()
        {
            throw new NotImplementedException();
        }

        public override void LogicUpdate()
        {


            //throw new NotImplementedException();
        }

        public override void PhysicsUpdate()
        {
            //throw new NotImplementedException();
        }

        public override void TryPerform(PRActions activity)
        {
            throw new NotImplementedException();
        }

        public void notifyMe()
        {
            Debug.Log("TutorialState: notifyMe");

            holder2.DecreaseByOne();
        }
    }
}
