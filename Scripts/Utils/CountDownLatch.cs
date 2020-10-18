using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class CountDownLatch
    {
        CountDownRoot root;

        public CountDownLatch(CountDownRoot root)
        {
            this.root = root;
            root.IncreaseByOne();
        }

        public void DecreaseByOne()
        {
            root.DecreaseByOne();
        }
    }
}
