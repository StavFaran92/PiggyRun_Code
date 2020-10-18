using Assets.Scripts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Communication.BluePrints
{
    public class BluePrintConstructBuilder
    {
        BluePrintConstruct mBluePrint = new BluePrintConstruct();
        string mImagePath;

        /// <summary>
        /// Request Factory
        /// </summary>
        /// <returns></returns>
        public static BluePrintConstructBuilder CreateBluePrint()
        {
            return new BluePrintConstructBuilder();
        }

        public BluePrintConstructBuilder AddConstructData(ConstructData imageMap)
        {
            mBluePrint.AddConstructData(imageMap);
            return this;
        }

        public BluePrintConstruct Build()
        {
            return mBluePrint;
        }
    }
}
