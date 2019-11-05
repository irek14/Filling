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
            else if(current_mode == PaintMode.Interpolate)
            {
                Parallel.ForEach(triangles, (triangle) =>
                {
                    FillPolygonInterpolate(triangle, triangle.kd, triangle.ks, triangle.m);
                });
            }
            else
            {
                Parallel.ForEach(triangles, (triangle) =>
                {
                    FillPolygonHybrid(triangle, triangle.kd, triangle.ks, triangle.m);
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

        #region menu
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
    #endregion
}
