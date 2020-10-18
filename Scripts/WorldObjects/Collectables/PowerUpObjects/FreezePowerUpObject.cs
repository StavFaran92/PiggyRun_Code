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
    class FreezePowerUpObject : PowerUpObject
    {
        public override void CollectableObjectEffect(Collider2D collision)
        {
            base.CollectableObjectEffect(collision);

            Debug.Log("Player collected freeze!");

            m_Scene.GetEventManager().CallEvent(ApplicationConstants.EVENT_PLAYER_COLLECTED_FREEZE, ActionParams.EmptyParams);

            //CommonRequests.RequestPowerUpActivation(PowerUpManager.PowerUpEffect.Freeze, worldIndex);
        }

        
    }
}
