using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Services
{
    class UserDataLoader : MonoBehaviour
    {
        string path = Application.dataPath + "/save.binary";
        TextAsset ta;

        private void Start()
        {

            LoadDataFromMemory();

            //TextAsset targetFile = Resources.Load<TextAsset>("playerData");
            ////--In case there is no save file
            //if(targetFile == null)
            //{
            //    File.
            //}
        }

        public void SavePlayerData()
        {
            if (File.Exists(path))
            {
                if(ta == null)
                {
                    LoadDataFromMemory();
                }
                
            }
        }

        private void LoadDataFromMemory()
        {
            if (File.Exists(path))
            {
                ta = Resources.Load(path, typeof(TextAsset)) as TextAsset;
                //byte[] data = File.ReadAllBytes(path);
                //TextAsset targetFile = new TextAsset();
                //targetFile.
            }
        }
    }
}
