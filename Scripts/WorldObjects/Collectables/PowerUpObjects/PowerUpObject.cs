using Assets.Scripts.Services;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.WorldObjects.Collectables
{
    class PowerUpObject : CollectableObject
    {
        private Sprite mIconSprite;

        private Vector3 mOriginalScale;

        private new void Start()
        {
            base.Start();

            mOriginalScale = transform.localScale;

            mIconSprite = GetComponent<SpriteRenderer>().sprite;
            if (!mIconSprite)
            {
                Debug.Log("Could not find sprite attribute of sprite renderer");
            }
        }

        public override void OnPlayerTrigger(Collider2D collision)
        {


            if (mCollectedParticleEffect)
            {
                GameObject pe = ObjectPoolManager.Instance.Spawn(mCollectedParticleEffect.name, transform.position);
                //GameObject pe = Instantiate(mCollectedParticleEffect, transform.position, Quaternion.identity);
                pe.transform.parent = m_Scene.transform;
            }

            CollectableObjectEffect(collision);
        }

        public override void CollectableObjectEffect(Collider2D collision)
        {
            AudioManager.instance.PlaySoundOverlap("collect_powerup");

            Transform tr = m_Scene.GetScore().GetPowerUpSlotTransform();

            transform.DOMove(tr.position, .5f);
            transform.DOScale(mOriginalScale * .4f, .5f).OnComplete(() =>
            {
                m_Scene.GetScore().SetPowerUpIcon(mIconSprite);

                transform.localScale = mOriginalScale;

                DestroyObject();
            });
        }
    }
}
