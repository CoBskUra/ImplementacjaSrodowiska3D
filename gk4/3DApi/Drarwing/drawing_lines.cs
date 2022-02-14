using System;
using System.Drawing;

namespace gk4._3DApi.Drarwing
{
    public static class drawing_lines
    {

        public static int PitagorasEquation(Point a, Point b)
        {
            return (int)(Math.Pow(a.X - b.X, 2)+ Math.Pow(a.Y - b.Y, 2));

        }
        

        public static void all_in(Point first, Point last, Color c, Bitmap whiteboard,
                                    int d, int incr1, int incr2, int add_to_x1, int add_to_y1, int add_to_x2, int add_to_y2)
        {
            int x1 = first.X;
            int y1 = first.Y;
            int x2 = last.X;
            int y2 = last.Y;


            int x = x1;
            int y = y1;
            if(x>0 && x<whiteboard.Width && y>0 && y<whiteboard.Height)
                whiteboard.SetPixel(x, y, c);
            while (x < x2 || y != y2)
            {
                if (d < 0) //choose S 
                {
                    d += incr1;
                    y = y + add_to_y1;
                    x = x + add_to_x1;
                }
                else //choose SE
                {
                    d += incr2;
                    x = x + add_to_x2;
                    y = y + add_to_y2;
                }
                if (x > 0 && x < whiteboard.Width && y > 0 && y < whiteboard.Height)
                    whiteboard.SetPixel(x, y, c);
                
            }
        }


        public static void drawe(int x1, int y1, int x2, int y2, Color c, ref Bitmap whiteboard)
        {


            if (x1 > x2)
            {
                int tem = x1;
                x1 = x2;
                x2 = tem;
                tem = y1;
                y1 = y2;
                y2 = tem;
            }


            int dx = x2 - x1;
            int dy = y2 - y1;
            int d, add_to_x1, add_to_x2, add_to_y1, add_to_y2, incr1, incr2;


            if (y2 >= y1)
            {
                if (x2 - x1 >= y2 - y1)
                {
                    d = 2 * dy - dx; //initial value of d
                    incr1 = 2 * dy; //increment used for move to E
                    incr2 = 2 * (dy - dx); //increment used for move to NE
                    add_to_x1 = 1;
                    add_to_y1 = 0;
                    add_to_x2 = 1;
                    add_to_y2 = 1;
                }
                else
                {
                    d = 2 * dx - dy; //initial value of d
                    incr1 = 2 * dx; //increment used for move to N
                    incr2 = 2 * (dx - dy); //increment used for move to NE
                    add_to_x1 = 0;
                    add_to_y1 = 1;
                    add_to_x2 = 1;
                    add_to_y2 = 1;
                }
            }
            else
            {
                if (x2 - x1 >= y1 - y2)
                {
                    d = 2 * dx + dy; //initial value of d
                    incr1 = 2 * (dx + dy); //increment used for move to SE
                    incr2 = 2 * dy; //increment used for move to E
                    add_to_x1 = 1;
                    add_to_y1 = -1;
                    add_to_x2 = 1;
                    add_to_y2 = 0;
                }
                else
                {
                    d = 2 * dx + dy; //initial value of d
                    incr1 = 2 * dx; //increment used for move to S
                    incr2 = 2 * (dy + dx); //increment used for move to SE
                    add_to_x1 = 0;
                    add_to_y1 = -1;
                    add_to_x2 = 1;
                    add_to_y2 = -1;
                }
            }

            all_in(new Point(x1, y1), new Point(x2, y2), c, whiteboard,
                                    d, incr1, incr2, add_to_x1, add_to_y1, add_to_x2, add_to_y2);


           
        }
    }
}