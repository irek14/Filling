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
    public partial class MainForm : Form
    {
        enum PaintMode {Normal, Interpolate, HybridInterpolate};

        List<Triangle> triangles = null;
        bool isVertexMoving = false;
        Color constColor = Color.White;
        bool isColorFromTexture = true;
        Color lightColor = Color.FromArgb(255, 255, 255);
        bool isNormalVectorFromMap = true;
        Color[,] photo;
        Color[,] normalMap;
        Color[,] newPhoto;
        Bitmap PhotoBitmap = new Bitmap(Resources.Spiderman);
        Bitmap NormalMapBitmap = new Bitmap(Resources.NormalMap);
        Bitmap testBitmap = new Bitmap(Resources.Spiderman);
        Vector3D LVersor = new Vector3D(Resources.Spiderman.Width/2, Resources.Spiderman.Height/2, 100);
        double t = 1;
        PaintMode current_mode = PaintMode.Normal;

        public MainForm()
        {
            InitializeComponent();
            triangles = GenerateTriangles(6, 8);

            InitializeMapAndPhoto();
            Photo.Invalidate();
        }

        private void InitializeMapAndPhoto()
        {
            photo = new Color[PhotoBitmap.Width, PhotoBitmap.Height];
            normalMap = new Color[PhotoBitmap.Width, PhotoBitmap.Height];
            newPhoto = new Color[PhotoBitmap.Width, PhotoBitmap.Height];

            for (int i=0; i<PhotoBitmap.Width; i++)
            {
                for(int j=0; j<PhotoBitmap.Height; j++)
                {
                    photo[i,j] = PhotoBitmap.GetPixel(i,j);
                }
            }

            for (int i = 0; i < PhotoBitmap.Width; i++)
            {
                for (int j = 0; j < PhotoBitmap.Height; j++)
                {
                    normalMap[i,j] = NormalMapBitmap.GetPixel(i%NormalMapBitmap.Height,j%NormalMapBitmap.Width);
                }
            }
        }

        private void Photo_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(PhotoBitmap, new Point(0,0));

            Graphics g = e.Graphics;

            if(current_mode == PaintMode.Normal)
            {
                Parallel.ForEach(triangles, (triangle) =>
                {
                    FillPolygonNormal(triangle.GetEdges(), triangle.kd, triangle.ks, triangle.m);
                });
            }
            if(current_mode == PaintMode.Interpolate)
            {
                Parallel.ForEach(triangles, (triangle) =>
                {
                    FillPolygonInterpolate(triangle, triangle.kd, triangle.ks, triangle.m);
                });
            }



            using (Bitmap processedBitmap = new Bitmap(PhotoBitmap.Width, PhotoBitmap.Height))
            {
                unsafe
                {
                    BitmapData bitmapData = processedBitmap.LockBits(new Rectangle(0, 0, processedBitmap.Width, processedBitmap.Height), ImageLockMode.ReadWrite, processedBitmap.PixelFormat);

                    int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(processedBitmap.PixelFormat) / 8;
                    int heightInPixels = bitmapData.Height;
                    int widthInBytes = bitmapData.Width * bytesPerPixel;
                    byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                    Parallel.For(0, heightInPixels, y =>
                    {
                        byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                        for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                        {
                            currentLine[x] = newPhoto[x / 4, y].B;
                            currentLine[x + 1] = newPhoto[x / 4, y].G;
                            currentLine[x + 2] = newPhoto[x / 4, y].R;
                            currentLine[x + 3] = newPhoto[x / 4, y].A;
                        }
                    });
                    processedBitmap.UnlockBits(bitmapData);
                }

                g.DrawImage(processedBitmap, 0, 0);
            }
            WaitLabel.Visible = false;

            if(triangles.Count < 100)
                DrawTrianglesNest(e.Graphics);
        }

        private List<Triangle> GenerateTriangles(int N, int M)
        {
            List<Triangle> result = new List<Triangle>();

            int width =  (int)(Photo.Width*0.9/N);
            int height = (int)(Photo.Height * 0.9 /M);

            int start_width = (Photo.Width - width*N) / 2;
            int start_height = (Photo.Height - height*M) / 2;

            Point[,] points = new Point[M + 1, N + 1];

            for(int i=0; i<=M; i++)
            {
                for(int j=0; j<=N; j++)
                {
                    points[i, j] = new Point(start_width + width * j,start_height + height*i);
                }
            }

            for(int i=0; i<=M; i++)
            {
                if(i!=0)
                {
                    for (int j = 0; j < N; j++)
                    {
                        result.Add(new Triangle(points[i, j], points[i, j + 1], points[i - 1, j]));
                    }
                }

                if(i!=M)
                {
                    for(int j=0; j<N; j++)
                    {
                        result.Add(new Triangle(points[i, j], points[i, j + 1], points[i + 1, j+1]));
                    }
                }
            }

            return result;
        }

        private void DrawTrianglesNest(Graphics graph)
        {
            if (triangles == null)
                return;

            foreach(var triangle in triangles)
            {
                graph.DrawLine(new Pen(Color.Black, 2), triangle.p1, triangle.p2);
                graph.DrawLine(new Pen(Color.Black, 2), triangle.p1, triangle.p3);
                graph.DrawLine(new Pen(Color.Black, 2), triangle.p2, triangle.p3);
            }
        }

        private void Photo_MouseMove(object sender, MouseEventArgs e)
        {
            if (isVertexMoving)
            {
                MoveVertex(new Point(e.Location.X, e.Location.Y));
                Photo.Invalidate();
            }
        }

        private void Photo_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.Location.X, e.Location.Y);
            Point? vertex = GetVertexFromTriangle(p);
            if (vertex != null)
            {
                isVertexMoving = true;
                vertex_to_move = (Point)vertex;
                Cursor.Current = Cursors.NoMove2D;
            }
        }

        private void Photo_MouseUp(object sender, MouseEventArgs e)
        {
            isVertexMoving = false;
        }

        private void FillPolygonInterpolate(Triangle triangle, double kd, double ks, double m)
        {
            List<Edge> edges = triangle.GetEdges();
            List<Edge>[] ET = EdgeBucketSort(edges);
            int edgesCounter = edges.Count;
            int y = 0;
            while (ET[y] == null)
                y++;

            List<(double yMax, double xMin, double m)> AET = new List<(double, double, double)>();

            List<(Point, Color)> triangle_vertex = new List<(Point, Color)>();
            for(int i=0; i<3; i++)
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

                triangle_vertex.Add((p, newColor));
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
                        Color newColor = CalculateInterpolateColor(area, triangle_vertex, j, y);

                        //graph.FillRectangle(new SolidBrush(newColor), j, y, 1, 1);
                        //testBitmap.SetPixel(j, y, newColor);
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

        private Color CalculateInterpolateColor(double area, List<(Point p, Color c)> vertex, int x, int y)
        {
            double alfa = CalculateTriangleArea(new Point(x, y), vertex[1].p, vertex[2].p)/area;
            double beta = CalculateTriangleArea(new Point(x, y), vertex[0].p, vertex[2].p) / area;
            double gamma = CalculateTriangleArea(new Point(x, y), vertex[0].p, vertex[1].p) / area;

            int R = (int)(alfa * vertex[0].c.R + beta * vertex[1].c.R + gamma * vertex[2].c.R);
            int G = (int)(alfa * vertex[0].c.G + beta * vertex[1].c.G + gamma * vertex[2].c.G);
            int B = (int)(alfa * vertex[0].c.B + beta * vertex[1].c.B + gamma * vertex[2].c.B);

            FixRGB(ref R, ref G, ref B);

            return Color.FromArgb(R, G, B);
        }

        private double CalculateTriangleArea(Point p1, Point p2, Point p3)
        {
            return Math.Abs((p1.X * (p2.Y - p3.Y) + p2.X * (p3.Y - p1.Y) + p3.X * (p1.Y - p2.Y)) / 2);
        }

        private void FillPolygonNormal(List<Edge> edges,double kd, double ks, double m)
        {
            List<Edge>[] ET = EdgeBucketSort(edges);
            int edgesCounter = edges.Count;
            int y = 0;
            while (ET[y] == null)
                y++;

            List<(double yMax, double xMin, double m)> AET = new List<(double, double, double)>();

            while(edgesCounter != 0 || AET.Any())
            {
                AET.RemoveAll(x => x.yMax == y);

                if (ET[y] != null)
                {
                    foreach(Edge edge in ET[y])
                    {
                        double mx = ((double)edge.p2.X - (double)edge.p1.X) / ((double)edge.p2.Y - (double)edge.p1.Y);

                        if (edge.p1.Y != edge.p2.Y)
                            AET.Add((edge.p2.Y, edge.p1.X, mx));

                        edgesCounter--;
                    }
                }
                
                AET.Sort((a,b)=>a.xMin.CompareTo(b.xMin));

                for(int i=0; i<AET.Count; i+=2)
                {
                    for(int j=(int)(AET[i].xMin); j<(int)(AET[i+1].xMin); j++)
                    {
                        Color objectColor = constColor;
                        if (isColorFromTexture)
                            objectColor = photo[j, y];

                        Vector3D normalVector = new Vector3D(0, 0, 1);
                        if(isNormalVectorFromMap)
                            normalVector = FromNormalMapToVector(normalMap[j,y]);

                        Vector3D newL = CalculateLVector(new Vector3D(j, y, 0));

                        int R = (int)GetLambertColor(((double)lightColor.R/(double)255), objectColor.R, newL, normalVector,ks,kd,m);
                        int G = (int)GetLambertColor(((double)lightColor.G / (double)255), objectColor.G, newL, normalVector,ks,kd,m);
                        int B = (int)GetLambertColor(((double)lightColor.B / (double)255), objectColor.B, newL, normalVector,ks,kd,m);

                        FixRGB(ref R, ref G, ref B);

                        Color newColor = Color.FromArgb(R, G, B);

                        //graph.FillRectangle(new SolidBrush(newColor), j, y, 1, 1);
                        //testBitmap.SetPixel(j, y, newColor);
                        newPhoto[j,y] = newColor;
                    }
                }

                y++;

                for(int i=0; i<AET.Count; i++)
                {
                    AET[i] = (AET[i].yMax, AET[i].xMin + AET[i].m, AET[i].m);
                }
            }


        }

        private List<Edge>[] EdgeBucketSort(List<Edge> edges)
        {
            List<Edge>[] result = new List<Edge>[Photo.Height];

            foreach(Edge edge in edges)
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

            Vector3D R = 2*(N*L)*N - L;

            double result = kd * lightColor * objectColor * (N * L) + ks * lightColor * objectColor * Math.Pow(V * R, m);

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
            var result = new Vector3D(-((double)color.R-127.0)/127.0, -((double)color.G - 127.0)/127.0,((double)color.B)/255.0);
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

        private void ColorLabel_Click(object sender, EventArgs e)
        {
            ColorDialog.ShowDialog();
            constColor = ColorDialog.Color;
            ColorLabel.BackColor = constColor;
        }

        private void NVectorLabel_Click(object sender, EventArgs e)
        {

        }

        private void CoefficientsLabel_Click(object sender, EventArgs e)
        {

        }

        private void LightColor_Click(object sender, EventArgs e)
        {
            ColorDialog.ShowDialog();
            LightColor.BackColor = ColorDialog.Color;
        }

        private void ChangeCoefficients()
        {
            int mNest = int.Parse(MText.Text);
            int nNest = int.Parse(NText.Text);

            triangles = GenerateTriangles(nNest, mNest);

            if(CoefficientSameValueRadioButton.Checked)
            {
                double kd = kTrackBar.Value * 0.01;
                double ks = 1 - kd;
                double m = mTrackBar.Value;

                triangles.WriteAllCoefficienst(kd, ks, m);
            }
            else
            {
                triangles.WriteRandomCoefficients();
            }

            if(LConstRadioButton.Checked)
            {
                LightTimer.Stop();
            }
            else
            {
                LightTimer.Start();
            }

            if(PreciselyFillRadioButton.Checked)
            {
                current_mode = PaintMode.Normal;
            }
            else if(InterpolationFillRadioButton.Checked)
            {
                current_mode = PaintMode.Interpolate;
            }
            else if(HybridFillRadioButton.Checked)
            {
                current_mode = PaintMode.HybridInterpolate;
            }

            isColorFromTexture = TextureColorRadioButton.Checked;
            isNormalVectorFromMap = NFromTextureRadioButton.Checked;
            lightColor = LightColor.BackColor;
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            WaitLabel.Visible = true;
            ChangeCoefficients();
            Photo.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = true;
            string sSelectedFile;

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                PhotoBitmap = new Bitmap(choofdlog.FileName);
                InitializeMapAndPhoto();
            }
        }

        private void LightTimer_Tick(object sender, EventArgs e)
        {
            double newX = PhotoBitmap.Width/2 * Math.Sin(t + 5 * Math.PI / 2) + PhotoBitmap.Width/2;
            double newY = PhotoBitmap.Height/2 * Math.Sin(4 * t) + PhotoBitmap.Height/2;
            t += 0.03;
            LVersor = new Vector3D(newX, newY, 100);
            Photo.Invalidate();
        }
    }
}
