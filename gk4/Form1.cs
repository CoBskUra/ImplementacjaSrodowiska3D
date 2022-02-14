
using gk4._3DApi.Drarwing;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using gk4.Matrix;
using gk4._3DApi;
using gk4._3DApi.Components;

namespace gk4
{
    public partial class Form1 : Form
    {
        Bitmap whithreboard = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                       Screen.PrimaryScreen.Bounds.Height);
        int i = 0;
        int direction = 1;

        Bitmaps_intermediary Api3D;
        Camera FolowingCamera, StationaryCamera, StaringCamera;


        public Form1()
        {
            InitializeComponent();
            Api3D = new Bitmaps_intermediary(whithreboard, whitheboardBox);
            StationaryCamera = Api3D.Create_Camera(0, 3, -5,
                                                0, 0, 0,
                                                1, 20,
                                                MathF.PI / 4);
            FolowingCamera = Api3D.Create_Camera(0, 3, -5,
                                                0, 0, 0,
                                                1, 20,
                                                MathF.PI / 4);
            StaringCamera = Api3D.Create_Camera(-2, -4, 7,
                                                0, 0, 0,
                                                1, 20,
                                                MathF.PI / 4);
            Api3D.Camera = StationaryCamera;



            Api3D.Create_New_Figure();
            {
                Api3D.Transform(0, 1, 0);
                int i = Api3D.Sphere(0.6f, 6, 5);
                Api3D[i].Shading = _3DApi.Components.Objects.ShadingOption.Constant;
            }

            Api3D.Create_New_Figure();
            {
                Api3D.Transform(0, 0, 0);
                int i = Api3D.Squer(0.5f);
                Api3D[i].Rotation_Center = (-1, -1f, 0);
                Api3D[i].LineColor = Color.White;
                Api3D[i].Shading = _3DApi.Components.Objects.ShadingOption.Constant;
            }

            Api3D.Create_New_Figure();
            {
                Api3D.Transform(0, 0, 0);
                int i = Api3D.Sphere(1f, 10, 10);
                Api3D[i].Rotation_Center = (-1, -1f, 0);
                Api3D[i].LineColor = Color.Green;
                Api3D[i].Shading = _3DApi.Components.Objects.ShadingOption.Constant;
            }

            Api3D.Create_New_Figure();
            {
                Api3D.Transform(0, 0, 0);
                int i = Api3D.Squer(0.5f);
                Api3D[i].LineColor = Color.Yellow;
                Api3D[i].Shading = _3DApi.Components.Objects.ShadingOption.Constant;
            }

            Api3D.Create_New_Figure();
            {
                Api3D.Transform(0.4327f, 0, 0);
                int i = Api3D.Squer(0.5f);
                Api3D[i].Rotation_Center = (1, 5, 0);
                Api3D[i].LineColor = Color.Beige;
            }

            Api3D.Create_New_Lighte();
            {
                Api3D.Transform(0, 0, 0);
                int i = Api3D.Sphere(0.5f, 4,4);
                Api3D.GetLighte(i).SetVarbles(1, 0.09f, 0.032f, (1f, 0.5f, 0.2f, 1f), (1f, 0.5f, 0.2f, 1f), (1f, 0.6f, 0.6f, 1f));
                Api3D.GetLighte(i).LineColor = Color.Red;
            }

            Api3D.drawAll();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            float moveTo = 0.01f* direction;
            i += direction;
            if (i == 100)
            {
                direction = -1;
            }
            else if(i==0)
            {
                direction = 1;
            }
            int d = 100;
            Api3D.rotate_x(2 * MathF.PI / d);
            Api3D.rotate_y(MathF.PI / d);
            Api3D[0].Rotation_Center = ((float)direction / 2, -(float)direction / 3, (float)direction);
            Api3D[0].rotate_y(MathF.PI / d);
            //Api3D[0].Move(-moveTo, moveTo, 0);
            StaringCamera.LookAt = Api3D[0].FigureCenter;
            FolowingCamera.LookAt = Api3D[0].FigureCenter;
            float3 oldPosition = Api3D[0].FigureCenter;
            //this.Text = Api3D[0].FigureCenter.ToString();


            Api3D.drawAll();
            FolowingCamera.Position = FolowingCamera.Position + Api3D[0].FigureCenter - oldPosition;

        }

        private void CameraButtonStationary_Click(object sender, EventArgs e)
        {
            Api3D.Camera = StationaryCamera;
        }

        private void CameraStaringButton_Click(object sender, EventArgs e)
        {
            Api3D.Camera = StaringCamera;
        }

        private void CameraFolowingButton_Click(object sender, EventArgs e)
        {
            Api3D.Camera = FolowingCamera;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            StartStop_Button.Text = StartStop_Button.Text == "Start" ? "Stop" : "Start";
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            var position = Api3D.Camera.Position;
            float move = 0.15f;
            this.Text = "asd";
            if (e.KeyCode == Keys.W)
            {
                Api3D.Camera.Position = (position.x, position.y, position.z + move);
                this.Text = "asd";
            }
            else if (e.KeyCode == Keys.S)
            {
                Api3D.Camera.Position = (position.x, position.y, position.z - move);
            }
            else if (e.KeyCode == Keys.A )
            {
                Api3D.Camera.Position = (position.x - move, position.y, position.z);
            }
            else if (e.KeyCode == Keys.D)
            {
                Api3D.Camera.Position = (position.x + move, position.y, position.z);
            }
            if (!timer1.Enabled)
                Api3D.drawAll();

        }

        
    }
}
