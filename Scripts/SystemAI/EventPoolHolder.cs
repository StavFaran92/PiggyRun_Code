using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SystemAI
{
    /// <summary>
    /// This Event pool is a singleton holding of the avaiable event for the selector 
    /// to pick from, it represents the probability space.
    /// </summary>
    class EventPoolHolder<T>
    {
        public static EventPoolHolder<T> mInstance = new EventPoolHolder<T>();


        //public static IEventPool<T> m_ObstaclesPool = new EventPool<T>("Obstacles");
        //public static IEventPool<T> m_GoodsPool = new EventPool<T>("Goods");
        public static IEventPool<T> mPool = new EventPool<T>("eventPool");

        public static void AddToPool(IEventPool<T> pool, List<T> toAdd)
        {
            pool.AddToPool(toAdd);

        }

        public EventPool<T> GetPool(string name )
        {
            EventPool<T>.Pools.TryGetValue(name, out EventPool<T> pool);

            return pool;
        }

        // todo fix
        public static T GetEventFromPool(IEventPool<T> pool)
        {
            return pool.GetEventFromPool();

        } 


        public static List<T> GetManyEventsFromPool(IEventPool<T> pool, int amount)
        {
            return pool.GetManyEventsFromPool(amount);

        }
    }
}
