using gk4.Matrix;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gk4.Shapes
{
    public class Figure
    {

        public Color LineColor;
        public List<Trialagle> Trialagles = new List<Trialagle>();

        private float3 Rads => Trialagles[0].a.Rads;

        public virtual void Add(Trialagle trarangle)
        {
            if (!Trialagles.Contains(trarangle))
                Trialagles.Add(trarangle);
        }

        // rysuje figure
        public void drawMe(ref Bitmap Whitheboard)
        {
            foreach (var t in Trialagles)
                t.drawMe(LineColor, ref Whitheboard);
        }

        private float Count_Rad(float rad, float curentRad)
        {
            if (MathF.Abs(rad + curentRad) >= MathF.PI)
            {
                return rad + curentRad - MathF.Round((rad + curentRad) / (MathF.PI * 2)) * MathF.PI * 2;
            }
            else
                return rad + curentRad;
        }

        public void rotate_x(float rad)
        {

            rad = Count_Rad(rad, Rads.x);

            foreach (var trarangle in Trialagles)
                trarangle.rotate_x(rad);
        }

        public void rotate_y(float rad)
        {
            rad = Count_Rad(rad, Rads.y);
            foreach (var trarangle in Trialagles)
                trarangle.rotate_y(rad);
        }

        public void rotate_z(float rad)
        {
            rad = Count_Rad(rad, Rads.z);
            foreach (var trarangle in Trialagles)
                trarangle.rotate_z(rad);
        }

        public void rotate(float rad)
        {
            rotate_x(rad);
            rotate_y(rad);
            rotate_z(rad);
        }

        public (float x, float y, float z) Rotation_Center
        {
            set
            {
                foreach (var t in Trialagles)
                    t.Rotation_Center = value;
            }
            get
            {
                return Trialagles[0].Rotation_Center;
            }
        }

        public void ResetRotationCenter()
        {

            foreach (var t in Trialagles)
                t.ResetRotationCenter();
        }

        public void Move(float x, float y, float z)
        {
            foreach (var t in Trialagles)
                t.Move(x, y, z);
        }
    }
}
