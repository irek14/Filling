using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filling
{
    public class Edge
    {
        public Point p1;
        public Point p2;

        public Edge(Point p1, Point p2)
        {
            if(p1.Y < p2.Y)
            {
                this.p1 = p1;
                this.p2 = p2;
            }
            else
            {
                this.p2 = p1;
                this.p1 = p2;
            }

        }
    }

    public class EdgeCompare : IComparer<Edge>
    {
        public int Compare(Edge edge1, Edge edge2)
        {
            int y1 = edge1.p1.Y < edge1.p2.Y ? edge1.p1.Y : edge1.p2.Y;
            int y2 = edge2.p1.Y < edge2.p2.Y ? edge2.p1.Y : edge2.p2.Y;

            return y1 - y2;
        }
    }

}
