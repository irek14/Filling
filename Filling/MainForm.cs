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
        Point vertex_to_move;
        bool isVertexMoving = false;

        public MainForm()
        {
            InitializeComponent();
            triangles = GenerateTriangles(6, 8);
            Photo.Invalidate();
        }

        private void Photo_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Resources.spiderman, new Point(0,0));
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

        private void MoveVertex(Point p)
        {
            CorrectTrianglesAfterRelation(vertex_to_move, p);
            vertex_to_move = p;
        }

        private void CorrectTrianglesAfterRelation(Point old_point, Point new_point)
        {
            for(int i=0; i<triangles.Count; i++)
            {
                if (triangles[i].p1 == old_point)
                    triangles[i].p1 = new_point;
                else if (triangles[i].p2 == old_point)
                    triangles[i].p2 = new_point;
                else if (triangles[i].p3 == old_point)
                    triangles[i].p3 = new_point;
            }

        }

        private Point? GetVertexFromTriangle(Point p)
        {
            foreach (var triangle in triangles)
            {
                if (CheckIfVertex(p, triangle.p1))
                    return triangle.p1;
                if (CheckIfVertex(p, triangle.p2))
                    return triangle.p2;
                if (CheckIfVertex(p, triangle.p3))
                    return triangle.p3;
            }

            return null;
        }

        private bool CheckIfVertex(Point p, Point vertex)
        {
            if (Math.Abs(p.X - vertex.X) <= 5 && Math.Abs(p.Y - vertex.Y) <= 5)
                return true;

            return false;
        }

    }
}
