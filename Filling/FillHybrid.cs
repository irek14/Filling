using Filling.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Filling
{
    public partial class MainForm
    {
        private void FillPolygonHybrid(Triangle triangle, double kd, double ks, double m)
        {
            List<Edge> edges = triangle.GetEdges();
            List<Edge>[] ET = EdgeBucketSort(edges);
            int edgesCounter = edges.Count;
            int y = 0;
            while (ET[y] == null)
                y++;

            List<(double yMax, double xMin, double m)> AET = new List<(double, double, double)>();

            List<(Point, Color, Vector3D)> triangle_vertex = new List<(Point, Color, Vector3D)>();
            for (int i = 0; i < 3; i++)
            {
                Point p;
                if (i == 0) p = triangle.p1;
                else if (i == 1) p = triangle.p2;
                else p = triangle.p3;

                Color objectColor = constColor;
                if (isColorFromTexture)
                    objectColor = photo[p.X, p.Y];

                Vector3D normalVector = new Vector3D(0, 0, 1);
                if (isNormalVectorFromMap)
                    normalVector = FromNormalMapToVector(normalMap[p.X, p.Y]);

                Vector3D newL = CalculateLVector(new Vector3D(p.X, p.Y, 0));

                int R = (int)GetLambertColor(((double)lightColor.R / (double)255), objectColor.R, newL, normalVector, ks, kd, m);
                int G = (int)GetLambertColor(((double)lightColor.G / (double)255), objectColor.G, newL, normalVector, ks, kd, m);
                int B = (int)GetLambertColor(((double)lightColor.B / (double)255), objectColor.B, newL, normalVector, ks, kd, m);

                FixRGB(ref R, ref G, ref B);

                Color newColor = Color.FromArgb(R, G, B);

                triangle_vertex.Add((p, newColor, normalVector));
            }

            double area = CalculateTriangleArea(triangle.p1, triangle.p2, triangle.p3);

            while (edgesCounter != 0 || AET.Any())
            {
                AET.RemoveAll(x => x.yMax == y);

                if (ET[y] != null)
                {
                    foreach (Edge edge in ET[y])
                    {
                        double mx = ((double)edge.p2.X - (double)edge.p1.X) / ((double)edge.p2.Y - (double)edge.p1.Y);

                        if (edge.p1.Y != edge.p2.Y)
                            AET.Add((edge.p2.Y, edge.p1.X, mx));

                        edgesCounter--;
                    }
                }

                AET.Sort((a, b) => a.xMin.CompareTo(b.xMin));

                for (int i = 0; i < AET.Count; i += 2)
                {
                    for (int j = (int)(AET[i].xMin); j < (int)(AET[i + 1].xMin); j++)
                    {
                        (Color color, Vector3D vector) interpolate_values = CalculateInterpolateColorAndVertex(area, triangle_vertex, j, y);

                        Vector3D newL = CalculateLVector(new Vector3D(j, y, 0));

                        int R = (int)GetLambertColor(((double)lightColor.R / (double)255), interpolate_values.color.R, newL, interpolate_values.vector, ks, kd, m);
                        int G = (int)GetLambertColor(((double)lightColor.G / (double)255), interpolate_values.color.G, newL, interpolate_values.vector, ks, kd, m);
                        int B = (int)GetLambertColor(((double)lightColor.B / (double)255), interpolate_values.color.B, newL, interpolate_values.vector, ks, kd, m);

                        FixRGB(ref R, ref G, ref B);

                        Color newColor = Color.FromArgb(R, G, B);

                        newPhoto[j, y] = newColor;
                    }
                }

                y++;

                for (int i = 0; i < AET.Count; i++)
                {
                    AET[i] = (AET[i].yMax, AET[i].xMin + AET[i].m, AET[i].m);
                }
            }
        }

        private (Color, Vector3D) CalculateInterpolateColorAndVertex(double area, List<(Point p, Color c, Vector3D v)> vertex, int x, int y)
        {
            double alfa = CalculateTriangleArea(new Point(x, y), vertex[1].p, vertex[2].p) / area;
            double beta = CalculateTriangleArea(new Point(x, y), vertex[0].p, vertex[2].p) / area;
            double gamma = CalculateTriangleArea(new Point(x, y), vertex[0].p, vertex[1].p) / area;

            int R = (int)(alfa * vertex[0].c.R + beta * vertex[1].c.R + gamma * vertex[2].c.R);
            int G = (int)(alfa * vertex[0].c.G + beta * vertex[1].c.G + gamma * vertex[2].c.G);
            int B = (int)(alfa * vertex[0].c.B + beta * vertex[1].c.B + gamma * vertex[2].c.B);

            FixRGB(ref R, ref G, ref B);

            Vector3D w = alfa * vertex[0].v + beta * vertex[1].v + gamma * vertex[2].v;
            w.Normalize();

            return (Color.FromArgb(R, G, B), w);
        }
    }

}
