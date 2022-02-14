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
        int x1, y1, x2, y2, x3, y3; // pozycja odpowiednich punktów na bitmapie
        public float3 TrialagleCenter => (a.visableCoordinates + b.visableCoordinates + c.visableCoordinates) / 3;

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


        private void sort(ref (int x, int y) theHighest, ref (int x, int y) theLowest, ref (int x, int y) medium)
        {
            if (theHighest.y < theLowest.y)
            {
                var tmp = theHighest;
                theHighest = theLowest;
                theLowest = tmp;
            }
            if (medium.y < theLowest.y)
            {
                var tmp = medium;
                medium = theLowest;
                theLowest = tmp;
            }
            else if (medium.y > theHighest.y)
            {
                var tmp = medium;
                medium = theHighest;
                theHighest = tmp;
            }
        }

        


        private void fill_me(ref Bitmap Whitheboard, Color c, FillLine fillLine)
        {
            (int x, int y) theHighest = (x1, y1);
            (int x, int y) theLowest = (x2, y2);
            (int x, int y) medium = (x3, y3);

            sort(ref theHighest, ref theLowest, ref medium);

            int TheLowestTheHighests_Highte = theHighest.y - theLowest.y;
            int theLowestMedium_Highte = medium.y - theLowest.y;
            int MediumTheHighests_Highte = theHighest.y - medium.y;


            double theLowestMediumSkalar = ((double)medium.x - (double)theLowest.x) / (double)theLowestMedium_Highte;
            double theLowestTheHighestSkalar = ((double)theHighest.x - (double)theLowest.x) / (double)TheLowestTheHighests_Highte;
            double MediumTheHighestsSkalar = ((double)theHighest.x - (double)medium.x) / (double)MediumTheHighests_Highte;

            


            if (TheLowestTheHighests_Highte != 0 && theLowestMedium_Highte != 0)
            {
                for (int i = 0; i <= theLowestMedium_Highte; i++)
                {
                    fillLine.fillLine((int)(theLowest.x + theLowestTheHighestSkalar * i),
                                (int)(theLowest.x + theLowestMediumSkalar * i),
                                i + theLowest.y,
                                ref Whitheboard, c);

                }
            }
            if (MediumTheHighests_Highte != 0 && TheLowestTheHighests_Highte != 0)
            {
                for (int i = theLowestMedium_Highte; i <= TheLowestTheHighests_Highte; i++)
                {
                    fillLine.fillLine((int)(theLowest.x + theLowestTheHighestSkalar * i),
                                (int)(medium.x + MediumTheHighestsSkalar * (i - theLowestMedium_Highte)),
                                i + theLowest.y,
                                ref Whitheboard, c);
                }
            }

        }

        private void countPosition()
        {
            x1 = a.x_parm_on_bitmap; y1 = a.y_parm_on_bitmap;
            x2 = b.x_parm_on_bitmap; y2 = b.y_parm_on_bitmap;
            x3 = c.x_parm_on_bitmap; y3 = c.y_parm_on_bitmap;
        }

        private float3 normalVector =>  MatrixTransformationNeededTo3DModeling.cross_product(
                b.visableCoordinates - a.visableCoordinates,
                c.visableCoordinates - a.visableCoordinates
                );

        public void drawMe(Color LineColor, ref Bitmap Whitheboard, FillLine fillLine)
        {
            countPosition();
            if (normalVector.z > 0)
            {
                fill_me(ref Whitheboard, Color.FromArgb(255 - LineColor.R, 255 - LineColor.G, 255 - LineColor.B), fillLine);
                drawing_lines.drawe(x1, y1, x2, y2, LineColor, ref Whitheboard);
                drawing_lines.drawe(x2, y2, x3, y3, LineColor, ref Whitheboard);
                drawing_lines.drawe(x3, y3, x1, y1, LineColor, ref Whitheboard);
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
                return a.CurenntFigureCenter;
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
