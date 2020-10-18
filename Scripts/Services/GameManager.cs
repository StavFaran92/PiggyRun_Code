using Assets.Scripts.Commands;
using Assets.Scripts.SceneData;
using Assets.Scripts.SequenceCommand;
using Assets.Scripts.Services;
using Assets.Scripts.SystemAI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    

    public enum ReceiverId { Players, Scenes, GameManager, HUD }

    class GameManager : MonoBehaviour, IActor//, IReceiver
    {
        private readonly string TAG = "GameManager";
        //public enum Actions
        //{
        //    GameOver = 0
        //}

        private static GameManager mInstance;

        [SerializeField] private GameObject mGameOverMenu;
        [SerializeField] private GameObject mGameFinishedMenu;
        private SubScene[] m_SubScenes;

        private Queue<Request> _requestQueue;
        private int mNumberOfFinishedPigs;

        public static GameManager GetInstance()
        {
            return mInstance;
        }

        private void Awake()
        {
            //DontDestroyOnLoad(this);

            GameLoader loader = new GameLoader();

            mInstance = this;// gameObject.GetComponent<GameManager>();

            //io = IOHandler.GetIntance();

            //_sceneManagerScripts = new Scene[3];

            //_sceneManagers =  GameObject.FindGameObjectsWithTag("SceneManager");
            //for(int i=0; i<_sceneManagers.Length; i++)
            //{
            //    _sceneManagerScripts[i] = _sceneManagers[i].gameObject.GetComponent<Scene>();
            //}

            _requestQueue = new Queue<Request>();

            m_SubScenes = new SubScene[ApplicationConstants.NumberOfScenes];

            

            //mGameReceiversMap.Add(Receiver.Players, PlayerMovementManager.Instance.GetAll());
            //mGameReceiversMap.Add(Receiver.Scenes, SubSceneMultiton.Instance.GetAll());
            //mGameReceiversMap.Add(Receiver.HUD, ScoreHolder.Instance.GetAll());
            //mGameReceiversMap.Add(Receiver.GameManager, new IReceiver[] { _gameManager });


        }

        private void Start()
        {
            //--Initiliazers
            DeploymentTubeManager.Instance.Init();

            //--Get tutorial assign and register the AI to events accordingly
            int shouldPlay = PlayerPrefs.GetInt("Tutorial", -1);
            bool IsTutorialActive = false;

            if (shouldPlay == -1)
            {
                Debug.Log("could not find tutorial data");
            }
            else
            {
                IsTutorialActive = shouldPlay == 1;
            }

            ////TODO remove
            //IsTutorialActive = true;

            mNumberOfFinishedPigs = 0;

            if (IsTutorialActive)
            {
                //-- In case the tutorial is active we let the AI wait for the end of it
                GameEventManager.Instance.ListenToEvent(ApplicationConstants.EVENT_END_OF_TUTORIAL, ActivateSystemAI);
                GameEventManager.Instance.ListenToEvent(ApplicationConstants.EVENT_GAME_CINEMATICS_OVER, ActivateTutorial);
            }

            else
            {
                //-- In case the tutorial is NOT active we let it listen to end of cinematics
                GameEventManager.Instance.ListenToEvent(ApplicationConstants.EVENT_GAME_CINEMATICS_OVER, ActivateSystemAI);
            }

            AudioManager.instance.PlayMusicInLoop("bensound-jazzyfrenchy");

            GameEventManager.Instance.ListenToEvent(ApplicationConstants.EVENT_PLAYER_DIE, OnPlayerDie);
            GameEventManager.Instance.ListenToEvent(ApplicationConstants.EVENT_PLAYER_FINISHED_LEVEL, OnPlayerFinishLevel);
            //GameEventManager.Instance.ListenToEvent(ApplicationConstants.EVENT_CINEMATIC_DEATH_BY_HIT_OVER, DisplayGameOverMenu);
        }

        private void OnPlayerFinishLevel(string eventName, ActionParams arg2)
        {
            Debug.unityLogger.Log(TAG, "OnPlayerFinishLevel()");


            //--One more dead pig
            mNumberOfFinishedPigs++;

            //--if all of the pigs are dead finish the game
            if (mNumberOfFinishedPigs >= ApplicationConstants.NumberOfScenes)
            {
                Debug.unityLogger.Log(TAG, "Level Finished!");

                //--remove listener
                GameEventManager.Instance.RemoveListenerFromEvent(eventName, OnPlayerDie);


                Instantiate(mGameFinishedMenu).SetActive(true);
            }
        }

        public void ActivateSystemAI(string eventName, ActionParams actionParams)
        {
            SystemAIManager.m_Instance.Activate(eventName);
        }

        public void ActivateTutorial(string eventName, ActionParams actionParams)
        {
            GameEventManager.Instance.RemoveListenerFromEvent(ApplicationConstants.EVENT_GAME_CINEMATICS_OVER, ActivateTutorial);
            TutorialSystem.Instance.Activate();
        }

        public void RegisterSceneInGame(SubScene subScene, int index)
        {
            m_SubScenes[index] = subScene;
        }

        public void UnregisterSubScene(int index)
        {
            m_SubScenes[index] = null;
        }

        

        private void AddRequest(Request request)
        {
            _requestQueue.Enqueue(request);

        }

        private void ReadRequestsFromQueue()
        {
            while (_requestQueue.Count != 0)
            {
                Request request = _requestQueue.Dequeue();
                request.Execute();
            }

        }

        // Update is called once per frame
        void Update()
        {

            //io.Update();

            //This can be optimized using locks.
            ReadRequestsFromQueue();
        }



        //public void DelegateMethod(string method, [Optional] int index)
        //{
        //    _sceneManagerScripts[index].StartCoroutine(method, index);
        //}

        //internal IReceiver GetReceiver(ReceiverId receiver, int m_SceneIndex)
        //{
        //    SubScene subscene = m_SubScenes[m_SceneIndex];

        //    if(subscene)
        //    {
        //        return subscene.GetReceiver(receiver);
        //    }
        //    return null;


        //}

        private void OnPlayerDie(string eventName, ActionParams actionParams)
        {
            Debug.unityLogger.Log(TAG, "OnPlayerDie()");
            

            //--One more dead pig
            mNumberOfFinishedPigs++;

            //--if all of the pigs are dead finish the game
            if (mNumberOfFinishedPigs >= ApplicationConstants.NumberOfScenes)
            {
                Debug.unityLogger.Log(TAG, "Game Over");

                //--remove listener
                GameEventManager.Instance.RemoveListenerFromEvent(eventName, OnPlayerDie);


                Instantiate(mGameOverMenu).SetActive(true);
            }
            //Instantiate(mGameOverMenu).SetActive(true);
        }

        //public void TryPerform(PRActions ability)
        //{
        //    if (ability.Equals(PRActions.GAME_OVER))
        //    {
        //        Debug.Log("Game Over");

        //        //mGameOverMenu.SetActive(true);

        //        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //    }
        //}

        internal SubScene GetSubScene(int selectedSubScene)
        {
            return m_SubScenes[selectedSubScene];
        }

        public void notifyMe()
        {
            Debug.Log("GameManager: notifyMe");
        }
    }
}
