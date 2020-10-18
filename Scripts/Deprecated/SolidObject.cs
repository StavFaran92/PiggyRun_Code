using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class SolidObject : WorldFiniteObject
    {
        private BoxCollider2D _boxCollider;

        private new void Start()
        {
            base.Start();

            _boxCollider = GetComponent<BoxCollider2D>();
        }




    }
}
