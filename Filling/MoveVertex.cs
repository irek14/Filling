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
        Point vertex_to_move;

        private void MoveVertex(Point p)
        {
            CorrectTrianglesAfterRelation(vertex_to_move, p);
            vertex_to_move = p;
        }

        private void CorrectTrianglesAfterRelation(Point old_point, Point new_point)
        {
            for (int i = 0; i < triangles.Count; i++)
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
