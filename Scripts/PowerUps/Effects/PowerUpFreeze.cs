using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.Scripts.PowerUpManager;

namespace Assets.Scripts.PowerUps.Effects
{
    [CreateAssetMenu(fileName = "PowerUpFreeze", menuName = "PowerUps/PowerUpFreeze")]

    class PowerUpFreeze : PowerUpEffectBase
    {

        //[SerializeField] override public Sprite IconSprite { get; }

        public PowerUpFreeze() : base()
        {
        }

        public override void EffectUpdate()
        {
            //Debug.Log("magnet active on scene "+mSubScene.name);


        }

        public override List<int> GetAffectedSubScenes(int index)
        {
            return new List<int>(index);
        }

        public override void OnFinish()
        {
            //if (mSubScene == null)
            //    throw new Exception("sub scene should not be null");

            Debug.Log("finished freeze");
        }

        public override string GetFinishEvent()
        {
            return ApplicationConstants.EVENT_FREEZE_OVER;
        }

        public override void OnStart()
        {
            //mSubScene = subScene;
            //mSubScene.SetPowerUpEffect(mPowerUpType, true);
            Debug.Log("Started freeze");
        }

        
    }
}
