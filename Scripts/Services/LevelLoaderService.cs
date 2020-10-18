using Assets.Scripts.SceneData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Services
{
    class LevelLoaderService
    {
        public static void LoadLevel(int world, int level)
        {
            for (int i = 0; i < ApplicationConstants.NumberOfScenes; i++)
            {
                //--Load texture file
                Texture2D texture = Utils.LoadTextureFile("World_" + world + "/Level_" + level + "/" + i);

                //--Get bounds
                int width = texture.width;
                int height = texture.height;

                //--convert Texture to pixel data
                Color[] colors = RenderService.RenderTextureToPixel(texture);

                //--The lambda needs to access the correct index
                int j = i;

                //--Get the service manager from scene
                ServiceManager serviceManager = SubSceneMultiton.Instance.
                    GetInstance(i).
                    GetServiceManager();

                //--Call upon creation of service manager to init and create the level
                serviceManager.CallActionWhenServiceIsLive(ApplicationConstants.SERVICE_LOCAL_LEVEL_BUILDER, () => 
                    {
                        LocalLevelBuilder_v2 localLevelBuilder = serviceManager.
                            GetService<LocalLevelBuilder_v2>(ApplicationConstants.SERVICE_LOCAL_LEVEL_BUILDER);
                        localLevelBuilder.Init(world);
                        localLevelBuilder.CreateLevel(colors, width, height);
                    });
            }


        }

    }
}
