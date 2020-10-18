using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.SystemAI
{
    public interface IEventPool<T>
    {
        T GetEventFromPool();
        List<T> GetManyEventsFromPool(int amount);
        void AddToPool(List<T> toAdd);
        bool IsBelowThreshold();
        string GetName();
        List<T> GetAll();
        int GetSize();
        List<T>.Enumerator GetEnumerator();
        void Remove(T val);
    }
}
