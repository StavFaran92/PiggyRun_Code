using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class Utils 
    {
        public static Texture2D LoadTextureFile(string filepath)
        {
            Texture2D tex = Resources.Load(filepath) as Texture2D;

            if(tex == null)
            {

                Debug.Log("Could not load texture file, check your path");
                return null;
            }

            return tex;
        }

        public static string LoadFileData(string fileName)
        {
            using (StreamReader r = new StreamReader(fileName))
            {
                return r.ReadToEnd();
            }
        }

        public static string LoadResourceTextfile(string path)
        {

            string filePath = path.Replace(".json", "");

            TextAsset targetFile = Resources.Load<TextAsset>(filePath);

            return targetFile.text;
        }

        public static Dictionary<string, string> LoadResourceTextfilesWithName(string path)
        {

            string filePath = path.Replace(".json", "");

            TextAsset[] targetFile = Resources.LoadAll<TextAsset>(filePath);
           

            Dictionary<string, string> result = new Dictionary<string, string>();
            for (int i = 0; i < targetFile.Length; i++)
            {
                result.Add(targetFile[i].name, targetFile[i].text);
            }
            return result;
        }

        public static string[] LoadResourceTextfiles(string path)
        {

            string filePath = path.Replace(".json", "");

            TextAsset[] targetFile = Resources.LoadAll<TextAsset>(filePath);

            string[] result = new string[targetFile.Length];
            for(int i=0; i<targetFile.Length; i++)
            {
                result[i] = targetFile[i].text;
            }
            return result;
        }

        //TODO generelyze
        internal static double[] DistributionFunction(int difficultyCount)
        {
            double Probability = .5f;

            double[] x = new double[difficultyCount];

            for(int i=0; i< difficultyCount; i ++)
            {
                x[i] = Math.Pow((1 - Probability), i) * Probability;
            }

            return x;
        }

        public static T ParseJsonData<T>(string jsonData)
        {
            return JsonConvert.DeserializeObject<T>(jsonData);
        }

        public static bool IsObjectLeftOfRightBound(Transform transform, float screenWidth)
        {
            return transform.position.x * 2 + transform.localScale.x < screenWidth + 50;
        }

        public static bool IsObjectLeftOfLeftBound(Transform transform, float screenWidth)
        {
            return transform.position.x * 2 + transform.localScale.x < -screenWidth - 1;
        }
    }

    public static class LinkedListExtensions
    {
        public static void AppendRange<T>(this LinkedList<T> source,
                                          IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                source.AddLast(item);
            }
        }

        public static void PrependRange<T>(this LinkedList<T> source,
                                           IEnumerable<T> items)
        {
            LinkedListNode<T> first = source.First;
            foreach (T item in items)
            {
                source.AddBefore(first, item);
            }
        }
    }
}
