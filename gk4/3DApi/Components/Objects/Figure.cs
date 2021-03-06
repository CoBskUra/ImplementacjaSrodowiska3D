using gk4._3DApi.Components.Objects.Components;
using gk4._3DApi.Drarwing;
using gk4.Matrix;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace gk4._3DApi.Components.Objects
{
    public class Material
    {
        public float3 specular;
        public float3 diffuse;
        public float3 ambient;
        public int shininess;

        public Material(float3 specular, float3 diffuse, float3 ambient, int shininess)
        {
            this.specular = specular;  
            this.diffuse = diffuse; 
            this.ambient = ambient; 
            this.shininess = shininess;
        }
    }

    public enum ShadingOption
    {
        None,
        Constant,
        Gouraud,
        Phonge
    }

    public class Figure
    {

        public Color LineColor;
        public List<Trialagle> Trialagles = new List<Trialagle>();
        public Material Material = new Material((1f,0.1f,0.5f), (1f, 0.1f, 0f), (1f, 0.5f, 0f), 2);
        public ShadingOption Shading = ShadingOption.None;

        private float3 Rads => Trialagles[0].a.Rads;

        public virtual void Add(Trialagle trarangle)
        {
            if (!Trialagles.Contains(trarangle))
                Trialagles.Add(trarangle);
        }

        // rysuje figure
        public void drawMe(ref Bitmap Whitheboard, FillTrialangle fillLine)
        {
            fillLine.shading = Shading;
            fillLine.Material = Material;
            foreach (var t in Trialagles)
            {
                fillLine.trialagleToFill = t;
                t.drawMe(LineColor, ref Whitheboard, fillLine);
            }
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

        public float3 Rotation_Center
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

        public float3 FigureCenter
        {
            
            get
            {
                return Trialagles[0].FigureCenter;
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
