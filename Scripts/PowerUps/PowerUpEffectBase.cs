using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.Scripts.PowerUpManager;

namespace Assets.Scripts.PowerUps
{
    
    public abstract class PowerUpEffectBase : ScriptableObject
    {
        //protected SubScene mSubscene;
         //abstract public float Time { get; }

        [SerializeField] public Sprite IconSprite;
        [SerializeField] public float Time;
        [SerializeField] public PowerUpEffect Effect;

        //abstract public PowerUpEffect Effect { get; }
        //abstract public Sprite IconSprite { get; }

        public PowerUpEffectBase()
        {
            //this.mSubscene = subScene;
        }

        //public void Init(SubScene subScene)
        //{
        //    this.mSubscene = subScene;
        //}

        //public virtual float GetTime()
        //{
        //    return mTime;
        //}

        public abstract void EffectUpdate();

        public abstract List<int> GetAffectedSubScenes(int index);

        public abstract void OnFinish();
        public abstract string GetFinishEvent();

        public abstract void OnStart();

        public Sprite GetIconSprite()
        {
            return IconSprite;
        }
    }
}
