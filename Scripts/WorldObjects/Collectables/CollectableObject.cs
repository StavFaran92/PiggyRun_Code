using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    abstract class CollectableObject : TriggerObject
    {
        [SerializeField]protected GameObject mCollectedParticleEffect;

        public override void OnPlayerTrigger(Collider2D collision)
        {
            if (mCollectedParticleEffect)
            {
                GameObject pe = ObjectPoolManager.Instance.Spawn(mCollectedParticleEffect.name, transform.position);
                //GameObject pe = Instantiate(mCollectedParticleEffect, transform.position, Quaternion.identity);
                pe.transform.parent = m_Scene.transform;
            }

            CollectableObjectEffect(collision);

            DestroyObject();
        }

        public abstract void CollectableObjectEffect(Collider2D collision);
    }
}
