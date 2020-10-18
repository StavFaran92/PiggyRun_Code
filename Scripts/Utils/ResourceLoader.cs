using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class ResourceLoader
    {
        public static UnityEngine.Object[] LoadGameObjectFromPath(string path)
        {
            return Resources.LoadAll(path, typeof(GameObject));
        }
    }

    
}
