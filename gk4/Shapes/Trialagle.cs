using gk4.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gk4
{
    public class Trialagle
    {
        public Point3 a,b,c;


        public Trialagle(Point3 a, Point3 b, Point3 c)
        { 

            this.a = a;
            this.b = b;
            this.c = c;
        }

        //public List<Edge>[] ET()
        //{

        //    List<Edge>[] et = new List<Edge>[ymax - ymini + 1];
        //    for (int j = 0; j < et.Length; j++)
        //        et[j] = new List<Edge>();

        //    int min_y = ymini;
        //    foreach (var p in vertex)
        //    {
        //        var es = edges(p);
        //        if (es.e1.ymini - ymini >= 0 && !et[es.e1.ymini - ymini].Contains(es.e1))
        //        {
        //            et[es.e1.ymini - ymini].Add(es.e1);
        //        }

        //        if (es.e2.ymini - ymini >= 0 && !et[es.e2.ymini - ymini].Contains(es.e2))
        //        {
        //            et[es.e2.ymini - ymini].Add(es.e2);
        //        }

        //    }


        //    return et;

        //}

        //public void fillMe(ref Bitmap whiteboard)
        //{
            

        //    List<Edge> ATE = new List<Edge>();
        //    var ET = this.ET();

        //    int x1 = Edges[0].v1.x_parm_on_bitmap;
        //    int y1 = Edges[0].v1.y_parm_on_bitmap;
        //    int x2 = Edges[0].v2.x_parm_on_bitmap;
        //    int y2 = Edges[0].v2.y_parm_on_bitmap;
        //    int x2 = Edges[1].v2.x_parm_on_bitmap;
        //    int y2 = Edges[1].v2.y_parm_on_bitmap;

        //    int y_min = this.ymini;
        //    int y = y_min;
        //    int ylast = ymax;
        //    int xmini = this.xmini;


        //    while (y != ylast + 1)
        //    {


        //        ATE.AddRange(ET[y - y_min]);

        //        ATE = ATE.OrderBy(a => a.ATEx).ToList();
        //        double[] N = new double[3];

        //        for (int i = 0; i + 1 < ATE.Count; i += 2)
        //        {

        //            int x1 = (int)ATE[i].ATEx;
        //            int x2 = (int)ATE[i + 1].ATEx;

        //            if (x1 < 0)
        //                break;



        //            for (int j = x1; j <= x2; j++)
        //            {
        //                Color c = Color.Black;
        //                double z = 0;
        //                N[0] = 0; N[1] = 0; N[2] = 1;
        //                c = Color.Red;
                        
        //                whiteboard.SetPixel(j, y, c);
        //            }

        //        }
        //        y++;
        //        for (int j = ATE.Count() - 1; j >= 0; j--)
        //            if (ATE[j].ymax == y)
        //                ATE.RemoveAt(j);
        //            else
        //                ATE[j].ATEx += ATE[j].skalar;

        //    }
        //}

        //public void Add(Edge e)
        //{
        //    Edges.Add(e);
        //}
        
        //}
        

        public void drawMe(Color LineColor, ref Bitmap Whitheboard)
        {
            int x1 = a.x_parm_on_bitmap, y1 = a.y_parm_on_bitmap;
            int x2 = b.x_parm_on_bitmap, y2 = b.y_parm_on_bitmap;
            int x3 = c.x_parm_on_bitmap, y3 = c.y_parm_on_bitmap;
            drawing_lines.drawe(x1,y1,x2,y2, LineColor, ref Whitheboard);
            drawing_lines.drawe(x2,y2,x3,y3, LineColor, ref Whitheboard);
            drawing_lines.drawe(x3,y3,x1,y1, LineColor, ref Whitheboard);
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


        public (float x, float y, float z) Rotation_Center
        {
            set
            {
                a.Rotation_Center = Rotation_Center;
                b.Rotation_Center = Rotation_Center;
                c.Rotation_Center = Rotation_Center;
            }
            get
            {
                return a.Rotation_Center;
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
