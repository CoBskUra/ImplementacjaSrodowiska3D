using gk4._3DApi.Drarwing;
using gk4.Matrix;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace gk4._3DApi.Components.Objects.Components
{
    public class Trialagle
    {
        public Point3 a,b,c;
        public int x1, y1, x2, y2, x3, y3, z1, z2, z3; // pozycja odpowiednich punktów na bitmapie
        public float3 TrialagleCenter => (a.visableCoordinates + b.visableCoordinates + c.visableCoordinates) / 3;
        public float3 TrialagleCenterInWorld => (a.WorldCoordinates + b.WorldCoordinates + c.WorldCoordinates) / 3;

        public Trialagle(Point3 a, Point3 b, Point3 c)
        { 
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public Trialagle(Point3 a, Point3 b, Point3 c, float3 normalvector): this(a, b,c)
        {

            a.NormalVector = normalvector;
            b.NormalVector = normalvector;
            c.NormalVector = normalvector;

        }
        


        
        private void countPosition()
        {
            x1 = a.x_parm_on_bitmap; y1 = a.y_parm_on_bitmap; z1 = a.z_parm_on_bitmap;
            x2 = b.x_parm_on_bitmap; y2 = b.y_parm_on_bitmap; z2 = b.z_parm_on_bitmap;
            x3 = c.x_parm_on_bitmap; y3 = c.y_parm_on_bitmap; z3 = c.z_parm_on_bitmap;

        }

        public float3 normalVector =>  MatrixTransformationNeededTo3DModeling.cross_product(
                b.visableCoordinates - a.visableCoordinates,
                c.visableCoordinates - a.visableCoordinates
                );

        public void drawMe(Color LineColor, ref Bitmap Whitheboard, FillTrialangle fillLine)
        {
            countPosition();
            if (normalVector.z > 0)
            {
                fillLine.fill_me(ref Whitheboard, Color.FromArgb(255 - LineColor.R, 255 - LineColor.G, 255 - LineColor.B), LineColor);
                
            }
        }

        public void rotate_x(float rad)
        { 
            a.Rads.x = rad;
            b.Rads.x = rad;
            c.Rads.x = rad;
        }

        public void rotate_y(float rad)
        {
            a.Rads.y = rad;
            b.Rads.y = rad;
            c.Rads.y = rad;
        }

        public void rotate_z(float rad)
        {
            a.Rads.z = rad;
            b.Rads.z = rad;
            c.Rads.z = rad;
        }


        public float3 Rotation_Center
        {
            set
            {
                a.Rotation_Center = value;
                b.Rotation_Center = value;
                c.Rotation_Center = value;
            }
            get
            {
                return a.Rotation_Center;
            }
        }

        public float3 FigureCenter
        {

            get
            {
                return a.FigureCenter;
            }
        }

        public void ResetRotationCenter()
        {
            a.ResetRotationCenter();
            b.ResetRotationCenter();
            c.ResetRotationCenter();
        }

        public void Move(float x, float y, float z)
        {
            a.Move(x, y, z);
            b.Move(x, y, z);
            c.Move(x, y, z);
        }

    }
}
