using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.WorldObjects.Enemies
{
    class EnemyBee : EnemyAir
    {
        private Player m_Player;

        private float mDirection;
        private float mSpeed = .02f;
    
        private new void Start()
        {
            base.Start();

            mDirection = UnityEngine.Random.Range(0, 359);
            StartCoroutine("ChangeDir");
        }

        private new void Update()
        {
            base.Update();

            transform.position += new Vector3(Mathf.Cos(mDirection), Mathf.Sin(mDirection)) * mSpeed;
        }

        IEnumerator ChangeDir()
        {
            while (true)
            {
                mDirection = UnityEngine.Random.Range(0, 359);
                yield return new WaitForSeconds(.25f);
            }
        }

    }
}
