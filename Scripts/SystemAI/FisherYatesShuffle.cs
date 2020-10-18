using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.SystemAI
{
    class FisherYatesShuffle
    {
        private static Random rng = new Random();

        /// <summary>
        /// This is the Fisher Yates shuffle algorithm in it's inside-out version.
        /// </summary>
        /// <param name="list"></param>
        public static void Shuffle<T>(LinkedList<T> list)
        {
            Random rand = new Random();

            for (LinkedListNode<T> n = list.First; n != null; n = n.Next)
            {
                T v = n.Value;
                if (rand.Next(0, 2) == 1)
                {
                    n.Value = list.Last.Value;
                    list.Last.Value = v;
                }
                else
                {
                    n.Value = list.First.Value;
                    list.First.Value = v;
                }
            }
        }

        public static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
