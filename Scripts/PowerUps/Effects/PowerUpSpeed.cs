using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.Scripts.PowerUpManager;

namespace Assets.Scripts.PowerUps.Effects
{
    [CreateAssetMenu(fileName = "PowerUpSpeed", menuName = "PowerUps/PowerUpSpeed")]

    class PowerUpSpeed : PowerUpEffectBase
    {


        public PowerUpSpeed() : base()
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

        public override string GetFinishEvent()
        {
            return ApplicationConstants.EVENT_SPEED_OVER;
        }

        public override void OnFinish()
        {
            Debug.Log("finished speed");
        }

        public override void OnStart()
        {
            Debug.Log("Started speed");
        }
    }
}
