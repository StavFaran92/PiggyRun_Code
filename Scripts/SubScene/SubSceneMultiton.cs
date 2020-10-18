using Assets.Scripts.SystemAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SceneData
{
    class SubSceneMultiton : MonoBehaviour
    {
        private static readonly int numberOfInstances = ApplicationConstants.NumberOfScenes;
        //private static SubSceneMultiton mInstance;
        private static SubScene[] scenes
            = new SubScene[numberOfInstances];

        private static SubSceneMultiton mInstance;

        public SubScene GetInstance(int index)
        {
            return scenes[index];
        }

        public SubScene[] GetAll()
        {
            return scenes;
        }

        public static SubSceneMultiton Instance {
            get
            {
                if (mInstance == null)
                {
                    GameObject go = new GameObject();
                    mInstance = go.AddComponent<SubSceneMultiton>();
                }
                return mInstance;
            }
        }

        public static void SetInstance(int index, SubScene scene)
        {
            scenes[index] = scene;
        }
    }
}
