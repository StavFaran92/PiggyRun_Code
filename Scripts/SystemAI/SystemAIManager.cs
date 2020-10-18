using Assets.Scripts.StateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SystemAI
{
    public class SystemAIManager : MonoBehaviour, ICharacter
    {
        public static SystemAIManager m_Instance;

        EventPoolFiller mEventPoolFiller;
        EventSelector mEventSelector;
        DifficultyManager difficultyManager;

        [SerializeField]
        private bool isActive = false;

        private void Start()
        {
            //-- Singleton
            m_Instance = this;

            mEventPoolFiller = EventPoolFiller.Instance;
            mEventSelector = EventSelector.Instance;
            difficultyManager = DifficultyManager.Instance;

            //GameEventManager.Instance.ListenToEvent(ApplicationConstants.EVENT_END_OF_TUTORIAL, Activate);
            //if(!TutorialSystem.Instance.IsActive)
            //    GameEventManager.Instance.ListenToEvent(ApplicationConstants.EVENT_GAME_CINEMATICS_OVER, Activate);



        }

        public void Activate(string eventName)
        {
            //TODO FIX
            //This is a hack, I give the game manager method of activation since it is the one registered to listen
            GameEventManager.Instance.RemoveListenerFromEvent(ApplicationConstants.EVENT_END_OF_TUTORIAL, GameManager.GetInstance().ActivateSystemAI);
            GameEventManager.Instance.RemoveListenerFromEvent(ApplicationConstants.EVENT_GAME_CINEMATICS_OVER, GameManager.GetInstance().ActivateSystemAI);
            if (isActive)
            {
                //isWaiting = false;
                CanOperate();
            }
        }

        public void CanOperate()
        {
            Debug.Log("SystemAIManager: CanOperate()");

            float waitForSeconds = 0;// UnityEngine.Random.Range(0f, 2f);
            StartCoroutine("OnCoroutine", waitForSeconds);
        }

        private void ShouldFillPool()
        {
            mEventPoolFiller.ShouldFillPool("eventPool");
            //mEventPoolFiller.ShouldFillPool(EventPoolHolder<GameEvent>.m_GoodsPool);
        }

        IEnumerator OnCoroutine(float waitForSeconds)
        {
            yield return new WaitForSeconds(waitForSeconds);

            //difficultyManager.GiveMoneyTo(mEventSelector);

            //ShouldFillPool();


            mEventSelector.Operate();

                
            
        }
    }
}
