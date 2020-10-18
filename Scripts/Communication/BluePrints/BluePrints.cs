using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Communication.BluePrints
{
    public interface BluePrint
    {

    }

    public class BluePrintConstruct : BluePrint
    {
        private List<ConstructData >constructDataList = new List<ConstructData>();

        public void AddConstructData(ConstructData data)
        {
            constructDataList.Add(data);
        }

        public List<ConstructData> GetData()
        {
            return constructDataList;
        }


    }

    public class BluePrintItem : BluePrint
    {
        public ItemType mType { get; set; }
    }

    //public class BluePrintHolder<T> where T : BluePrint
    //{
    //    public T data { get; set; }
    //}
}
