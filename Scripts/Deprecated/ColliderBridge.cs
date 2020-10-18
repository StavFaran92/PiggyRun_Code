using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class ColliderBridge : MonoBehaviour
    {
        IColliderListener _listener;
        public void Initialize(IColliderListener l)
        {
            _listener = l;
        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            _listener.OnCollisionEnter2D(collision);
        }
        void OnCollisionExit2D(Collision2D collision)
        {
            _listener.OnCollisionExit2D(collision);
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            _listener.OnTriggerEnter2D(other);
        }
    }
}
