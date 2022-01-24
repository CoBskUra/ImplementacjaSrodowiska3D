using gk4.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gk4
{
    public class Trialagle
    {

        public List<Edge> Edges = new List<Edge>();
        public Point3[] Verticles = new Point3[3];

        public Trialagle(Point3 a, Point3 b, Point3 c)
        {
            Verticles[0] = a;
            Verticles[1] = b;
            Verticles[2] = c;

            var e1 = new Edge(a, b);
            var e2 = new Edge(b, c);
            var e3 = new Edge(c, a);

            Add(e1); Add(e2); Add(e3);
        }

        public void Add(Edge e)
        {
            if (Edges.Count < 3)
            {
                if (!Edges.Contains(e))
                    Edges.Add(e);
            }
        }


        public void rotate_x(float rad)
        {
            foreach (var e in Edges)
                e.rotate_x(rad);
        }

        public void rotate_y(float rad)
        {
            foreach (var e in Edges)
                e.rotate_y(rad);
        }

        public void rotate_z(float rad)
        {
            foreach (var e in Edges)
                e.rotate_z(rad);
        }

        public void rotate(float rad)
        {
            foreach (var e in Edges)
                e.rotate(rad);
        }

        public (float x, float y, float z) Rotation_Center
        {
            set
            {

                foreach (var e in Edges)
                    e.Rotation_Center = value;
            }
        }

        public void ResetRotationCenter()
        {
            foreach (var e in Edges)
                e.ResetRotationCenter();
        }

        public void Move(float x, float y, float z)
        {
            foreach (var e in Edges)
                e.Move(x,y,z);
        }

    }
}
