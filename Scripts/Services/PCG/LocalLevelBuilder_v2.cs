using Assets.Scripts.Communication.BluePrints;
using Assets.Scripts.Painters;
using Assets.Scripts.Services;
using Assets.Scripts.SystemAI;
using Assets.Scripts.WorldObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts
{
    public class LocalLevelBuilder_v2 : MonoBehaviour, IBuilder, IService
    {
        private readonly string TAG = "LevelBuilder";

        #region FIELDS
        [SerializeField] private Level[] mLevels;
        private Vector3[] _screensRealtivePosition;
        [SerializeField] private GameObject m_GridPrefab;
        [SerializeField] private GameObject m_TilemapPrefab_Platform;
        [SerializeField] private GameObject m_TilemapPrefab_Solid;
        [SerializeField] private GameObject m_TilemapPrefab_Default;

        private Tilemap mTilemapDefault;
        private Tilemap mTilemapPlatform;
        private Tilemap mTilemapSolid;

        private Dictionary<string, TilePainter> m_ColorToObjectDict;
        private SubScene mScene;
        
        private Dictionary<string, PixelData>[] mCachedLevel;
        private const int mBufferSize = 15;
        private int mChunkIndex;
        private int mNumOfChunks;
        private Player mPlayer;
        #endregion

        #region METHODS

        private void Start()
        {
            Debug.unityLogger.Log(TAG, "Start()");

            mScene = GetComponentInParent<SubScene>();

            if (mScene == null)
            {
                Debug.Log("Scene cannot be null");
                return;
            }

            

            //mScene.SetLocalLevelBuilder(this);


            //ActionParams actionParams = new ActionParams();
            //actionParams.Put("ref", this);
            mScene.GetEventManager().CallEvent(ApplicationConstants.BUILDER_IS_INITIALIZED, ActionParams.EmptyParams);

            mPlayer = mScene.GetPlayer();

            mScene.GetServiceManager().RegisterService(ApplicationConstants.SERVICE_LOCAL_LEVEL_BUILDER, this);



        }

        /// <summary>
        /// This level initializes the local level builder, it gets the world index as parameter
        /// and configures the correct background and tileset to the system
        /// </summary>
        /// <param name="worldIndex"></param>
        public void Init(int worldIndex)
        {
            Debug.unityLogger.Log(TAG, "Init()");

            //get current level from data
            Level currentLevel = mLevels[worldIndex];

            //spawn a background for the game
            GameObject background = Instantiate(currentLevel.GetBackground());
            background.transform.position = mScene.transform.position + new Vector3(0, 6.29f) ;
            background.transform.parent = mScene.transform;


            Dictionary<string, TilePainter> configMap = new Dictionary<string, TilePainter>();
            foreach (ColorToTilePainter ctt in currentLevel.GetColorToTileMap())
            {
                configMap.Add(ColorUtility.ToHtmlStringRGB(ctt.GetColor()).ToLower(), ctt.GetTilePainter());
            }

            //set builder new configuration
            SetColorToTileDataDictionaryConfig(configMap);

            mChunkIndex = 0;

            //--Instantiate a new grid 
            GameObject grid = Instantiate(m_GridPrefab, mScene.transform.position + 
                new Vector3(-13.51f, 17.9f), Quaternion.identity, mScene.transform);

            //--Instantiate the different types of tilemaps the builder can draw into
            mTilemapDefault = Instantiate(m_TilemapPrefab_Default, grid.transform.position, Quaternion.identity, grid.transform)
                .GetComponent<Tilemap>();
            mTilemapSolid = Instantiate(m_TilemapPrefab_Solid, grid.transform.position, Quaternion.identity, grid.transform)
                .GetComponent<Tilemap>();
            mTilemapPlatform = Instantiate(m_TilemapPrefab_Platform, grid.transform.position, Quaternion.identity, grid.transform)
                .GetComponent<Tilemap>();
        }

        /// <summary>
        /// This method takes the pixel data as a color array and creates the level DB using this information
        /// it divides it into chunks in order to spawn them when needed and not upon start for optimization.
        /// </summary>
        /// <param name="pixels"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void CreateLevel(Color[] pixels, int width, int height)
        {
            Debug.unityLogger.Log(TAG, "CreateLevel()");

            mNumOfChunks = (int)Mathf.Ceil(width / mBufferSize);

            mCachedLevel = new Dictionary<string, PixelData>[mNumOfChunks + 1];

            for(int i =0; i< mNumOfChunks + 1; i++)
            {
                mCachedLevel[i] = new Dictionary<string, PixelData>();
            }

            //--Populate the temp cache dict with the new data given
            for (int  y= 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int chunkIndex = (int)Mathf.Floor(x / mBufferSize);

                    //--get pixel color
                    Color color = pixels[y * width + x];

                    //--Get color as hexadecimal
                    string hexColor = ColorUtility.ToHtmlStringRGB(color);

                    //--We want to ignore white since it indicates open space
                    if(hexColor == "FFFFFF")
                    {
                        continue;
                    }
                    AddPointToCacheMap( hexColor.ToLower(), x, y - height + 1, chunkIndex);
                }
            }
            CreateLevelChunk();
            CreateLevelChunk();

            StartCoroutine(ShouldGenerate());
        }

        private void AddPointToCacheMap(string hexColor, int x, int y, int chunkIndex)
        {
            //Debug.unityLogger.Log(TAG, "AddPointToCacheMap()");

            //--get the list of pixels as pixelData under the given color
            mCachedLevel[chunkIndex].TryGetValue(hexColor, out PixelData value);

            //--if the list does not exsits create onr
            if (value == null)
            {
                value = new PixelData(hexColor, new List<Point>());
                mCachedLevel[chunkIndex].Add(hexColor, value);
            }

            //--add the pixel to the list
            value.points.Add(new Point(x, y));
        }

        /// <summary>
        /// Returns the buffer size times chunk index, this indicates the right most element generated by the builder
        /// </summary>
        /// <returns></returns>
        public int GetEndOfLastChunkOffsetInWorld()
        {
            //Debug.unityLogger.Log(TAG, "GetEndOfLastChunkOffsetInWorld()");

            return mBufferSize * mChunkIndex;
        }

        /// <summary>
        /// This method is used to configure the builder with the correct world map objects
        /// </summary>
        /// <param name="colorToObjectDict"></param>
        public void SetColorToTileDataDictionaryConfig(Dictionary<string, TilePainter> colorToObjectDict)
        {
            Debug.unityLogger.Log(TAG, "SetColorToTileDataDictionaryConfig()");

            m_ColorToObjectDict = colorToObjectDict;
        }

        /// <summary>
        /// This method is responsible of generating a new section of the level at runtime
        /// </summary>
        /// <returns></returns>
        IEnumerator ShouldGenerate()
        {
            Debug.unityLogger.Log(TAG, "ShouldGenerate");
            while (true)
            {
                float lastChunkOffset = GetEndOfLastChunkOffsetInWorld();

                //--If we reached the end of the level -> stop generating
                if (mChunkIndex == mNumOfChunks)
                {
                    break;
                }

                //--Check to see if the player POV requires more level to generate
                if (mPlayer.transform.position.x + 60 > lastChunkOffset * 2)
                {
                    //Debug.Log("player is at: " + transform.position.x + ", last chunk is at: " + lastChunkOffset);

                    CreateLevelChunk();
                }

                //We want to check every second to optimize the system
                yield return new WaitForSeconds(1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateLevelChunk()
        {
            Debug.unityLogger.Log(TAG, "CreateLevelChunk()" );

            

            //--For each of the <color,list> objects in the construct data draw a list of objects 
            //--according to the list <x,y> and color parameter.
            foreach (PixelData pixel in mCachedLevel[mChunkIndex].Values)
            {
                if (pixel == null)
                {
                    Debug.unityLogger.Log(TAG, "pixel is null");
                }
                if (m_ColorToObjectDict == null)
                {
                    Debug.unityLogger.Log(TAG, "m_ColorToObjectDict is null");
                }

                //--Get the tile data from the color to object fictionary
                m_ColorToObjectDict.TryGetValue(pixel.color, out TilePainter tilePainter);

                if (tilePainter == null)
                {
                    Debug.unityLogger.Log(TAG, "tile data not set in specified map: " + pixel.color);
                }

                //--Get the tilemap type from the painter
                Tilemap tilemapToDrawUnto = mTilemapDefault;

                if (tilePainter.GetType().Equals(typeof(RuleTilePainter)))
                {
                    TilemapType tileMapType = ((RuleTilePainter)tilePainter).GetTilemapType();

                    switch (tileMapType)
                    {
                        case TilemapType.TilemapPlatform:
                            tilemapToDrawUnto = mTilemapPlatform;
                            break;
                        case TilemapType.TilemapSolid:
                            tilemapToDrawUnto = mTilemapSolid;
                            break;
                    }

                }


                //--Draw the objects unto the tilemap
                DrawPixelListInTilemap(ref tilemapToDrawUnto, pixel.points, tilePainter);

                //Cleam tile data pointer
                tilePainter = null;
            }

            //--After we are done painting the batch we increment the chunk index
            mChunkIndex++;
        }

        private void DrawPixelListInTilemap(ref Tilemap tilemap, List<Point> points, TilePainter tileData)
        {
            //Debug.unityLogger.Log(TAG, "DrawPixelListInTilemap()");

            tileData.Init(tilemap);

            foreach (Point point in points)
            {
                tileData.Paint(new Vector3Int(point.x, point.y, 0));
            }
        }

        private void OnDestroy()
        {
            mScene.GetServiceManager().UnRegisterService(ApplicationConstants.SERVICE_LOCAL_LEVEL_BUILDER, this);
        }
    }

    

    
    #endregion
}
