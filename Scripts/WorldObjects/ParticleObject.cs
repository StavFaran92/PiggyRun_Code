using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.WorldObjects
{
    class ParticleObject : WorldFiniteObject
    {
        private new void Start()
        {
            base.Start();

            ParticleSystem particleSystem = GetComponent<ParticleSystem>();
            particleSystem.Play();
            ObjectPoolManager.Instance.Destroy(name, gameObject, particleSystem.main.duration);
            //Destroy(particleSystem, particleSystem.main.duration);
        }
    }
}
