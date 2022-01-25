using gk4._3DApi.Components;
using gk4._3DApi.Drarwing_on_bitmap;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace gk4
{
    public partial class Form1 : Form
    {
        Bitmap whithreboard = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                       Screen.PrimaryScreen.Bounds.Height);
        int i = 0;
        Bitmaps_intermediary Api3D;
        public Form1()
        {
            InitializeComponent();
            Api3D = new Bitmaps_intermediary(whithreboard, whitheboardBox);
            Api3D.Camera = Api3D.Create_Camera(-5, 0, 0,
                                                1, -2, 0,
                                                10, 200,
                                                2);

            Api3D.Create_New_Figure();
            {
                Api3D.Transform(0, 4, 10);
                int i = Api3D.Sphere(3, 10, 20);
                Api3D[i].Rotation_Center = (0f, 0f, 0);
            }
            Api3D.Create_New_Figure();
            {
                Api3D.Transform(1, 1, -10);
                int i = Api3D.Squer(1);
                Api3D[i].LineColor = Color.White;
            }


            //Bitmap_API.rotate_x(MathF.PI / 2);
            Api3D.drawAll();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int d = 10;
            Api3D.rotate_z(i * MathF.PI / d);
            Api3D.drawAll();
            i++;
            if (i == 2 * d)
                i = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
        }
    }
}
