using Assets.Scripts.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.WorldObjects.Enemies
{
    class EnemyPlayerCollider : MonoBehaviour
    {

        EnemyBase mEnemy;

        private void Start()
        {
            mEnemy = GetComponentInParent<EnemyBase>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {


            if (collision.gameObject.CompareTag("player"))
            {
                Player player = collision.gameObject.GetComponentInParent<Player>();

                player.OnCollideWithEnemy();

                if (player.GetState() is UltraJumpState || player.GetState() is SlideState)
                {

                    Rigidbody2D otherRigidBody = player.GetComponent<Rigidbody2D>();

                    Debug.Log("Enemy Hit!");

                    otherRigidBody.velocity = Vector2.zero;
                    otherRigidBody.AddForce(new Vector2(0, 600), ForceMode2D.Force);
                    mEnemy.EnemyDie();

                }
                
            }
        }



    }
}
