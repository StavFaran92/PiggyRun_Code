using Assets.Scripts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Communication.BluePrints
{
    public class BluePrintItemBuilder
    {
        public static BluePrintItemBuilder Instance { get; }
            = new BluePrintItemBuilder();

        BluePrintItem mBluePrint = new BluePrintItem();
        ItemType mType;

        public BluePrintItemBuilder SetItemType(ItemType type)
        {
            mBluePrint.mType = type;
            return this;
        }

        public BluePrintItem Build()
        {
            return mBluePrint;
        }
    }
}
