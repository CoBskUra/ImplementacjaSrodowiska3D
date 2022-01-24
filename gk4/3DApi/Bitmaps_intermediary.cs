﻿using g4;
using gk4._3DApi.Components;
using gk4.Shapes;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace gk4
{
    

    // Pośrednik między bitmapą a programistą
    public class Bitmaps_intermediary
    {
        Bitmap Whitheboard;
        PictureBox WhitheboardBox;

        // zmienne wyznaczające rozmiar widzialnego świata
        int Max_z, Max_y, Max_x;

        public Color DefaultLineColor = Color.FromArgb(100, 255, 100);


        private float Transform_x = 0, Transform_y = 0, Transform_z = 0;

        public Camera Camera;
        

        // zbiór figur, każda figura zawiera sie w zbiorze krawędzi
        List<Figure> Figures = new List<Figure>();

        public int FiguresNumber => Figures.Count; 

        public Bitmaps_intermediary(Bitmap w, PictureBox pb)
        {
            WhitheboardBox = pb;
            Whitheboard = w;

            // ustawiami parametry widzialności
            Max_z = 100;
            Max_x = w.Width;
            Max_y = w.Height;
        }

        public void Transform(float x, float y, float z)
        {
            Transform_x = -x;
            Transform_y = -y;
            Transform_z = -z;
        }

        public void setRotationCenter(int id, float x, float y, float z)
        {

            Figures[id].Rotation_Center = (x, y, z);

        }

        public void ResetRotationCenter(int id)
        {
            Figures[id].ResetRotationCenter();
        }


        // tworzy point3 by za każdym razem nie pisać parametrów widzialmości
        public Point3 create_point(int x, int y, int z)
        {

            return new Point3(
                x + Transform_x, y + Transform_y, z + Transform_z, 1,
                Max_x, Max_y, Max_z,
                Transform_x, Transform_y, Transform_z, Camera
                );
        }

        public Point3 create_point(float x, float y, float z)
        {
            return new Point3(
                x + Transform_x, y + Transform_y, z + Transform_z, 1,
                Max_x, Max_y, Max_z,
                Transform_x, Transform_y, Transform_z, Camera
                );
        }

        // tworzy prostokąt by za każdym razem nie pisać parametrów widzialmości
        public void Add_rectangle(Point3 a, Point3 b, Point3 c, Point3 d)
        {
            Add_Trialagle(a, b, c);
            Add_Trialagle(c, d, a);
        }

        // tworzy prostokąt by za każdym razem nie pisać parametrów widzialmości
        public void Add_Trialagle(Point3 a, Point3 b, Point3 c)
        {
            Add(new Trialagle(a, b, c));
        }

        // rysuje wybraną figure
        private void drawFigure(int i)
        {
            Figures[i].drawMe(ref Whitheboard);
        }


        // rysuje wszystkie krawędzi
        public void drawAll()
        {
            Whitheboard = new Bitmap(Whitheboard.Width, Whitheboard.Height);
            for (int i = 0; i < Figures.Count; i++)
                drawFigure(i);
            WhitheboardBox.Image = Whitheboard;
        }

        // rysuje figury o wybranym id
        public void drawSelected(params int[] id)
        {
            Whitheboard = new Bitmap(Whitheboard.Width, Whitheboard.Height);
            foreach (var i in id)
                drawFigure(i);
            WhitheboardBox.Image = Whitheboard;
        }

        // dodaje krawędź do figury jak i do zbioru krawędzi
        public void Add(Trialagle trialagle)
        {
            this[Figures.Count - 1].Add(trialagle);
        }

        // konczy dodawanie do fugury i pozwala dodawać do nowej figury
        public void Create_New_Figure()
        {
            Figures.Add(new Figure());
            Figures[FiguresNumber - 1].LineColor = DefaultLineColor;
        }

        public Camera Create_Camera(float position_x, float position_y, float position_z,
                        float lookAt_x, float lookAt_y, float lookAt_z,
                        float n, float f,
                        float fov)
        {
            return new Camera(position_x, position_y, position_z, lookAt_x, lookAt_y, lookAt_z, Max_x, Max_y, n, f, fov);
        }

        public Figure this[int i]
        {
            get { return Figures[i]; }
        }

        /// <summary>
        /// Roracje 
        /// </summary>
        /// <param name="rad"></param>
        /// <param name="id"></param>
        /// 
                // poniższe funkcje obracają wybrane figury względem danej osi
                public void rotate_x(float rad, int id)
                {
                        Figures[id].rotate_x(rad);
                }

                public void rotate_y(float rad, int id)
                {
                        Figures[id].rotate_y(rad);
                }

                public void rotate_z(float rad, int id)
                {
                        Figures[id].rotate_z(rad);
                }

                // wybrane figury obraca względem wszystkich osi
                public void rotate(float rad, int id)
                {
                        Figures[id].rotate(rad);
                }

                // poniższe funkcje obracają wybrane figury względem danej osi
                public void rotate_x(float rad, params int[] id)
                {
                    foreach (var i in id)
                        Figures[i].rotate_x(rad);
                }

                public void rotate_y(float rad, params int[] id)
                {
                    foreach (var i in id)
                        Figures[i].rotate_y(rad);
                }

                public void rotate_z(float rad, params int[] id)
                {
                    foreach (var i in id)
                        Figures[i].rotate_z(rad);
                }

                // wybrane figury obraca względem wszystkich osi
                public void rotate(float rad, params int[] id)
                {
                    foreach (var i in id)
                        Figures[i].rotate(rad);
                }

                // poniższe funkcje obracają wszystkie krawędzi względem danej osi
                public void rotate_x(float rad)
                {
                    foreach (var f in Figures)
                        f.rotate_x(rad);
                }


                // mówi o jaki kąt ma się coś przesunąc
                public void rotate_y(float rad)
                {
                    foreach (var f in Figures)
                        f.rotate_y(rad);
                }


                public void rotate_z(float rad)
                { 
                    foreach (var f in Figures)
                        f.rotate_z(rad);
                }

                // wszystkie krawędzie obraca względem wszystkich osi
                public void rotate(float rad)
                {
                    foreach (var f in Figures)
                        f.rotate(rad);
                }

    }
}
