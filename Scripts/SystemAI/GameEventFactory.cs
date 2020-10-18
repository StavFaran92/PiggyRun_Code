using Assets.Scripts;
using Assets.Scripts.SystemAI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

internal class GameEventFactory
{
    private static GameEventFactory mInstance = new GameEventFactory();

    public enum PREventType
    {
        Null,
        Obstacles,
        Goods
    }

    private string FromEventTypeToString(PREventType type)
    {
        switch (type)
        {
            case PREventType.Goods:
                return "Goods";
            case PREventType.Obstacles:
                return "Obstacles";
        }
        return null;
    }

    private PREventType FromStringToEventType(string type)
    {
        switch (type)
        {
            case "Goods":
                return PREventType.Goods;
            case "Obstacles":
                return PREventType.Obstacles;
        }
        return PREventType.Null;
    }

    private Dictionary<string, int> m_ObjectToCostDict;

    public static GameEventFactory GetIntance()
    {
        if (mInstance == null)
        {
            mInstance = new GameEventFactory();
        }
        return mInstance;
    }

    private GameEvent[][] m_ObstaclesBluePrints;
    private GameEvent[][] m_GoodsBluePrints;

    public Dictionary<PREventType, GameEvent[][]> dict;

    private GameEventFactory()
    {
        dict = new Dictionary<PREventType, GameEvent[][]>();
        m_GoodsBluePrints = new GameEvent[10][]; //TODO set to difficulty number
        m_ObstaclesBluePrints  = new GameEvent[10][]; //TODO set to difficulty number

        Init();
    }

    private void Init()
    {

        //m_ObjectToCostDict = Utils.ParseJsonData<Dictionary<string, string>>();

        //TODO fix
        m_ObjectToCostDict = new Dictionary<string, int>();



        m_ObjectToCostDict.Add("Goods_1_data", 1);
        m_ObjectToCostDict.Add("Goods_2_data", 1);
        m_ObjectToCostDict.Add("Goods_3_data", 1);
        m_ObjectToCostDict.Add("Goods_4_data", 1);
        m_ObjectToCostDict.Add("Obstacles_1_data", 2);
        m_ObjectToCostDict.Add("Obstacles_2_data", 3);
        m_ObjectToCostDict.Add("Obstacles_3_data", 4);
        m_ObjectToCostDict.Add("Obstacles_4_data", 5);
        m_ObjectToCostDict.Add("Obstacles_5_data", 2);


        dict.Add(PREventType.Obstacles, m_ObstaclesBluePrints);
        dict.Add(PREventType.Goods, m_GoodsBluePrints);

        LoadAllResources();
        
        //LoadResourcesToBlueprints("Obstacles");
        //LoadResourcesToBlueprints("Goods");

        

        
    }

    private void LoadAllResources()
    {
        for (int level = 0; level < 10; level++)
        {
            LoadAllResourcesOfLevel(level);
        }
    }

    private void LoadAllResourcesOfLevel(int level)
    {
        LoadResourcesToBlueprints(PREventType.Obstacles, level);
        LoadResourcesToBlueprints(PREventType.Goods, level);
    }

    private void LoadResourcesToBlueprints(PREventType type, int level)
    {
        //Get 
        dict.TryGetValue(type, out GameEvent[][] bluePrints);
        if (bluePrints == null)
            throw new Exception("Can't find the specified blueprint");
            


        string resourcesPath = ConfigHolder.GetInstance().RESOURCES_PATH + level + "/"+ FromEventTypeToString(type);
        //string resourcesPath = "TestConstructs/Level_" + level + "/"+type;

        Dictionary<string, string> resourcesPrefabs = Utils.LoadResourceTextfilesWithName(resourcesPath);
        

        int length = resourcesPrefabs.Count;

        bluePrints[level] = new GameEvent[length];

        if (length <= 0)
            return;

        Dictionary<string, string>.Enumerator iter = resourcesPrefabs.GetEnumerator();

        int j = 0;
        while (iter.MoveNext()){

            string fname = iter.Current.Key;
            string data = iter.Current.Value;

            m_ObjectToCostDict.TryGetValue(fname, out int cost);

            bluePrints[level][j] = new GameEvent(fname, data, level, cost, FromEventTypeToString(type));

            //bluePrints[level][j].mJsonData = data;

            

            bluePrints[level][j].mCost = cost;
            //search name in map and edit cost

            j++;
        }


        
    }

    internal List<GameEvent> GenerateMany(string type, int amount, int level)
    {
        List<GameEvent> list = new List<GameEvent>();

        PREventType prEventType = FromStringToEventType(type);

        dict.TryGetValue(prEventType, out GameEvent[][] bluePrints);
        if (bluePrints == null)
            throw new Exception("Can't find the specified type");

        if (GetRange(bluePrints, level) > 0)
        {
            for (int i = 0; i < amount; i++)
            {
                list.Add(GenerateOneRandomely(prEventType, level));
            }
        }

        return list;

        //int range = GetRange(level);
        //int r = UnityEngine.Random.Range(0, range); 

        //for(int i=0; i<r; i++)
        //{
        //    list.AddLast(GenerateOne(level));
        //}

        //return list;

    }

    private int GetRange(GameEvent[][] bluePrints, int level)
    {
        return bluePrints[level].Length;
    }

    /// <summary>
    /// This method will get the level and generate one object randomely chosen from 
    /// the bluprint level options.
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    private GameEvent GenerateOneRandomely(PREventType type, int level)
    {
        dict.TryGetValue(type, out GameEvent[][] bluePrints);
        if (bluePrints == null)
            throw new Exception("Can't find the specified type");

        int random = UnityEngine.Random.Range(0, GetRange(bluePrints, level));
        GameEvent gameEvent = bluePrints[level][random];

        return gameEvent;
    }

    /// <summary>
    /// This method will get the level and generate one blueprint.
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public GameEvent GenerateOne(PREventType type, int level, int index)
    {
        dict.TryGetValue(type, out GameEvent[][] bluePrints);
        if (bluePrints == null)
            throw new Exception("Can't find the specified type");


        GameEvent gameEvent = bluePrints[level][index];

        return gameEvent;
    }
}

//internal class GameEventBluePrint
//{
//    public string mJsonData { get; set; }
//    public int mCost{ get; set; }

//}