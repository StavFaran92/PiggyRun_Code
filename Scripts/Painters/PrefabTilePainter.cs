using Assets.Scripts.Painters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class PrefabTilePainter : TilePainter
    {
        protected string prefabName;

        public override void Paint( Vector3Int pos)
        {
            if(prefabName == null)
            {
                Debug.Log("prefab name not found!");
                return;
            }
            GameObject instance = ObjectPoolManager.Instance.Spawn(prefabName, Vector3.zero);
            //instance.SendMessage("SetAsChildOfGrid", root);
            //WorldFiniteObject wo = instance.GetComponent<WorldFiniteObject>();

            //Debug.Log("Created instance with name: " + instance.name);

            Grid grid = tilemap.layoutGrid;

            if (instance != null)
            {
                instance.transform.SetParent(tilemap.transform);
                instance.SendMessage("Init");
                instance.transform.position = grid.LocalToWorld(grid.CellToLocalInterpolated(pos)) + new Vector3(1,1);

            }
        }
    }
}
