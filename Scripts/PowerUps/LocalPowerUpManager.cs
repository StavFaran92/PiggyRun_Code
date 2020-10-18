using Assets.Scripts.PowerUps.Effects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.Scripts.PowerUpManager;

namespace Assets.Scripts.PowerUps
{
    /// <summary>
    /// In order to add a new power up you need to:
    /// 1. Create a prefab for the power up and assign it a script and an image
    /// 2. Create a power up object script and attach it to the correct prefab
    /// 2. create the event of start and stop and handle the listeners routine
    /// 4. create an effect script that implements IPowerUpeffect
    /// 5. handle the effect input in the local power up manager
    /// 6. handle the new listeners update
    /// </summary>
    public class LocalPowerUpManager : MonoBehaviour
    {

        PowerUpEffectBase mActivePowerUpEffect;
        float mTime;
        SubScene mSubScene;

        ActionParams mActionParams;

        HUD mHud;

        [SerializeField] private PowerUpMagnet mPowerUpMagnet;
        [SerializeField] private PowerUpFreeze mPowerUpFreeze;
        [SerializeField] private PowerUpShield mPowerUpShield;
        [SerializeField] private PowerUpSpeed mPowerUpSpeed;
        [SerializeField] private PowerUpDoubleCash mPowerUpDoubleCash;
        [SerializeField] private PowerUpHealth mPowerUpHealth;


        public void Start()
        {
            mActivePowerUpEffect = null;
            mTime = 0;
            mActionParams = ActionParams.EmptyParams;

            mSubScene = GetComponentInParent<SubScene>();

            Init();

        }

        void Init()
        {
            mSubScene.GetEventManager().ListenToEvent(ApplicationConstants.EVENT_PLAYER_COLLECTED_MAGNET, ActivateMagnet);
            mSubScene.GetEventManager().ListenToEvent(ApplicationConstants.EVENT_PLAYER_COLLECTED_FREEZE, ActivateFreeze);
            mSubScene.GetEventManager().ListenToEvent(ApplicationConstants.EVENT_PLAYER_COLLECTED_SPEED, ActivateSpeed);
            mSubScene.GetEventManager().ListenToEvent(ApplicationConstants.EVENT_PLAYER_COLLECTED_DOUBLECASH, ActivateDoubleCash);
            mSubScene.GetEventManager().ListenToEvent(ApplicationConstants.EVENT_PLAYER_COLLECTED_SHIELD, ActivateShield);
            mSubScene.GetEventManager().ListenToEvent(ApplicationConstants.EVENT_PLAYER_COLLECTED_HEALTH, ActivateHealth);

        }

        private void ActivateHealth(string arg1, ActionParams actionParams)
        {
            ActivatePowerUpEffect(mPowerUpHealth, actionParams);
        }

        private void ActivateShield(string arg1, ActionParams actionParams)
        {
            ActivatePowerUpEffect(mPowerUpShield, actionParams);
        }

        private void ActivateDoubleCash(string arg1, ActionParams actionParams)
        {
            ActivatePowerUpEffect(mPowerUpShield, actionParams);
        }

        internal void SetHUD(HUD m_Score)
        {
            mHud = m_Score;
        }

        private void ActivateSpeed(string arg1, ActionParams actionParams)
        {
            ActivatePowerUpEffect(mPowerUpSpeed, actionParams);

        }

        private void ActivateFreeze(string arg1, ActionParams actionParams)
        {
            ActivatePowerUpEffect(mPowerUpFreeze, actionParams);
        }

        void ActivateMagnet(string eventName, ActionParams actionParams)
        {
            ActivatePowerUpEffect(mPowerUpMagnet, actionParams);
        }

        public PowerUpEffect GetActivatedPowerUp()
        {
            if (mActivePowerUpEffect == null)
            {
                return PowerUpEffect.None;
            }
            return mActivePowerUpEffect.Effect;
        }

        public ActionParams GetActionParams()
        {
            return mActionParams;
        }

        public void ActivatePowerUpEffect(PowerUpEffectBase powerUpEffect, ActionParams actionParams)
        {
            Debug.Log("Power up activated");

            //--Deactivate previous powerup if nesseccary
            DeactivateEffect();

            //StopCoroutine("Effect");

            mActionParams = actionParams;
            mActivePowerUpEffect = powerUpEffect;
            mActivePowerUpEffect.OnStart();
            mTime = powerUpEffect.Time;


            //mHud.SetPowerUpIcon(mActivePowerUpEffect.IconSprite);

            StartCoroutine("Effect");
            //if (mActivePowerUpEffect == null)
            //{
                
            //}
            //else
            //{
            //    Debug.Log("Power up slot is filled");
            //}
        }

        public void DeactivateEffect()
        {
            if (mActivePowerUpEffect)
            {
                string eventName = mActivePowerUpEffect.GetFinishEvent();
                mSubScene.GetEventManager().CallEvent(eventName, ActionParams.EmptyParams);
            }
            mTime = 0;
            mActivePowerUpEffect = null;
            mActionParams = ActionParams.EmptyParams;

            if (mHud)
            {
                mHud.RemovePowerUpIcon();
                mHud.SetPowerUpSlotBarPercent(0);
            }
        }

        IEnumerator Effect()
        {
            if(mActivePowerUpEffect != null)
            {
                while (mTime > 0)
                {
                    mActivePowerUpEffect.EffectUpdate();
                    mTime -= Time.deltaTime;
                    float startTime = mActivePowerUpEffect.Time;

                    //Update hud timer view
                    if (mHud)
                    {
                        mHud.SetPowerUpSlotBarPercent(mTime / startTime);
                    }
                    
                    yield return new WaitForEndOfFrame();
                }

                DeactivateEffect();
            }
        }

            //internal void UpdateNewListener(WorldObject wo)
            //{
            //    if (mActivePowerUpEffect == null)
            //        return;

            //    PowerUpManager.PowerUpEffect type = mActivePowerUpEffect.GetPowerUpType();

            //    wo.HandlePowerUpEffect(type, true);

            //    //switch (type)
            //    //{
            //    //    case PowerUpManager.PowerUpEffect.Magnet:
            //    //        wo.HandlePowerUpEffect(PowerUpManager.PowerUpEffect.Magnet, true);

            //    //        if (wo is CoinObject)
            //    //        {
            //    //            wo.HandlePowerUpEffect(PowerUpManager.PowerUpEffect.Magnet, true);
            //    //            //((CoinObject)wo).SetPowerUpEffectOnObject(true);
            //    //        }
            //    //        break;
            //    //}
            //}

            //internal void RemoveListener(WorldObject wo)
            //{
            //    if (mActivePowerUpEffect == null)
            //        return;

            //    PowerUpManager.PowerUpEffect type = mActivePowerUpEffect.GetPowerUpType();

            //    //This should be more of a reset
            //    wo.HandlePowerUpEffect(type, false);


            //    //wo.HandlePowerUpEffect(PowerUpManager.PowerUpEffect.Magnet, true);

            //    //if (wo is CoinObject)
            //    //    ((CoinObject)wo).SetPowerUpEffectOnObject(false);
            //}
        }
}
