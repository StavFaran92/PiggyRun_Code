using Assets.Scripts.Painters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "RuleTilePainter", menuName = "World/RuleTilePainter")]
    public class RuleTilePainter : TilePainter
    {

        [SerializeField] private RuleTile ruleTile;
        [SerializeField] private TilemapType mTilemapType;


        //public RuleTilePainter(string tilemapType)
        //{
        //    //Initalize the rule tile to paint
        //    //LevelBuilder.Instance.ruleTiles.TryGetValue(mTileName, out UnityEngine.Object tile);
        //    //ruleTile = (RuleTile)tile;
        //    mTilemapType = tilemapType;
        //}

        public override void Paint(Vector3Int pos)
        {
            tilemap.SetTile(pos, ruleTile);

        }

        public TilemapType GetTilemapType()
        {
            return mTilemapType;
        }
    }
}
