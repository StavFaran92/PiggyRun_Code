using UnityEngine;

namespace Assets.Scripts
{
    public interface IColliderListener
    {
        void OnCollisionEnter2D(Collision2D collision);
        void OnTriggerEnter2D(Collider2D collision);
        void OnCollisionExit2D(Collision2D collision);
    }
}