using DG.Tweening;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    internal class BoosterHandler : MonoBehaviour
    {

        [SerializeField] private Transform mTransform;
        private float mBoosterAmount;
        private const float mMaxBoosterAmount = 15;


        private void Start()
        {
            ResetBooster();
        }

        private void ResetBooster()
        {
            mBoosterAmount = 0;
            Draw(0);
        }

        internal void IncreaseBoosterByAmount(float amount)
        {
            mBoosterAmount = Math.Min(amount, mMaxBoosterAmount);
            Draw(mBoosterAmount / mMaxBoosterAmount);
        }

        private void Draw(float percent)
        {
            mTransform.DOScaleY(percent, 1);
            
        }
    }
}