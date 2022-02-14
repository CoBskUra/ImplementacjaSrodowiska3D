using g4;
using gk4._3DApi.Components;
using gk4._3DApi.Components.Objects;
using gk4._3DApi.Components.Objects.Components;
using gk4.Matrix;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace gk4
{
    
    public enum objectType
    {
        Figure,
        Lighte
    }

    // Pośrednik między bitmapą a programistą
    public class Bitmaps_intermediary
    {
        public Bitmap Whitheboard;
        public PictureBox WhitheboardBox;

        // zmienne wyznaczające rozmiar widzialnego świata
        int Max_z, Max_y, Max_x;

        public Color DefaultLineColor = Color.FromArgb(100, 255, 100);


        private float3 Transformation;

        public Camera Camera;
        

        // zbiór figur, każda figura zawiera sie w zbiorze krawędzi
        List<Figure> Figures = new List<Figure>();
        public int FiguresNumber => Figures.Count;

        List<Light> Lights = new List<Light>();
        public int LightsNumber => Lights.Count;

        public int ObjectID => objectType == objectType.Figure ? FiguresNumber -1 : LightsNumber -1;

        private objectType objectType = objectType.Figure;


        public Bitmaps_intermediary(Bitmap w, PictureBox pb)
        {
            WhitheboardBox = pb;
            Whitheboard = w;
            Transformation = new float3();
            // ustawiami parametry widzialności
            Max_z = 100;
            Max_x = w.Width;
            Max_y = w.Height;
        }

        public void Transform(float x, float y, float z)
        {
            Transformation.x = x;
            Transformation.y = y;
            Transformation.z = z;
        }

        public void setRotationCenter(int id, float x, float y, float z)
        {

            Figures[id].Rotation_Center = (x, y, z);

        }

        public void ResetRotationCenter(int id)
        {
            Figures[id].ResetRotationCenter();
        }

        public Point3 create_point(float x, float y, float z, float3 normalvector, float3 tangentialVector, float3 binormal)
        {
            return new Point3(
                (x - Transformation.x, y - Transformation.y, z - Transformation.z, 1),
                normalvector, tangentialVector, binormal,
                Max_x, Max_y, Max_z, 
                Transformation, Camera
                );
        }

        public Point3 create_point(float3 point, float3 normalvector, float3 tangentialVector, float3 binormal)
        {
            return new Point3(
                (point.x - Transformation.x, point.y - Transformation.y, point.z - Transformation.z, 1),
                normalvector, tangentialVector, binormal,
                Max_x, Max_y, Max_z,
                Transformation, Camera
                );
        }

        // tworzy prostokąt by za każdym razem nie pisać parametrów widzialmości
        public void Add_rectangle(Point3 a, Point3 b, Point3 c, Point3 d)
        {
            Add_Trialagle(a, b, c);
            Add_Trialagle(c, d, a);
        }

        // tworzy prostokąt by za każdym razem nie pisać parametrów widzialmości
        public void Add_Trialagle(Point3 a, Point3 b, Point3 c, float3? normalvector = null)
        {
            if (normalvector == null)
                Add(new Trialagle(a, b, c));
            else
                Add(new Trialagle(a, b, c, (float3)normalvector));
        }

        // rysuje wybraną figure
        private void drawFigure(int i)
        {
            Figures[i].drawMe(ref Whitheboard, Lights);
        }


        // rysuje wszystkie krawędzi
        public void drawAll()
        {
            Whitheboard = new Bitmap(Whitheboard.Width, Whitheboard.Height);
            // segreguje obiekty ze względu na odległość od kamery 
            List<(int i, float z)> cos = new List<(int i, float z)>();
            for (int i = 0; i < FiguresNumber; i++)
                cos.Add((i, Figures[i].FigureCenter.z));

            cos.Sort((a, b) => a.z < b.z ? 1 : -1);

            for (int i = 0; i < FiguresNumber; i++)
                drawFigure(cos[i].i);
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
            if (objectType == objectType.Figure)
                this[Figures.Count - 1].Add(trialagle);
            else if (objectType == objectType.Lighte)
            {
                GetLighte(LightsNumber - 1).Add(trialagle);
                Figures[FiguresNumber - 1] = GetLighte(LightsNumber - 1);
            }
        }

        // konczy dodawanie do fugury i pozwala dodawać do nowej figury
        public void Create_New_Figure()
        {
            Figures.Add(new Figure());
            Figures[FiguresNumber - 1].LineColor = DefaultLineColor;
            objectType = objectType.Figure;
        }

        // konczy dodawanie do fugury i pozwala dodawać do nowej figury
        public void Create_New_Lighte()
        {
            Create_New_Figure();
            Lights.Add(new Light());
            objectType = objectType.Lighte;
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

        public Light GetLighte(int i) => Lights[i];
        

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

                public void rotate(float rad, int id)
                {
                    Figures[id].rotate(rad);
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
