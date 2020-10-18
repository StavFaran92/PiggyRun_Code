using Assets.Scripts;
using Assets.Scripts.SystemAI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPoolFiller
{
    public const int GAME_DIFFICULTY_COUNT = 10;

    public static EventPoolFiller Instance { get; } = new EventPoolFiller();

    public void FillPool(IEventPool<GameEvent> pool, int amountOfEvents, string type)
    {
        //get discrete function for how to pick game events
        double[] eventProbability = Utils.DistributionFunction(GAME_DIFFICULTY_COUNT);

        int[] numberOfEvenetsFromEachLevel = new int[GAME_DIFFICULTY_COUNT];

        for(int i=0; i< GAME_DIFFICULTY_COUNT; i++)
        {
            numberOfEvenetsFromEachLevel[i] = (int)Math.Floor(eventProbability[i] * amountOfEvents);
        }

        for(int i=0; i<eventProbability.Length; i++)
        {
            //factory give me from this pool of options an amount of events according to it's 
            //specified distrub
            int amount = numberOfEvenetsFromEachLevel[i];

            List<GameEvent> generatedEvents =  GameEventFactory.GetIntance().GenerateMany(type, amount, i);

            EventPoolHolder<GameEvent>.AddToPool( pool, generatedEvents);
        }


        //choose from each type x the f(x) amount
            //for every object clone and send to pool

        //call it self in {level} time
        
    }

    internal void ShouldFillPool(string name)
    {
        EventPool<GameEvent> pool = EventPoolHolder<GameEvent>.mInstance.GetPool(name);
        //--Safe while
        int count = 0;
        while (pool.IsBelowThreshold() && ++count < 100)
        {
            if(count == 99)
            {
                Debug.Log("Safe while reached!");
            }
            //--Fill pool with obstacles
            FillPool(pool, 10, "Obstacles");

            //--Fill pool with goods
            FillPool(pool, 10, "Goods");
        }
    }
}



