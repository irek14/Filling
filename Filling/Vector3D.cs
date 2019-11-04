using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filling
{
    public class Vector3D
    {
        public double x;
        public double y;
        public double z;

        public Vector3D(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector3D operator -(Vector3D v, Vector3D w)
        {
            return new Vector3D(v.x - w.x, v.y - w.y, v.z - w.z);
        }

        public static Vector3D operator*(double a, Vector3D v)
        {
            return new Vector3D(a * v.x, a * v.y, a * v.z);
        }

        public static double operator *(Vector3D v, Vector3D w)
        {
            return v.x * w.x + v.y * w.y + v.z * w.z;
        }

        public void Normalize()
        {
            double length = Math.Sqrt(x * x + y * y + z * z);
            x = x / length;
            y = y / length;
            z = z / length;
        }
    }
}
