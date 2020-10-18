using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.WorldObjects.Enemies
{
    class EnemyLand : EnemyBase
    {
        Rigidbody2D m_RigidBody;

        protected new void Start()
        {
            base.Start();

            m_RigidBody = GetComponent<Rigidbody2D>();

            //This is made so that the enemy eill not fall down when instatnaited
            m_RigidBody.gravityScale = 0;
        }

        //public new void Start()
        //{
        //    base.Start();

        //    m_Player = m_Scene.GetPlayer();
        //}

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Background"))
            {
                //When the nemy enters the scene it should be affected by physics
                m_RigidBody.gravityScale = 1;
            }
        }
    }
}
