using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SystemAI
{
    /// <summary>
    /// This is the Event Assembler, it's job is to collect elements from the selector's bank
    /// and assemble them into a larger piece, for now it uses a greedy alggorithm to operate 
    /// but I may change it later into a dynamic programming algorithm, 
    /// 
    /// </summary>
    public class EventAssemblerGreedy : IEventAssembler
    {


        /// <summary>
        /// Explenation:
        /// 
        /// things the system must do:
        /// -buy at least one object
        /// -but no more than 3 objects
        /// -generate numOfScenes in a decreasing probbability
        /// -be randkm
        /// -be dynamic to change in level
        /// -not become overpowered
        /// -have a state meaning if at one point it bought a hard combination the next time it 
        ///     will not be able to do it and vice versa -> save money to buy more expensive stuff.
        /// 
        /// Algorithm:
        /// 
        /// -every time there is a levevl change reset the money to prevent an overpowered AI
        /// 
        /// -Pick random 5 events of different cost, 
        /// -give money the same as max val
        /// -shuffle them
        /// -pick one and add to list
        /// -maxGuess = 2
        /// -guess = random
        /// -repeat(maxGuess > 0 && guess is lower than 0.5f){
        /// -maxGuess--
        /// -pick one
        /// -if can afford it 
        ///     -buy it
        /// - guess = random
        /// 
        ///     
        /// }
        /// </summary>
        //LinkedList<int> scenesLeft = new LinkedList<int>();


        public static EventAssemblerGreedy Instance { get; internal set; } = new EventAssemblerGreedy();

        private static string TAG = "Assembler";

        private static int GetMaxCost(LinkedList<GameEvent> mGameEventList)
        {
            LinkedList<GameEvent>.Enumerator e = mGameEventList.GetEnumerator();
            int maxCost = 0;
            GameEvent ge;
            while (e.MoveNext())
            {
                ge = e.Current;
                maxCost = Math.Max(maxCost, ge.GetCost());
            }
            return maxCost;
        }

        public List<GameEvent>[] Assemble()
        {
            //--Get the selector
            EventSelector selector = EventSelector.Instance;
            if (selector == null)
            {
                Debug.Log("selector list cannot be null!");
            }

            //--Get the obtsacles list
            List<GameEvent> obstacles = selector.GetList("Obstacles");
            if (obstacles == null)
            {
                Debug.Log("obstacles list cannot be null!");
            }
            //--Get the goods list
            List<GameEvent> goods = selector.GetList("Goods");
            if (goods == null)
            {
                Debug.Log("goods list cannot be null!");
            }

            //--Shuffle the lists
            FisherYatesShuffle.Shuffle(obstacles);
            FisherYatesShuffle.Shuffle(goods);

            //--Initaite the game event list array
            List<GameEvent>[] gameEventsListArray = 
                new List<GameEvent>[ApplicationConstants.NumberOfScenes];

            for (int i= 0; i < ApplicationConstants.NumberOfScenes; i++)
            {
                gameEventsListArray[i] = new List<GameEvent>();
            }

            //--this is the list of available constructs the assembler can choose from.
            List<string> availableConstructsTypes = new List<string>();

            int offset = 0;

            //--While the lists are not empty
            while (obstacles.Count > 0 || goods.Count > 0)
            {
                availableConstructsTypes.Clear();

                //--we add the null type because it is always an option
                availableConstructsTypes.Add("Null");

                //--we init the offset of the created structs to 0
                int layerMaxWidth = 0;

                if (goods.Count > 0)
                {
                    availableConstructsTypes.Add("Goods");
                }
                if (obstacles.Count > 0)
                {
                    availableConstructsTypes.Add("Obstacle");
                }

                //--this value is used the offset the chosen scene by a random amount
                int sceneOffset = UnityEngine.Random.Range(0, ApplicationConstants.NumberOfScenes - 1);

                //--For each scene
                for (int i=0; i< ApplicationConstants.NumberOfScenes; i++)
                {
                                 

                    //--generate a random number
                    int rand = UnityEngine.Random.Range(0, availableConstructsTypes.Count);

                    GameEvent gameEventToAdd = null;
                    string constructType = availableConstructsTypes[rand];

                    //--if the construct type chosen is null continue to next scene
                    if (constructType.Equals("Null"))
                    {
                        continue;
                    }
                    else if (constructType.Equals("Obstacle"))
                    {
                        //--We remove the obstacle from the type list in order to prevent 2 obstacles in 1 layer
                        availableConstructsTypes.Remove("Obstacle");

                        //--get the obstacle from the list
                        gameEventToAdd = obstacles[0];
                        obstacles.RemoveAt(0);
                    }
                    else if (constructType.Equals("Goods"))
                    {
                        //--get the obstacle from the list
                        gameEventToAdd = goods[0];
                        goods.RemoveAt(0);
                        if(goods.Count == 0)
                        {
                            availableConstructsTypes.Remove("Goods");
                        }
                    }


                    if(gameEventToAdd == null)
                    {
                        Debug.Log("Game event to add is null, this should not happen");
                    }

                    layerMaxWidth = Math.Max(layerMaxWidth, gameEventToAdd.ConstructData.w);

                    i = (i + sceneOffset) % ApplicationConstants.NumberOfScenes;

                    //ConstructData constructData = Utils.ParseJsonData<ConstructData>(gameEventToAdd.mJsonData);

                    gameEventToAdd.SetOffset(offset);
                    gameEventsListArray[i].Add(gameEventToAdd);


                }

                offset += layerMaxWidth;
            }

            return gameEventsListArray;


        }

        public List<GameEvent>[] Assemble(LinkedList<GameEvent> mGameEventList, int tubeCapacity)
        {
            if (mGameEventList.Count == 0)
            {
                return null;
            }

            EventSelector selector = EventSelector.Instance;

            Debug.unityLogger.Log(TAG, string.Format("Event Assembler now has {0} money", selector.GetMoney()));

            int maxCost = GetMaxCost(mGameEventList);


            //return null;

            selector.GiveMoney(maxCost);

            Debug.unityLogger.Log(TAG,string.Format("Event Assembler now has {0} money", selector.GetMoney()));

            //collect gameEvents from the bank
            FisherYatesShuffle.Shuffle(mGameEventList);

            //Choose one random game event
            LinkedList<GameEvent>.Enumerator enumarator = mGameEventList.GetEnumerator();
            enumarator.MoveNext();

            //Iterate the list until we find an event we can buy
            int cost = enumarator.Current.GetCost();
            string name = enumarator.Current.mName;
            while (selector.GetMoney() < cost)
            {
                //This means we couldn't find any GameEvent  
                if (!enumarator.MoveNext())
                    return null;

                cost = enumarator.Current.GetCost();
            }

            //--Initialize the result array
            List<GameEvent>[] gameEventsListArray = new List<GameEvent>[ApplicationConstants.NumberOfScenes];
            for(int i=0; i< ApplicationConstants.NumberOfScenes; i++)
            {
                gameEventsListArray[i] = new List<GameEvent>();
            }

            //List<GameEvent> gEvents = new List<GameEvent>();

            List<int> scenesLeft = new List<int>() { 1, 2, 3 };

            int selectedScene = UnityEngine.Random.Range(0, ApplicationConstants.NumberOfScenes);

            scenesLeft.Remove(selectedScene);

            GameEvent gEventFirst = enumarator.Current;
            gEventFirst.SetScene(selectedScene);
            gameEventsListArray[selectedScene].Add(gEventFirst);
            selector.ChargeMoney(cost);

            string gameEventType = gEventFirst.GetEventType();

            Debug.unityLogger.Log(TAG, string.Format("Event Assembler bought {0} that cost {1} of type {2}, it was" +
                "deployed to scene {3}", name, cost, gameEventType, selectedScene));

            //enumarator.MoveNext();
            mGameEventList.Remove(gEventFirst);

            //Move current to first node
            //GameEvent currentGEvent = gEventFirst;

            //Populate the remaining scenes list


            

            //scenesLeft.AddLast(0);
            //scenesLeft.AddLast(1);
            //scenesLeft.AddLast(2);

            //Shuffle the last to generate randomness
            //FisherYatesShuffle.Shuffle(scenesLeft);

            //Draw a random scene
            //int currScene = scenesLeft.Last.Value;
            //scenesLeft.RemoveLast();
            //gEventFirst.SetScene(selectedScene);

            //Release variables for an additional use
            //enumarator.Dispose() ;
            //cost = -1;

            //Get list enumarator
            //enumarator = mGameEventList.GetEnumerator();
            //enumarator.MoveNext();

            bool cantFind = false;
            bool isReady = false;
            int guessesLeft = 2;

            int count0 = 0;
            int count1 = 0;
            int count2 = 0;
            
            GameEvent gEventInspected;

            float random = UnityEngine.Random.Range(0f, 1f);
            enumarator = mGameEventList.GetEnumerator();
            enumarator.MoveNext();

            //iterate the list looking for a matching puzzle piece that is also 
            while (guessesLeft > 0 && random < .7f && scenesLeft.Count > 0 && ++count1 != 100)
            {
                if(count1 == 9)
                {
                    Debug.Log("help");
                }
                guessesLeft--;

                gEventInspected = enumarator.Current;
                cost = gEventInspected.GetCost();
                name = gEventInspected.mName;
                gameEventType = gEventInspected.GetEventType();

                Debug.unityLogger.Log(TAG, string.Format("Event Assembler got a chance to buy"));


                //Update current Event
                //currentGEvent = gEventInspected;

                if (selector.GetMoney() >= cost)
                {
                    //Random if should add to scene ot change to a different one
                    random = UnityEngine.Random.Range(0f,1f);
                    if (random > .5f)
                    {
                        selectedScene = mod((selectedScene + 1), ApplicationConstants.NumberOfScenes);
                        scenesLeft.Remove(selectedScene);
                    }
                    gEventInspected.SetScene(selectedScene);

                    selector.ChargeMoney(cost);

                    //Debug.unityLogger.Log(TAG, string.Format("Event Assembler bought {0} that cost {1} of type {2}", name, cost, gameEventType));

                    Debug.unityLogger.Log(TAG, string.Format("Event Assembler bought {0} that cost {1} of type {2}, it was" +
                "deployed to scene {3}", name, cost, gameEventType, selectedScene));

                    gameEventsListArray[selectedScene].Add(gEventInspected);
                    
                }

                enumarator.MoveNext();

                //attach the pieces together
                //currentGEvent.Attach(gEventInspected);







                random = UnityEngine.Random.Range(0f, 1f);

                //Update Current Scene
                



                ////Populate the Remaining moves left
                //LinkedList<AttachmentType> attachmentList 
                //    = PopulateAttachmentsList(currScene, scenesLeft);

                ////Shuffle the moves list to generate randomnes
                //FisherYatesShuffle.Shuffle(attachmentList);

                ////Draw a random move
                //AttachmentType attachment = attachmentList.Last.Value;
                //attachmentList.RemoveLast();



                //    if (GameEventRules.IsValidCombination(currentGEvent, gEventInspected, attachment))
                //    {
                //        //Update Current Scene
                //        currScene = GetSceneAfterAttachment(currScene, attachment);

                //        //attach the pieces together
                //        currentGEvent.Attach(attachment, gEventInspected);
                //        mGameEventList.Remove(gEventInspected);

                //        //Charge for attachment
                //        money -= cost;
                //        tubeCapacity -= cost;

                //        //Update current Event
                //        currentGEvent = gEventInspected;
                //        currentGEvent.SetScene(currScene);

                //        //Restarting the enumarator
                //        enumarator = mGameEventList.GetEnumerator();
                //        enumarator.MoveNext();


                //        break;
                //    }

                //    //Draw a random move
                //    attachment = attachmentList.Last.Value;
                //    attachmentList.RemoveLast();


                //}
                //if (!enumarator.MoveNext())
                //{
                //    cantFind = true;
                //    break;
                //}



            }
            Debug.unityLogger.Log(TAG, string.Format("Event Assembler now has {0} money", selector.GetMoney()));

            //return the combined piece
            return gameEventsListArray;

            

        }

        private LinkedList<AttachmentType> PopulateAttachmentsList(int currScene, LinkedList<int> scenesLeft)
        {
            LinkedList<AttachmentType> attachmentList = new LinkedList<AttachmentType>();
            attachmentList.AddLast(AttachmentType.Forward);

            //Check if Down is possible
            currScene = mod((currScene + 1) , ApplicationConstants.NumberOfScenes);
            if (scenesLeft.Contains(currScene))
            {
                attachmentList.AddLast(AttachmentType.Down);
            }

            //Check if Up is possible
            currScene = mod((currScene + 1) , ApplicationConstants.NumberOfScenes);
            if (scenesLeft.Contains(currScene))
            {
                attachmentList.AddLast(AttachmentType.Up);
            }

            return attachmentList;
            
        }

        private int GetSceneAfterAttachment(int currScene, AttachmentType attachment)
        {
            switch (attachment)
            {
                case AttachmentType.Up:
                    return mod((currScene - 1) , ApplicationConstants.NumberOfScenes);
                case AttachmentType.Down:
                    return mod((currScene + 1) , ApplicationConstants.NumberOfScenes);
                case AttachmentType.Forward:
                    return currScene;
            }
            return -1;
        }

        int mod(int x, int m)
        {
            return (x % m + m) % m;
        }
    }

    public interface IEventAssembler
    {
        List<GameEvent >[]Assemble(LinkedList<GameEvent> mGameEventList, int tubeCapacity);
    }

    public enum AttachmentType
    {
        Up, 
        Down,
        Forward
    }
}