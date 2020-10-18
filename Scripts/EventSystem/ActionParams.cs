using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class ActionParams
    {
        public static readonly ActionParams EmptyParams = new ActionParams(true);

        private Dictionary<string, object> keyValuePairs;

        //This is done for optimiziation
        private ActionParams(bool seal)
        {

        }

        public ActionParams()
        {
            keyValuePairs = new Dictionary<string, object>();
        }

        public void Put<T>(string name, T val)
        {
            keyValuePairs.Add(name, val);
        }


        public static bool IsNull<T>(T obj)
        {
            return EqualityComparer<T>.Default.Equals(obj, default(T));
        }

        public T Get<T>(string name)
        {
            keyValuePairs.TryGetValue(name, out var result);
            return (T)result;
        }

        //public int GetInt(string name)
        //{
        //    if (keyValuePairs.ContainsKey(name))
        //    {
        //        keyValuePairs.TryGetValue(name, out var result);
        //        return (int)result;
        //    }
        //    return -1;
        //}
    }
}
