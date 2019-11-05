using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filling
{
    public class Triangle
    {
        public Point p1 { get; set; }
        public Point p2 { get; set; }
        public Point p3 { get; set; }
        public double ks { get; set; }
        public double kd { get; set; }
        public double m { get; set; }

        public Triangle(Point p1, Point p2, Point p3)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
            ks = 0.5;
            kd = 0.5;
            m = 1;
        }

        public List<Edge> GetEdges()
        {
            List<Edge> result = new List<Edge>();

            result.Add(new Edge(p1, p2));
            result.Add(new Edge(p1, p3));
            result.Add(new Edge(p2, p3));

            return result;
        }

        public List<Point> GetVertex()
        {
            List<Point> result = new List<Point>();

            result.Add(p1);
            result.Add(p2);
            result.Add(p3);

            return result;
        }
    }

    public static class Extension
    {
        public static void WriteAllCoefficienst(this List<Triangle> list, double kd, double ks, double m)
        {
            for(int i=0; i<list.Count; i++)
            {
                list[i].kd = kd;
                list[i].ks = ks;
                list[i].m = m;
            }
        }

        public static void WriteRandomCoefficients(this List<Triangle> list)
        {
            Random rnd = new Random(1234);

            for(int i=0; i<list.Count; i++)
            {
                list[i].kd = rnd.NextDouble();
                list[i].ks = rnd.NextDouble();
                list[i].m = rnd.Next(1, 100);
            }
        }
    }

}
