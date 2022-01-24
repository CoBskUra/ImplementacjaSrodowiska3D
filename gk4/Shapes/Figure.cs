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

        public virtual void Add(Trialagle trarangle)
        {
            if (!Trialagles.Contains(trarangle))
                Trialagles.Add(trarangle);
        }

        // rysuje figure
        public void drawMe(ref Bitmap Whitheboard)
        {
            foreach (var t in Trialagles)
                foreach (var e in t.Edges)
                    drawing_lines.drawe(e, LineColor, ref Whitheboard);
        }


        public void rotate_x(float rad)
        {
            foreach (var trarangle in Trialagles)
                trarangle.rotate_x(rad);
        }

        public void rotate_y(float rad)
        {
            foreach (var trarangle in Trialagles)
                trarangle.rotate_y(rad);
        }

        public void rotate_z(float rad)
        {
            foreach (var trarangle in Trialagles)
                trarangle.rotate_z(rad);
        }

        public void rotate(float rad)
        {
            foreach (var trarangle in Trialagles)
                trarangle.rotate(rad);
        }

        public (float x, float y, float z) Rotation_Center
        {
            set
            {
                foreach (var t in Trialagles)
                    t.Rotation_Center = value;
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
