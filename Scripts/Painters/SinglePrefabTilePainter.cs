using Assets.Scripts.Painters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "SinglePrefabTilePainter", menuName = "World/SinglePrefabTilePainter")]
    public class SinglePrefabTilePainter : PrefabTilePainter
    {

        [SerializeField] protected GameObject mPrefab;

        public override void Paint(Vector3Int pos)
        {
            prefabName = mPrefab.name;

            base.Paint(pos);
        }
    }
}
