using Assets.Scripts.PowerUps.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using static Assets.Scripts.PowerUpManager;

namespace Assets.Scripts
{
    class DiamondObject : CollectableObject
    {
        public int _value;
        private int origValue;
        //private bool mIsAffectedByMagnet { get; set; } = false;
        private Player mPlayer;

        //I am using this boolean because specifically for magnet power up the power up remains 
        //even after the power is over, it is deactivated when the object is collected
        private bool isMagnetActive;

        
        private PowerUpEffect mActivePowerUp;

        private new void Start()
        {
            base.Start();

            isMagnetActive = false;

            mActivePowerUp = PowerUpEffect.None;

            origValue = _value;
        }
        public override void Init()
        {
            base.Init();

            isMagnetActive = false;

            mActivePowerUp = m_Scene.GetLocalPowerUpManager().GetActivatedPowerUp();

            if (mActivePowerUp.Equals(PowerUpEffect.Magnet))
            {
                OnCollectedMagnet("", ActionParams.EmptyParams);
            }

            mPlayer = m_Scene.GetPlayer();
            m_Scene.GetEventManager().ListenToEvent(ApplicationConstants.EVENT_PLAYER_COLLECTED_MAGNET, OnCollectedMagnet);
        }

        /// <summary>
        /// Event function for collected magnet
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="actionParams"></param>
        private void OnCollectedMagnet(string eventName, ActionParams actionParams)
        {
            m_Scene.GetEventManager().RemoveListenerFromEvent(ApplicationConstants.EVENT_PLAYER_COLLECTED_MAGNET, OnCollectedMagnet);


            mActivePowerUp = PowerUpEffect.Magnet;
            isMagnetActive = true;

        }




        public override void CollectableObjectEffect(Collider2D collision)
        {
            if (m_Scene != null)
            {


                ActionParams actionParams = new ActionParams();
                actionParams.Put("amount", _value);

                
            
                m_Scene.GetEventManager().CallEvent(ApplicationConstants.EVENT_PLAYER_COLLECTED_DIAMOND, actionParams);

                //m_Scene.IncreaseCoinsByAmount(_value);
                
            }
            else
            {
                throw new Exception("Stav please fix me!");
            }
        }

        public void MoveTowardsPlayer(float speed)
        {
            transform.position = Vector3.MoveTowards(transform.position, mPlayer.transform.position, speed);
        }

        public override void DestroyObject()
        {
            m_Scene.GetEventManager().RemoveListenerFromEvent(ApplicationConstants.EVENT_PLAYER_COLLECTED_MAGNET, OnCollectedMagnet);

            base.DestroyObject();
        }

        private new void Update()
        {
            base.Update();

            if (isMagnetActive)
            {
                MoveTowardsPlayer(.3f);
            }

            
        }

        

        //internal void StartPowerUpEffectOnObject(PowerUpManager.PowerUpEffect effect)
        //{
        //    mPowerUpHandler.StartEffect( effect);
        //}
    }
}
