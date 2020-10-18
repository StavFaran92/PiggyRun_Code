using Assets.Scripts.Communication.BluePrints;
using Assets.Scripts.SceneData;
using Assets.Scripts.SequenceCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SystemAI
{
    /// <summary>
    /// Process:
    /// The EventSelector first collects GameEvents from the pool, 
    /// he stores it in hise GameEventBank, Now He his using the Assembler 
    /// to Assemble the puzzle Pieces (GameEvent implements puzzle)
    /// and when the Assembler is finished He sends it off to the Tube.
    /// </summary>
    class EventSelector : IActor
    {
        //private static LinkedList<GameEvent> mGameEventList = new LinkedList<GameEvent>();
        private static List<GameEvent> mObstacles = new List<GameEvent>();
        private static List<GameEvent> mGoods = new List<GameEvent>();
        private int mMoney;
        private CountDownRoot holder2;

        public static EventSelector Instance { get; } = new EventSelector();

        //private DeploymentTube[] deploymentTubes;

        private EventSelector()
        {
            //Reset money
            mMoney = 0;

            //deploymentTubes = new DeploymentTube[ApplicationConstants.NumberOfScenes];

            //for (int i=0; i<ApplicationConstants.NumberOfScenes; i++)
            //{
            //    deploymentTubes[i] = new DeploymentTube();
            //}
        }

        

        public void Operate()
        {
            ClearList();

            int amountToCollect = UnityEngine.Random.Range(1, 9);


            CollectFromPool(EventPoolHolder<GameEvent>.mInstance.GetPool("eventPool"), amountToCollect);
            //CollectFromPool(EventPoolHolder<GameEvent>.m_GoodsPool, 3);

            Debug.Log(String.Format("EventSelector: collected {0} elements from pool, size of goods: {1}, size of obstacles: {2}"
                , amountToCollect, mGoods.Count, mObstacles.Count));

            //We can specify 0 in the tube since the greedy algo does not use this information.
            List<GameEvent >[] gameEventsToDeployArray = Assembel(EventAssemblerGreedy.Instance, 0);

            Debug.Log(String.Format("EventSelector: asseembled {0} ", gameEventsToDeployArray.ToString()));

            //DeploymentTubeManager.Instance.ApplyRequest(gameEventsToDeploy, SystemAIManager.m_Instance.Activate);

            
            float timeDelay = 0;

            holder2 = new CountDownRoot(WakeUpSystemAI);

            for (int i=0; i< gameEventsToDeployArray.Length; i++)
            {
                //--If there are game events to deploy
                if (gameEventsToDeployArray[i].Count != 0)
                {


                    DeploymentTubeManager.Instance.ApplyRequestToTube(i, (Action callback) =>
                     {
                         Debug.Log("EventSelector: ApplyRequestToTube");

                         CountDownRoot holder = new CountDownRoot(callback);

                         holder2.IncreaseByOne();

                         timeDelay += UnityEngine.Random.Range(0f, 1f); ;

                         //BluePrintConstruct bluePrint = LevelConstructUtil.ConvertFromLevelConstructToBluePrint(gameEventsToDeployArray[i]);

                         SubSceneMultiton.Instance.GetInstance(i).GetLocalLevelBuilder().Build(gameEventsToDeployArray[i], Vector3.zero, holder, timeDelay);
                     }, this);
                }
            }

            //deploymentTubes[0].ApplyRequest(gameEventsToDeploy);

            foreach(List<GameEvent >gel in gameEventsToDeployArray)
            {
                foreach(GameEvent ge in gel)
                {
                    Debug.Log(String.Format("EventSelector: applied request to deploy {0}", ge.mName));
                }
            }

            Debug.Log(String.Format("EventSelector: applied request to tube"));
        }

        public List<GameEvent> GetList(string name)
        {
            if (name.Equals("Obstacles"))
            {
                return mObstacles;
            }
            else if (name.Equals("Goods"))
            {
                return mGoods;
            }
            else
            {
                return null;
            }
        }



        public void CollectFromPool(IEventPool<GameEvent> pool, int amountToCollect)
        {
            int amount = 0;
            int cnt1 = 0;
            int cnt2 = 0;
            while (amount < amountToCollect && ++cnt1 < 100)
            {
                if(cnt1 == 99)
                {
                    Debug.Log("Error");
                }
                List<GameEvent>.Enumerator iter = pool.GetEnumerator();
                int averageCost = EventPoolUtil.GetAverageCost(pool);
                GiveMoney(averageCost);
                

                while (iter.MoveNext() && ++cnt2 < 100)
                {
                    if (cnt2 == 99)
                    {
                        Debug.Log("Error");
                    }
                    GameEvent ge = iter.Current;
                    int cost = ge.GetCost();
                    if (mMoney >= cost)
                    {
                        mMoney -= cost;
                        if (ge.GetEventType().Equals("Obstacles"))
                        {
                            mObstacles.Add(ge);
                        }
                        else if (ge.GetEventType().Equals("Goods"))
                        {
                            mGoods.Add(ge);
                        }
                            //mGameEventList.AddLast(ge);
                        amount++;
                        pool.Remove(iter.Current);
                        break;
                    }
                }
            }


            //I should also consider if to clear the list after every cycle

            
        }

        private void ClearList()
        {
            if (mObstacles != null)
            {
                mObstacles.Clear();
            }

            if (mGoods != null)
            {
                mGoods.Clear();
            }
        }

        public static List<GameEvent >[]Assembel(EventAssemblerGreedy assembler, int tubeCapacity)
        {

            return assembler.Assemble();
        }

        internal int GetMoney()
        {
                return mMoney;
        }

        internal void GiveMoney(int amount)
        {

                mMoney += amount;
            
        }

        internal void ChargeMoney(int cost)
        {
            mMoney = Math.Max(mMoney - cost, 0);
        }

        public void notifyMe()
        {
            Debug.Log("EventSelector: notifyMe");

            holder2.DecreaseByOne();
        }

        private void WakeUpSystemAI()
        {
            Debug.Log("EventSelector: WakeUpSystemAI");

            SystemAIManager.m_Instance.CanOperate();
        }

        //void ChargeMoney(int amount)
        //{
        //    mMoney = M  
        //        amount;
        //}



    }

    static class Extensions
    {
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }

    
}
