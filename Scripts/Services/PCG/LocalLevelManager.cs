using Assets.Scripts.Painters;
using Assets.Scripts.SequenceCommand;
using Assets.Scripts.SystemAI;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// This is the local level manager, it is responsible of generating a level on startup 
    /// and switch levels as the game progresses
    /// </summary>
    class LocalLevelManager : MonoBehaviour, IActor
    {
        [SerializeField] private Level[] mLevels;
        [SerializeField] private int mCurrentLevel;

        private GameObject mCurrentBackground;
        private GameObject mCurrentForeground;

        private SubScene mScene;

        private void Start()
        {
            //reset current level
            mCurrentLevel = 0;

            //get scene
            mScene = GetComponentInParent<SubScene>();

            //get current level from data
            Level currentLevel = mLevels[mCurrentLevel];

            //spawn a background for the game
            mCurrentBackground = ObjectPoolManager.Instance.Spawn(currentLevel.GetBackground().name,
                mScene.transform.position + new Vector3(0, 0, 10));

            //set it to be a child of scene
            mCurrentBackground.transform.parent = mScene.transform;

            //spawn a foreground for the game
            mCurrentForeground = ObjectPoolManager.Instance.Spawn(currentLevel.GetForeground().name,
                mScene.transform.position + new Vector3(0, -15.68f, 5),
                mScene.transform);

            //set it to be a child of scene
            mCurrentForeground.transform.parent = mScene.transform;

            mScene.GetServiceManager().CallActionWhenServiceIsLive(ApplicationConstants.SERVICE_LOCAL_LEVEL_BUILDER, 
                () => {SetConfigurationInBuilder(currentLevel); } );

            //mScene.GetEventManager().ListenToEvent(ApplicationConstants.BUILDER_IS_INITIALIZED, (string arg1, ActionParams arg2) => {
            //    SetConfigurationInBuilder(currentLevel);
            //});

            //mScene.GetEventManager().

            mScene.GetEventManager().ListenToEvent(ApplicationConstants.EVENT_CHANGE_TO_NEXT_WORLD, ChangeToNextLevel);

        }

        

        void Operate()
        {

        }

        void ChangeToNextLevel(string eventName, ActionParams actionParams)
        {
            Debug.Log("ChangeToNextLevel()");

            //Save old values
            GameObject oldBackground = mCurrentBackground;
            GameObject oldForeground = mCurrentForeground;

            //move the current level by 1 with modulo
            mCurrentLevel++;
            mCurrentLevel = mCurrentLevel  % mLevels.Length;

            Level level = mLevels[mCurrentLevel];

            GameObject backgroundPrefab = level.GetBackground();
            if(backgroundPrefab == null)
            {
                Debug.Log("background prefab cannot be null");
            }
            //TODO FIX
            //Create the prefab in the correct place
            //ObjectPoolManager.Instance.Spawn();
            mCurrentBackground = ObjectPoolManager.Instance.Spawn(backgroundPrefab.name, mScene.transform.position + 
                new Vector3(oldBackground.GetComponentInChildren<Renderer>().bounds.size.x, 0, 10));

            mCurrentBackground.transform.parent = mScene.transform;

            //move the cuurent bg prefab away
            mCurrentBackground.transform.DOMoveX(mCurrentBackground.transform.position.x - 
                oldBackground.GetComponentInChildren<Renderer>().bounds.size.x, 3);

            //move the new prefab into the scene
            oldBackground.transform.DOMoveX(oldBackground.transform.position.x - 
                oldBackground.GetComponentInChildren<Renderer>().bounds.size.x, 3)
                .OnComplete(()=> {
                    ObjectPoolManager.Instance.Destroy(oldBackground.name, oldBackground); });


            GameObject foregroundPrefab = level.GetForeground();
            if (foregroundPrefab == null)
            {
                Debug.Log("Foreground prefab cannot be null");
            }

            //Spawn a new foreground
            mCurrentForeground = ObjectPoolManager.Instance.Spawn(foregroundPrefab.name, mScene.transform.position +
                new Vector3(oldForeground.GetComponentInChildren<Renderer>().bounds.size.x, -15.68f, 5), mScene.transform);

            //set it as child of scene
            mCurrentForeground.transform.parent = mScene.transform;

            //move the cuurent fg prefab away
            mCurrentForeground.transform.DOMoveX(mCurrentForeground.transform.position.x -
                oldForeground.GetComponentInChildren<Renderer>().bounds.size.x, 3);

            //move the new prefab into the scene
            oldForeground.transform.DOMoveX(oldForeground.transform.position.x -
                oldForeground.GetComponentInChildren<Renderer>().bounds.size.x, 3)
                .OnComplete(() => {
                    ObjectPoolManager.Instance.Destroy(oldForeground.name, oldForeground);
                });

            SetConfigurationInBuilder(level);
        }

        void SetConfigurationInBuilder(Level level)
        {
            //get builder from
            IBuilder builder = mScene.GetServiceManager().GetService<LocalLevelBuilder_v2>(ApplicationConstants.SERVICE_LOCAL_LEVEL_BUILDER);

            if (builder == null)
            {
                Debug.Log("Builder cannot be null");
                return;
            }

            Dictionary<string, TilePainter> configMap = new Dictionary<string, TilePainter>();
            foreach (ColorToTilePainter ctt in level.GetColorToTileMap())
            {
                configMap.Add(ColorUtility.ToHtmlStringRGB(ctt.GetColor()).ToLower(), ctt.GetTilePainter());
            }

            //set builder new configuration
            builder.SetColorToTileDataDictionaryConfig(configMap);
        }

        public void notifyMe()
        {
            Debug.Log("LocalLevelManager: notifyMe");
        }
    }
}
