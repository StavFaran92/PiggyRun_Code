using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    [Serializable]
    public class ConstructData
    {
        public ConstructData()
        {

        }

        public string name;
        public List<PixelData> data;
        public int w, h;
        public int cost;
        public int Offset;
    }

    [Serializable]
    public class PixelData
    {
        public PixelData(string color, List<Point> points)
        {
            this.color = color;
            this.points = points;
        }
        public string color;
        public List<Point> points;
    }

    [Serializable]
    public class Point
    {
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int x, y;
    }

}
