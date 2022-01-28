using g4;
using gk4.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gk4
{
    

    // krawędź
    public class Edge
    {
        public Point3 v1, v2;

        public Edge(Point3 V1, Point3 V2)
        {
            this.v1 = V1;
            this.v2 = V2;
        }

        public void rotate_y(float rad)
        {
            v1.Rads.y = rad;
            v2.Rads.y = rad;
        }

        public void rotate_x(float rad)
        {

            v1.Rads.x = rad;
            v2.Rads.x = rad;
        }


        public void rotate_z(float rad)
        {
            v1.Rads.z = rad;
            v2.Rads.z = rad;
        }


        public (float x, float y, float z) Rotation_Center
        {
            set
            {

                v1.Rotation_Center = value;
                v2.Rotation_Center = value;
            }
            get
            {

                return v1.Rotation_Center;
            }
        }

        public void ResetRotationCenter()
        {

            v1.ResetRotationCenter();
            v2.ResetRotationCenter();
        }

        public void Move(float x, float y, float z)
        {
            v1.Move(x, y, z);
            v2.Move(x, y, z);
        }

        public static bool operator ==(Edge e1, Edge e2)
        {
            return (e1.v1 == e2.v1 && e1.v2 == e2.v2) ||
                        (e1.v1 == e2.v2 && e1.v2 == e2.v1);
        }

        

        public static bool operator !=(Edge e1, Edge e2)
        {
            return !(e1 == e2);
        }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            if (obj.GetType() == typeof(Edge))
                return (Edge)obj == this;
            else
                return false;

        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
