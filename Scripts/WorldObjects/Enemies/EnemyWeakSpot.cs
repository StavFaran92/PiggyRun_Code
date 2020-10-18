using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.WorldObjects.Enemies
{
    /// <summary>
    /// This script should be attached to a game object who has a collider2D and is a child of an enemy,
    /// it marks the weak spot of the enemy.
    /// </summary>
    class EnemyWeakSpot : MonoBehaviour
    {
        //BoxCollider2D mBoxCollider2D;
        //Player mPlayer;

        public int force = 2000;
        private Animator mAnimator;
        private EnemyBase mEnemy;

        private void Start()
        {
            //mBoxCollider2D = gameObject.GetComponent<BoxCollider2D>();

            //Get the correct player 
            mEnemy = GetComponentInParent<EnemyBase>();
            mAnimator = mEnemy.GetComponent<Animator>();
            //SubScene subScene = mEnemy.GetScene();
            //mPlayer = subScene.GetPlayer();

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            GameObject other = collision.gameObject;

            

            if (other.CompareTag("player"))
            {
                Rigidbody2D otherRigidBody = other.GetComponent<Rigidbody2D>();

                if (otherRigidBody.velocity.y < 0)
                {
                    Debug.Log("Enemy Hit!");

                    otherRigidBody.velocity = Vector2.zero;
                    otherRigidBody.AddForce(new Vector2(0, 1200), ForceMode2D.Force);
                    mEnemy.EnemyDie();
                }
            }
        }

    }
}
