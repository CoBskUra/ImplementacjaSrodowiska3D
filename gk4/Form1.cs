
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
        float3 vector;

        public Form1()
        {
            InitializeComponent();
            Api3D = new Bitmaps_intermediary(whithreboard, whitheboardBox);
            StationaryCamera = Api3D.Create_Camera(2, -4, -7,
                                                0, 0, 0,
                                                1, 20,
                                                MathF.PI / 4);
            FolowingCamera = Api3D.Create_Camera(-2, -4, 7,
                                                0, 0, 0,
                                                1, 20,
                                                MathF.PI / 4);
            StaringCamera = Api3D.Create_Camera(-0.1f, -6, -1,
                                                 0, 0, 0,
                                                 1, 20,
                                                 MathF.PI / 4);
            Api3D.Camera = StationaryCamera;



            //Api3D.Create_New_Figure();
            //{
            //    Api3D.Transform(0, 1, 0);
            //    int i = Api3D.Sphere(0.6f, 10, 10);
            //    Api3D[i].Material = new _3DApi.Components.Objects.Material((0.5f, 0.7f, 0.5f),
            //                                                                (0.8f, 0.4f, 0.3f),
            //                                                                (0.8f, 0.23f, 0.3f),
            //                                                                10);
            //    Api3D[i].Shading = _3DApi.Components.Objects.ShadingOption.Constant;
            //}

            Api3D.Create_New_Figure();
            {
                Api3D.Transform(0,0,0);
                Api3D.Add_rectangle(

                    Api3D.create_point((2, 0, 2),
                                        (0, 1, 0), (0, -1, 0), (0, 0, -1)),
                    Api3D.create_point((-2, 0, 2),
                                        (0, 1, 0), (0, -1, 0), (0, 0, -1)),
                    
                    
                    Api3D.create_point((-2, 0, -2),
                                        (0, 1, 0), (0, -1, 0), (0, 0, -1)),
                    Api3D.create_point((2, 0, -2),
                                        (0, 1, 0), (0, -1, 0), (0, 0, -1))
                    );
                Api3D.Add_rectangle(
                    Api3D.create_point((-2, 0, 2),
                                        (0, 1, 0), (0, -1, 0), (0, 0, -1)),
                    Api3D.create_point((2, 0, 2),
                                        (0, 1, 0), (0, -1, 0), (0, 0, -1)),
                    Api3D.create_point((2, 0, -2),
                                        (0, 1, 0), (0, -1, 0), (0, 0, -1)),
                    Api3D.create_point((-2, 0, -2),
                                        (0, 1, 0), (0, -1, 0), (0, 0, -1))
                    );
                Api3D[i].LineColor = Color.Green.negation();
                Api3D[i].Material = new _3DApi.Components.Objects.Material(Color.Green.ConvertTofloat3(), Color.Green.ConvertTofloat3(), Color.Green.ConvertTofloat3(), 4);
            }

            Api3D.Create_New_Figure();
            {
                Api3D.Transform(-1.5f, 0, 1);
                int i = Api3D.Person(0.6f, 0.3f);
                
                Api3D[i].LineColor = Color.Blue.negation();
                Api3D[i].Material = new _3DApi.Components.Objects.Material(Color.Blue.ConvertTofloat3(), Color.Blue.ConvertTofloat3(), Color.Blue.ConvertTofloat3(), 4);
            }

            Api3D.Create_New_Figure();
            {
                Api3D.Transform(1f, 0, -0.9f);
                int i = Api3D.Sphere(0.3f, 6, 7);
                Api3D[i].LineColor = Color.Khaki.negation();
                StaringCamera.LookAt = Api3D[i].FigureCenter;
                Api3D[i].Material = new _3DApi.Components.Objects.Material(Color.Khaki.ConvertTofloat3(), Color.Khaki.ConvertTofloat3(), Color.Khaki.ConvertTofloat3(), 4);
            }

            Api3D.Create_New_Figure();
            {
                Api3D.Transform(1.5f, 0, -1);
                int i = Api3D.Person(0.6f, 0.3f);
                Api3D[i].LineColor = Color.Red.negation();
                Api3D[i].Material = new _3DApi.Components.Objects.Material(Color.Red.ConvertTofloat3(), Color.Red.ConvertTofloat3(), Color.Red.ConvertTofloat3(), 4);

            }



            Api3D.Create_New_Lighte();
            {

                Api3D.Transform(-4f, 0, -1);
                int i = Api3D.Sphere(0.5f, 6, 7);
                Api3D.GetLighte(i).LineColor = Color.DarkCyan;

                Api3D.GetLighte(i).SetVarbles(0.6f, 0.2f, 0.032f, Color.DarkCyan.negation().ConvertTofloat3(), (1f, 1f, 1f), (1f, 1f, 1f));

                StaringCamera.LookAt = Api3D[i].FigureCenter;
            }





            vector = Api3D[2].FigureCenter - Api3D[1].FigureCenter + (-0.6f, 0, 0.6f);



            Api3D.Create_New_Lighte();
            {
                Api3D.Transform(2, 3.5f, 0);
                int i = Api3D.Sphere(0.5f, 4, 4);
                Api3D.GetLighte(i).SetVarbles(0.6f, 0.2f, 0.032f, Color.Yellow.ConvertTofloat3(), (1f, 0.5f, 0.2f), (0.5f, 0.6f, 0.6f));
                Api3D.GetLighte(i).LineColor = Color.Yellow.negation();
            }


            Api3D.Create_New_Lighte();
            {
                Api3D.Transform(-2, 1f, 0);
                int i = Api3D.Squer(0.2f);
                Api3D.GetLighte(i).SetVarbles(1f, 0.2f, 0.032f, Color.Orange.ConvertTofloat3(), (0.1f, 0.5f, 0.2f), (0.5f, 0.6f, 0.6f));
                Api3D.GetLighte(i).LineColor = Color.Red.negation();
                Api3D.GetLighte(i).Rotation_Center = (-1, 1, 0);
            }


            Api3D.drawAll();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            float moveTo = 0.01f* direction;
            
            i += direction;
            if (i == 40)
            {
                direction = -1;
            }
            else if(i==0)
            {
                direction = 1;
            }
            float3 skalar = vector / 40 * direction;
            int d = 100;
            //Api3D.rotate_x(2 * MathF.PI / d);
            //Api3D.rotate_y(MathF.PI / d);
            //Api3D[0].rotate_y(MathF.PI / d);
            StaringCamera.LookAt = Api3D[2].FigureCenter;
            float3 oldPosition = Api3D[2].FigureCenter;
            Api3D[2].Move(skalar.x, skalar.y, skalar.z);
            Api3D.drawAll();
            Api3D[2].rotate_x(MathF.PI / 100);
            Api3D[2].rotate_z(MathF.PI / 10);
            Api3D[6].rotate_y(MathF.PI / 10);



            //this.Text = Api3D[0].FigureCenter.ToString();
            Api3D.drawAll();

            FolowingCamera.Position -= Api3D[2].FigureCenter - oldPosition;
            FolowingCamera.LookAt -= Api3D[2].FigureCenter - oldPosition;
        }

        private void CameraButtonStationary_Click(object sender, EventArgs e)
        {
            Api3D.Camera = StationaryCamera;
            Api3D.drawAll();
        }

        private void CameraStaringButton_Click(object sender, EventArgs e)
        {
            Api3D.Camera = StaringCamera;
            Api3D.drawAll();
        }

        private void CameraFolowingButton_Click(object sender, EventArgs e)
        {
            Api3D.Camera = FolowingCamera;
            Api3D.drawAll();
        }

        private void ConstantShadingButton_Click(object sender, EventArgs e)
        {
            Api3D.Shading = _3DApi.Components.Objects.ShadingOption.Constant;
            Api3D.drawAll();
        }

        private void NoneShadingButton_Click(object sender, EventArgs e)
        {
            Api3D.Shading = _3DApi.Components.Objects.ShadingOption.None;
            Api3D.drawAll();
        }

        private void GouraudShadingButton_Click(object sender, EventArgs e)
        {
            Api3D.Shading = _3DApi.Components.Objects.ShadingOption.Gouraud;
            Api3D.drawAll();
        }

        private void PhongShadingButton_Click(object sender, EventArgs e)
        {
            Api3D.Shading = _3DApi.Components.Objects.ShadingOption.Phonge;
            Api3D.drawAll();
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
