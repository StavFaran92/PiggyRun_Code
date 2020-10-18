using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    class GameLoader
    {

        Config mConfig;

        public GameLoader()
        {
            string jsonData = Utils.LoadResourceTextfile("config");
            Config config = Utils.ParseJsonData<Config>(jsonData);
            ConfigHolder.Init(config);
        }
    }
}
