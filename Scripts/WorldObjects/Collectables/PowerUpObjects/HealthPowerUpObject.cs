﻿using Assets.Scripts.WorldObjects.Collectables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    class HealthPowerUpObject : PowerUpObject
    {
        public override void CollectableObjectEffect(Collider2D collision)
        {
            base.CollectableObjectEffect(collision);

            Debug.Log("Player " + worldIndex + " collected Health!");

            m_Scene.GetEventManager().CallEvent(ApplicationConstants.EVENT_PLAYER_COLLECTED_HEALTH, ActionParams.EmptyParams);
            //CommonRequests.RequestPowerUpActivation(PowerUpManager.PowerUpEffect.Magnet, worldIndex);
        }

    }
}
