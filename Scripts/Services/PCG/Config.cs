using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    class ConfigHolder
    {
        private static Config mConfig;

        public static void Init(Config config)
        {
            mConfig = config;
        }

        public static Config GetInstance()
        {
            if (mConfig == null)
                throw new Exception("Config has not been loaded!");

            return mConfig;
        }
    }

    [Serializable]
    class Config
    {
        public string RESOURCES_PATH { set; get; }
    }
}
