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
    public class LocalLevelBuilder : MonoBehaviour, IBuilder, IService
    {
        //public static LevelBuilder Instance { get; set; }

        private Vector3[] _screensRealtivePosition;
        public GameObject m_GridPrefab;
        public GameObject m_TilemapPrefab_Platform;
        public GameObject m_TilemapPrefab_Solid;
        public GameObject m_TilemapPrefab_Default;
        public GameObject m_CoinPrefab;
        public TileBase TileFloatingGround;
        [HideInInspector]
        public Dictionary<string, UnityEngine.Object> ruleTiles
            = new Dictionary<string, UnityEngine.Object>();



        private Dictionary<string, TilePainter> m_ColorToObjectDict;
        private SubScene mScene;
        private readonly string TAG = "LevelBuilder";

        
        private bool mIsLoaded = false;
        //private DependencyLoader mDependencyLoader;



        //private LevelBuilder()
        //{


        //    CreateConstruct("Assets/star_data.json", new Dictionary<string, string>()
        //    {
        //        { "fff200" , "CoinGold"},
        //    });
        //}

        private void Awake()
        {
            //mDependencyLoader.Awake();


            //Resources.LoadAll<Sprite>("Prefabs");
            //Instance = this;
        }

        private void Start()
        {
            //TODO move this to global

            //-- Load all Rule Tiles into an array, at this point it is only raw data
            UnityEngine.Object[] RuleTiles = Resources.LoadAll("RuleTile/", typeof(RuleTile));

            //-- Insert them into a dictionary 
            foreach (UnityEngine.Object ruleTile in RuleTiles)
            {
                if (!ruleTile || !(ruleTile is RuleTile))
                {
                    throw new System.Exception("Resource loading has failed!");
                }

                ruleTiles.Add(ruleTile.name, (RuleTile)ruleTile);
            }

            mScene = GetComponentInParent<SubScene>();

            if(mScene == null)
            {
                Debug.Log("Scene cannot be null");
                return;
            }
            mScene.SetLocalLevelBuilder(this);
            mScene.GetServiceManager().RegisterService(ApplicationConstants.SERVICE_LOCAL_LEVEL_BUILDER, this);

            //ActionParams actionParams = new ActionParams();
            //actionParams.Put("ref", this);
            mScene.GetEventManager().CallEvent(ApplicationConstants.BUILDER_IS_INITIALIZED, ActionParams.EmptyParams);

            mIsLoaded = true;
            //mDependencyLoader.Run();
        }

        public GameObject CreateElementInFixedPosition(string typeOfElement, Vector3 elementPosition)
        {
            return GetNewInstanceOfType(typeOfElement, elementPosition);
        }

        public GameObject CreateElementInRandomPosition(string typeOfElement, Vector3 relativePosition)
        {
            float cameraHeight = Camera.main.orthographicSize;
            float cameraWidth = cameraHeight * Screen.width / Screen.height;
            Vector3 localPosition = new Vector3(cameraWidth, UnityEngine.Random.Range(-cameraHeight, cameraHeight), 0);

            return GetNewInstanceOfType(typeOfElement, relativePosition + localPosition);
        }

        GameObject GetNewInstanceOfType(string type, Vector3 position)
        {
            GenericFactory factory = new GenericFactory();
            return factory.GetNewInstance(type, position);
        }

        Vector3 GetScreenOffset(int screen)
        {

            return Vector3.one;
        }

        public void SetColorToTileDataDictionaryConfig(Dictionary<string, TilePainter> colorToObjectDict)
        {
            m_ColorToObjectDict = colorToObjectDict;
        }

        /// <summary>
        /// This is the actual level builder API, it is a given the json data holding the construct's information
        /// the scene the object should be deployed to and a holder.
        /// </summary>
        /// <param name="jsonData"></param>
        /// <param name="scene"></param>
        /// <param name="holder"></param>
        /// <returns></returns>
        private GameObject CreateConstruct(GameEvent chunk, CountDownRoot holder)
        {
            //Get Bounds data
            ConstructData constructData = chunk.ConstructData;

            //int h = constructData.h;
            int w = constructData.w;

            int offset = constructData.Offset + (int)mScene.mScreenWidthInWorld;

            //float hRatio = scene.mScreenHeightInWorld / h;


            //float yOffset = scene.mScreenHeightInWorld / 2 + scene.transform.position.y;
            //float xOffset = scene.mScreenWidthInWorld / 2 + scene.transform.position.x;

            //--Instantiate a new grid 
            GameObject grid = GameObject.Instantiate(m_GridPrefab, Vector3.zero, Quaternion.identity);

            //--Set the grid as child of the scene
            grid.transform.parent = mScene.transform;

            //GameObject tilemapGameObject = GameObject.Instantiate(m_TilemapPlatformPrefab, scene.transform.position, Quaternion.identity);
            //Tilemap tilemap = tilemapGameObject.GetComponent<Tilemap>();
            //tilemap.SendMessage("SubscribeToRoot", holder);
            //Tilemap tilemap = grid.GetComponentInChildren<Tilemap>();

            //tilemap.transform.parent = grid.transform;

            //WorldFiniteObject wfo = tilemap.GetComponent<WorldFiniteObject>();

            //--For each of the <color,list> objects in the construct data draw a list of objects 
            //--according to the list <x,y> and color parameter.
            foreach (PixelData pixel in constructData.data)
            {
                if (pixel == null)
                {
                    Debug.Log("pixel is null ");
                }
                if(m_ColorToObjectDict == null)
                {
                    Debug.Log("m_ColorToObjectDict is null ");
                }

                //--Get the tile data from the color to object fictionary
                m_ColorToObjectDict.TryGetValue(pixel.color, out TilePainter tilePainter);

                if (tilePainter == null)
                {
                    Debug.Log("tile data not set in specified map: " + pixel.color);
                }

                GameObject tilemapTemplate = m_TilemapPrefab_Default;

                if (tilePainter.GetType().Equals(typeof(RuleTilePainter)))
                {
                    TilemapType tileMapType = ((RuleTilePainter)tilePainter).GetTilemapType();

                    switch (tileMapType)
                    {
                        case TilemapType.TilemapPlatform:
                            tilemapTemplate = m_TilemapPrefab_Platform;
                            break;
                        case TilemapType.TilemapSolid:
                            tilemapTemplate = m_TilemapPrefab_Solid;
                            break;
                    }

                }
                

                //get pixel collider type and get the corrseponfing tilemap .

                //--Generate a tilemap layer to draw the objects unto.
                GameObject tilemapGameObject = GameObject.Instantiate(tilemapTemplate, Vector3.zero, Quaternion.identity);
                tilemapGameObject.transform.parent = grid.transform;
                tilemapGameObject.SendMessage("SubscribeToRoot", holder);
                tilemapGameObject.SendMessage("Init");

                Tilemap tilemap = tilemapGameObject.GetComponent<Tilemap>();
                

                //--Draw the objects unto the tilemap
                DrawPixelListInTilemap(ref tilemap, pixel.points, tilePainter, holder);

                //Cleam tile data pointer
                tilePainter = null;

                //--This is a hack, if the tilemap is empty (only coins for example which is a prefab)
                //--then I still want to know when the grid is out of bound, hence I draw this extra pixel
                PixelData pd = new PixelData("ff00ff", new List<Point>() { new Point(w, 0) });
                m_ColorToObjectDict.TryGetValue(pd.color, out tilePainter);
                DrawPixelListInTilemap(ref tilemap,
                    pd.points,
                    tilePainter, 
                    holder);

                //--offset the tile map position
                grid.transform.position =
                    mScene.transform.position +
                    new Vector3(
                        offset * grid.GetComponent<Grid>().cellSize.x - 40,
                        mScene.mScreenHeightInWorld / 2 + 1);
                tilemap.transform.position = grid.transform.position;
            }

            ////--This is a hack, if the tilemap is empty (only coins for example which is a prefab)
            ////--then I still want to know when the grid is out of bound, hence I draw this extra pixel
            //PixelData pd = new PixelData("ff00ff", new List<Point>() { new Point(w, 0) });
            //DrawPixelInTilemap(ref tilemap, pd, holder);

            //wfo.SubscribeToRoot(holder);
            //wfo.StartCoroutine("SetAsChildOfGrid", holder);

            ////--offset the tile map position
            //grid.transform.position =
            //    scene.transform.position +
            //    new Vector3(
            //        scene.mScreenWidthInWorld / 2,
            //        scene.mScreenHeightInWorld / 2 + 1);
            //tilemap.transform.position = grid.transform.position;

            //Debug.Log("Created constructat time: {0} at " + Time.time);

            

            Debug.unityLogger.Log(TAG, string.Format("Created chunk {3} in offset {0}, of size {1} at time {2}", offset, w, Time.time, chunk.mName));

            //--Updates the offset
            offset = offset += w;

            //If the tilemap doesnt have any tiles in it we want to desrtroy it
            //TODO optimize
            //if(tilemap.layoutGrid.transform.childCount == 0)
            //{
            //    wfo.DestroyMe();
            //}

            return grid;
        }

        private void DrawPixelListInTilemap(ref Tilemap tilemap, List<Point> points, TilePainter tileData, CountDownRoot root)
        {
            tileData.Init(tilemap);

            foreach (Point point in points)
            {
                //tileData.Paint(new Vector3Int(point.x, -point.y, 0), root);
            }

            ////if (tileData == null)
            ////{
            ////    Debug.Log("tile data not set in specified map");
            ////}
            //else
            //{

            //    //new Vector3Int((int)xOffset + point.x, (int)(yOffset - point.y * hRatio), 0);

            //    //Init the object with the tilemap, if it is a rule tile it uses it to be attached to
            //    //if it is a worlf object it uses it to set it's location.
                



            //}
        }

        public void Build(GameEvent chunk, Vector3 position, CountDownRoot holder, float time)
        {
            StartCoroutine(DelayBuild(chunk, position, holder, time));
        }

        public void Build(List<GameEvent >chunks, Vector3 position, CountDownRoot holder, float time)
        {
            StartCoroutine(DelayBuild(chunks, position, holder, time));
        }

        IEnumerator DelayBuild(GameEvent chunk, Vector3 position, CountDownRoot holder, float time)
        {
            yield return new WaitForSeconds(time);

            Build(chunk, position, holder);
        }

        IEnumerator DelayBuild(List<GameEvent> chunks, Vector3 position, CountDownRoot holder, float time)
        {
            yield return new WaitForSeconds(time);

            Build(chunks, position, holder);
        }

        public void Build(GameEvent chunk, Vector3 position, CountDownRoot holder)
        {
            CreateConstruct(chunk, holder);
        }

        public void Build(List<GameEvent> chunks, Vector3 position, CountDownRoot holder)
        {
            CreateMultipleConstructs(chunks, holder);
        }

        private void CreateMultipleConstructs(List<GameEvent> chunks, CountDownRoot holder)
        {

            foreach(GameEvent chunk in chunks)
            {
                CreateConstruct(chunk, holder);
            }

            
        }

        //public void CallOnLoaded(Action action)
        //{
        //    if (mIsLoaded)
        //    {
        //        action.Invoke();
        //    }
        //    else
        //    {
        //        mDependencyLoader.AddAction(action);
        //    }
        //}
    }

    

    

    

    public interface IBuilder
    {
        //void Build(GameEvent element, Vector3 position, CountDownRoot holder);
        //void Build(GameEvent element, Vector3 position, CountDownRoot holder, float delay);
        void SetColorToTileDataDictionaryConfig(Dictionary<string, TilePainter> colorToObjectDict);
    }

    class Construct
    {
        private List<GameObject> _children = new List<GameObject>();

        public void AppendToChildren(GameObject gameObject)
        {
            _children.Add(gameObject);
        }

        public void DeployConstruct()
        {
            // Activate all children.
        }
    }

    [Serializable]
    public enum TilemapType
    {
        TilemapPlatform,
        TilemapSolid,
        TilemapDefault
    }

}
