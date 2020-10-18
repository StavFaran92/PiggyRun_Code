using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Services
{
    class LevelInitializerService : MonoBehaviour
    {

        private void Start()
        {

            int world = PlayerPrefs.GetInt(ApplicationConstants.WORLD_TO_LOAD, 0);
            int level = PlayerPrefs.GetInt(ApplicationConstants.LEVEL_TO_LOAD, 0);

            if(world == -1 || level == -1)
            {
                Debug.Log("Could not get world or level data from perfs: " + world +","+ level);
                return;
            }
            LevelLoaderService.LoadLevel(world, level);

        }
    }
}
