using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Painters
{
    [SerializeField]
    public abstract class TilePainter : ScriptableObject
    {
        //[SerializeField] protected string mTileName;
        protected Tilemap tilemap;
        //public bool isPrefab { get; }

        public void Init(Tilemap tilemap)
        {
            this.tilemap = tilemap;
        }

        public virtual void Paint(Vector3Int pos)
        {

        }
    }
}
