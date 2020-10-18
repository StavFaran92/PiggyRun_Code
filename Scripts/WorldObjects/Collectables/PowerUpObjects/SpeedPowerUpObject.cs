using Assets.Scripts.WorldObjects.Collectables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    class SpeedPowerUpObject : PowerUpObject
    {
        ActionParams actionParams;

        private new void Start()
        {
            base.Start();

            actionParams = new ActionParams();
            actionParams.Put("speed", 30f);
        }

        public override void CollectableObjectEffect(Collider2D collision)
        {
            base.CollectableObjectEffect(collision);

            Debug.Log("Player " + worldIndex +" collected Speed!");

            m_Scene.GetEventManager().CallEvent(ApplicationConstants.EVENT_PLAYER_COLLECTED_SPEED, actionParams);
            //CommonRequests.RequestPowerUpActivation(PowerUpManager.PowerUpEffect.Magnet, worldIndex);
        }

        
    }
}
