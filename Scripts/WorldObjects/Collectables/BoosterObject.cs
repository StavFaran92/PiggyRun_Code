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
    class BoosterObject : CollectableObject
    {
        [SerializeField] private int amount;

        public override void CollectableObjectEffect(Collider2D collision)
        {
            

            Debug.Log("Player " + worldIndex +" collected booster!");

            ActionParams actionParams = new ActionParams();
            actionParams.Put("amount", amount);

            m_Scene.GetEventManager().CallEvent(ApplicationConstants.EVENT_PLAYER_COLLECTED_BOOSTER, actionParams);
            //CommonRequests.RequestPowerUpActivation(PowerUpManager.PowerUpEffect.Magnet, worldIndex);
        }

        
    }
}
