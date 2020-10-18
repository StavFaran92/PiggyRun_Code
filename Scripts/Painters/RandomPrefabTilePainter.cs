using Assets.Scripts.Painters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "RandomPrefabTilePainter", menuName = "World/RandomPrefabTilePainter")]
    public class RandomPrefabTilePainter : PrefabTilePainter
    {

        [SerializeField] protected List<GameObject> mPrefabList;

        public override void Paint(Vector3Int pos)
        {
            //--get a random number
            int random = UnityEngine.Random.Range(0, 100);

            //--modulo it with the list size
            random = random % mPrefabList.Count;

            //--get it's name
            prefabName = mPrefabList[random].name;

            //--paint a random prefab.
            base.Paint(pos);
        }
    }
}
