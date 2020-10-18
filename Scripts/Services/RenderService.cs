using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Services
{
    

    class RenderService 
    {
        //--Singleton
        public static RenderService Instance = new RenderService();

        public Texture2D texture2D;

        public static Color[] RenderTextureToPixel(Texture2D texture)
        {
            if( texture == null)
            {
                Debug.Log("Texture cannot be null!");
                return null;
            }

            Color[] color = texture.GetPixels();
            if(color == null)
            {
                Debug.Log("Could not parse texture input");
                return null;
            }
            return color;

        }

        public void Start()
        {
            //--get the pixels
            Color[] color = texture2D.GetPixels();

            //--



            
        }
    }
}
