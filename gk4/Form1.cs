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
            Api3D.Camera = Api3D.Create_Camera(0f, 0, 0, 0, 5, 5, 1, 200, 1);
            MessageBox.Show( Api3D.Camera.Proj.ToString());


            Api3D.Create_New_Figure();
            {
                int i = Api3D.Sphere(3, 10, 20);
                //Api3D[i].Rotation_Center = (1.0f, 1.0f, 0);
            }
            Api3D.Create_New_Figure();
            {
                int i = Api3D.Squer(1);
                Api3D[i].LineColor = Color.White;
            }


            //Bitmap_API.rotate_x(MathF.PI / 2);
            Api3D.drawAll();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int d = 40;
            Api3D[0].ResetRotationCenter();
            Api3D[1].ResetRotationCenter();
            Api3D.rotate_y(i * MathF.PI / d);
            Api3D[0].Rotation_Center = (1, 1, 1);
            Api3D[1].Rotation_Center = (1, 1, 1);
            Api3D.rotate_y(i * MathF.PI / d);
            Api3D.drawAll();
            Api3D.Camera.lookAt = (MathF.Sin(4*i * MathF.PI / d), 5, 5);
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
