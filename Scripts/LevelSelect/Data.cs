using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.LevelSelect
{
    [Serializable]
    public class Data
    {
        public List<WorldItem> data;

        public Data(List<WorldItem> data)
        {
            this.data = data;
        }
    }
}
