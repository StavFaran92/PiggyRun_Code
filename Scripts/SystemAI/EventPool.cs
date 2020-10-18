using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.SystemAI
{
    class EventPool<K> : IEventPool<K>
    {
        int threshold = 5;
        string name;

        public static Dictionary<string, EventPool<K>> Pools = new Dictionary<string, EventPool<K>>();

        public EventPool(string name)
        {
            this.name = name;
            Pools.Add(name, this);
        }

        List<K> m_Pool = new List<K>();

        public void AddToPool(List<K> toAdd)
        {
            foreach (K ge in toAdd)
            {
                m_Pool.Add(ge);
            }

            FisherYatesShuffle.Shuffle(m_Pool);
        }

        public K GetEventFromPool()
        {
            K gEvent = default;

            if (m_Pool.Count == 0)
            {
                EventPoolFiller.Instance.ShouldFillPool(name);
            }

            gEvent = m_Pool.First();
            m_Pool.RemoveAt(0);

            

            return gEvent;
        }

        //public LinkedList<K> GetManyEventsFromPool(int amount)
        //{
        //    LinkedList<K> range = new LinkedList<K>();
        //    for (int i = 0; i < amount; i++)
        //    {
        //        K gEvent = GetEventFromPool();
        //        if (gEvent != null)
        //        {
        //            range.AddFirst(gEvent);
        //        }
        //    }
        //    return range;
        //}

        public string GetName()
        {
            return name;
        }

        public bool IsBelowThreshold()
        {
            return m_Pool.Count < threshold;
        }

        public List<K> GetAll()
        {
            return m_Pool;
        }

        public int GetSize()
        {
            return m_Pool.Count;
        }

        public List<K>.Enumerator GetEnumerator()
        {
            if (m_Pool.Count == 0)
            {
                EventPoolFiller.Instance.ShouldFillPool(name);
            }

            return m_Pool.GetEnumerator();
        }

        public void Remove(K val)
        {
            m_Pool.Remove(val);
        }

        public List<K> GetManyEventsFromPool(int amount)
        {
            throw new NotImplementedException();
        }
    }

    public class EventPoolUtil
    {
        public static int GetAverageCost(IEventPool<GameEvent> pool)
        {
            if(pool.GetSize() == 0)
            {
                return 0;
            }
            int sum = 0;
            foreach(GameEvent ge in pool.GetAll())
            {
                sum += ge.GetCost();
            }

            sum = (int)Math.Ceiling((float)sum / pool.GetSize());

            return sum;
        }
    }
}
