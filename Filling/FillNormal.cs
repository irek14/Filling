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
        private void FillPolygonNormal(List<Edge> edges, double kd, double ks, double m)
        {
            List<Edge>[] ET = EdgeBucketSort(edges);
            int edgesCounter = edges.Count;
            int y = 0;
            while (ET[y] == null)
                y++;

            List<(double yMax, double xMin, double m)> AET = new List<(double, double, double)>();

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
                        Color objectColor = constColor;
                        if (isColorFromTexture)
                            objectColor = photo[j, y];

                        Vector3D normalVector = new Vector3D(0, 0, 1);
                        if (isNormalVectorFromMap)
                            normalVector = FromNormalMapToVector(normalMap[j, y]);
                        else if(isNormalVectorFromBubble)
                            normalVector = GetNormalVectorFromBubble(j, y);

                        Vector3D newL = CalculateLVector(new Vector3D(j, y, 0));

                        int R = (int)GetLambertColor(((double)lightColor.R / (double)255), objectColor.R, newL, normalVector, ks, kd, m);
                        int G = (int)GetLambertColor(((double)lightColor.G / (double)255), objectColor.G, newL, normalVector, ks, kd, m);
                        int B = (int)GetLambertColor(((double)lightColor.B / (double)255), objectColor.B, newL, normalVector, ks, kd, m);

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

        private Vector3D GetNormalVectorFromBubble(int x, int y)
        {
            double d = Math.Sqrt((x - mousePosition.X) * (x - mousePosition.X) + (y - mousePosition.Y) * (y - mousePosition.Y));
            if(d > bubbleR)
            {
                return new Vector3D(0,0,1);
            }

            double h = Math.Sqrt(bubbleR * bubbleR - d*d);
            Vector3D vector = new Vector3D((x - mousePosition.X), (y - mousePosition.Y), h);
            vector.Normalize();

            return vector;
        }

        private double CalculateTriangleArea(Point p1, Point p2, Point p3)
        {
            return Math.Abs((p1.X * (p2.Y - p3.Y) + p2.X * (p3.Y - p1.Y) + p3.X * (p1.Y - p2.Y)) / 2);
        }

        private List<Edge>[] EdgeBucketSort(List<Edge> edges)
        {
            List<Edge>[] result = new List<Edge>[Photo.Height];

            foreach (Edge edge in edges)
            {
                int index = edge.p1.Y < edge.p2.Y ? edge.p1.Y : edge.p2.Y;

                if (result[index] == null)
                    result[index] = new List<Edge>();

                result[index].Add(edge);
            }

            return result;
        }

        Vector3D DefaultLightColor = new Vector3D(1, 1, 1);

        private double GetLambertColor(double lightColor, double objectColor, Vector3D L, Vector3D N, double ks, double kd, double m)
        {
            Vector3D V = new Vector3D(0, 0, 1);

            Vector3D R = 2 * (N * L) * N - L;

            double firstCosinus = kd * lightColor * objectColor * (N * L);
            if (firstCosinus < 0) firstCosinus = 0;

            double secondCosinus = ks * lightColor * objectColor * Math.Pow(V * R, m);
            if (secondCosinus < 0) secondCosinus = 0;

            double result = firstCosinus + secondCosinus;

            return result;
        }

        private Vector3D CalculateLVector(Vector3D current_point)
        {
            Vector3D newL = new Vector3D(LVersor.x - current_point.x, LVersor.y - current_point.y, LVersor.z);
            newL.Normalize();
            return newL;
        }

        private Vector3D FromNormalMapToVector(Color color)
        {
            var result = new Vector3D(-((double)color.R - 127.0) / 127.0, -((double)color.G - 127.0) / 127.0, ((double)color.B) / 255.0);
            return result;
        }

        private void FixRGB(ref int R, ref int G, ref int B)
        {
            if (R > 255) R = 255;
            else if (R < 0) R = 0;

            if (G > 255) G = 255;
            else if (G < 0) G = 0;

            if (B > 255) B = 255;
            else if (B < 0) B = 0;
        }
    }

}
