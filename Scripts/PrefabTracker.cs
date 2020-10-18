using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class PrefabTracker : MonoBehaviour
    {
        string _path;

        public PrefabTracker(string path)
        {
            _path = path;
        }

        public string getPath()
        {
            return _path;
        }

        public void SetPrefabName(string name)
        {
            _path = name;
        }
    }
}
