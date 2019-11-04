using Filling.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Filling
{
    public partial class MainForm : Form
    {
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
        Vector3D LVersor = new Vector3D(Resources.Spiderman.Width/2, Resources.Spiderman.Height/2, 100);
        double t = 1;

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

            for (int i=1; i<PhotoBitmap.Width; i++)
            {
                for(int j=1; j<PhotoBitmap.Height; j++)
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
            e.Graphics.DrawImage(Resources.Spiderman, new Point(0,0));

            Parallel.ForEach(triangles, (triangle) =>
           {
               FillPolygon(triangle.GetEdges(), triangle.kd, triangle.ks, triangle.m, e.Graphics);
           });

            //Parallel.ForEach(triangles, (triangle) =>
            //   {
            //       FillPolygon(triangle.GetEdges(), triangle.kd, triangle.ks, triangle.m, e.Graphics, Resources.Spiderman, Resources.NormalMap);
            //   });

            DrawPhoto(newPhoto, e.Graphics);
            DrawTrianglesNest(e.Graphics);
            WaitLabel.Visible = false;
        }

        private void DrawPhoto(Color[,] colours, Graphics g)
        {
            photo = new Color[PhotoBitmap.Width, PhotoBitmap.Height];
            for (int i=0; i< PhotoBitmap.Width; i++)
            {
                for(int j=0; j< PhotoBitmap.Height; j++)
                {
                    g.FillRectangle(new SolidBrush(colours[i, j]), i, j, 1, 1);
                }
            }
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

        private void FillPolygon(List<Edge> edges,double kd, double ks, double m, Graphics graph)
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
            var result = new Vector3D(((double)color.R-127.0)/127.0, ((double)color.G - 127.0)/127.0,((double)color.B)/255.0);
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
            t += 0.2;
            LVersor = new Vector3D(newX, newY, 100);
            Photo.Invalidate();
        }
    }
}
