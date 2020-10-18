using Assets.Scripts.Painters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "PR Level", menuName = "World/PR Level")]
    class Level : ScriptableObject
    {

        [SerializeField] private GameObject mBackground;
        [SerializeField] private GameObject mForeground;

        [SerializeField] private ColorToTilePainter[] mMapConfig;

        public GameObject GetBackground()
        {
            return mBackground;
        }

        public GameObject GetForeground()
        {
            return mForeground;
        }

        public ColorToTilePainter[] GetColorToTileMap()
        {
            return mMapConfig;
        }


    }

    [Serializable]
    public class ColorToTilePainter
    {
        [SerializeField] private Color mColor;
        [SerializeField] private TilePainter mTileData;

        public Color GetColor()
        {
            return mColor;
        }

        public TilePainter GetTilePainter()
        {
            return mTileData;
        }
    }
}
