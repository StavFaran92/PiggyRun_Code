using Assets.Scripts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class HUD : MonoBehaviour, IReceiver
    {
        SubScene m_Scene;
        CoinsCount m_Coins;
        MetersCount m_Meters;
        [SerializeField]private BoosterHandler mBoosterHandler;
        [SerializeField]private Image mPowerUpSlotBar;
        [SerializeField]private Transform mPowerUpSlotTransform;
        [SerializeField]private Image mPowerUpIcon;

        

        internal Transform GetPowerUpSlotTransform()
        {
            return mPowerUpSlotTransform;
        }

        internal void setMetersCount(MetersCount metersCount)
        {
            this.m_Meters = metersCount;
        }

        public void SetPowerUpSlotBarPercent(float val)
        {
            mPowerUpSlotBar.fillAmount = val;
        }

        public void SetPowerUpIcon(Sprite sprite)
        {
            mPowerUpIcon.gameObject.SetActive(true);
            mPowerUpIcon.sprite = sprite;
        }

        public void RemovePowerUpIcon()
        {
            mPowerUpIcon.gameObject.SetActive(false);
        }

        private int _score = 0;

        void Start()
        {
            m_Scene = gameObject.GetComponentInParent<SubScene>();
            m_Scene.SetScore(this);

            mPowerUpIcon.gameObject.SetActive(false);

            m_Scene.GetEventManager().ListenToEvent(ApplicationConstants.EVENT_PLAYER_COLLECTED_BOOSTER, IncreaseBoosterByAmount);
            m_Scene.GetEventManager().ListenToEvent(ApplicationConstants.EVENT_PLAYER_COLLECTED_COIN, IncreaseCoinsByAmount);
            
        }

        public void IncreaseCoinsByAmount(string eventName, ActionParams actionParams)
        {
            int amount = actionParams.Get<int>("amount");

            m_Coins.IncreaseCoinsByAmount(amount);
        }


        public void AddToScore(int amount)
        {
            _score += amount;
        }

        public int GetCoins()
        {
            return m_Coins.GetCoins();
        }

        internal void SetCoins(CoinsCount coinsCount)
        {
            m_Coins = coinsCount;
        }

        internal void IncreaseMetersByAmount(float amount)
        {
            m_Meters.IncreaseMetersByAmount(amount);
        }

        internal void IncreaseBoosterByAmount(string eventName, ActionParams actionParams)
        {
            var obj = actionParams.Get<int>("amount");

            int amount = (int)obj;

            mBoosterHandler.IncreaseBoosterByAmount(amount);
        }

        public void TryPerform(PRActions action)
        {
            throw new NotImplementedException();
        }
    }

    //class ScoreHolder
    //{
    //    private static readonly int numberOfInstances = 4;
    //    private static Score[] scores
    //        = new Score[numberOfInstances];

    //    public static ScoreHolder Instance { get; } = new ScoreHolder();

    //    public Score[] GetAll()
    //    {
    //        return scores;
    //    }

    //    public Score GetInstance(int index)
    //    {
    //        return scores[index];
    //    }
    //    public static void SetInstance(int index, Score score)
    //    {
    //        scores[index] = score;
    //    }
    //}
}
