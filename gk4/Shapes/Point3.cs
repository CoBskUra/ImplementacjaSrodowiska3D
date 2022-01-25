using g4;
using gk4._3DApi.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gk4.Shapes
{
    public struct float3
     {
        public float x, y, z;
     }

    public struct float4
    {
        public float x, y, z, g;
    }
    // punkt w przestrzeni 3d
    public struct Point3
    {
        public Camera Camera;

        public Point3(float x, float y, float z, float g,
                        int mx, int my, int mz,
                        float transform_x, float transform_y, float transform_z,
                        Camera c)
        {
            coordinates.x = x;
            coordinates.y = y;
            coordinates.z = z;
            coordinates.g = g;
            max_z = mz;
            max_y = my;
            max_x = mx;
            rad.x = 0;
            rad.y = 0;
            rad.z = 0;
            figure_center.x = transform_x;
            figure_center.y = transform_y;
            figure_center.z = transform_z;
            RotationCenter.x = figure_center.x;
            RotationCenter.y = figure_center.y;
            RotationCenter.z = figure_center.z;
            Camera = c;
        }

        // wymiary przestrzeni 3d
        private int max_z, max_y, max_x;
        

        // położenie w przestrzeni
        private float4 coordinates;

        // rotacja o dany kąt 
        private float3 rad;

        // środek figury 
        private float3 figure_center;


        // środek figury 
        private float3 RotationCenter;

        // czy dany punkt znajduje się w widzialnej przestrzeni
        public bool visable => !(x_parm_on_bitmap < 0 || y_parm_on_bitmap < 0);


        // w jakim miejsu na bitmapie musie pojawić się x by była iluzja 3d
        public int z_parm_on_bitmap
        {
            get
            {

                var vector = CoordinateMulipleByProjViewModel();
                vector = vector / vector[3, 0];
                return (int)((1 + vector[2, 0]) / 2);

            }
        }

        // w jakim miejsu na bitmapie musie pojawić się x by była iluzja 3d
        public int x_parm_on_bitmap
        {
            get
            {

                var vector = CoordinateMulipleByProjViewModel();
                vector = vector / vector[3, 0];
                return (int)(max_x * (1 + vector[0, 0]) / 2);

            }
        }

        // w jakim miejsu na bitmapie musie pojawić się y by była iluzja 3d
        public int y_parm_on_bitmap
        {
            get
            {
                var vector = CoordinateMulipleByProjViewModel();
                vector = vector / vector[3, 0];
                return (int)(max_y * (1 - vector[1, 0]) / 2);


            }
        }

        
        private matrix<float> CoordinateMulipleByProjViewModel()
        {
            var vector = this.vector;
            var M = make_rotations();
            vector = M * vector;
            vector[0, 0] += RotationCenter.x / 2;
            vector[1, 0] += RotationCenter.y / 2;
            vector[2, 0] += RotationCenter.z / 2;
            vector = Camera.View * vector;
            return Camera.View * vector;
        }


        private (float x, float y, float z) Coordinates
        {
            set
            {
                this.coordinates.x = value.x;
                this.coordinates.y = value.y;
                this.coordinates.z = value.z;
                figure_center.x += value.x - this.coordinates.x;
                figure_center.y += value.y - this.coordinates.y;
                figure_center.z += value.z - this.coordinates.z;
            }
            get
            {
                return (this.coordinates.x, this.coordinates.y, this.coordinates.z);
            }
        }

        public (float x, float y, float z) Rotation_Center
        {
            set
            {
                RotationCenter.x = value.x;
                RotationCenter.y = value.y;
                RotationCenter.z = value.z;
            }
            get
            {
                return (RotationCenter.x, RotationCenter.y, RotationCenter.z);
            }
        }

        

        public void ResetRotationCenter()
        {

            RotationCenter.x = figure_center.x;
            RotationCenter.y = figure_center.y;
            RotationCenter.z = figure_center.z;
        }

        public void Move(float x, float y, float z)
        {
            Coordinates = (x + coordinates.x, y + coordinates.y, z + coordinates.z);
        }
       

        

        public static bool operator == (Point3 a, Point3 b)
        {
            return a.coordinates.x == b.coordinates.x && a.coordinates.y == b.coordinates.y && a.coordinates.z == b.coordinates.z;
        }

        public static bool operator !=(Point3 a, Point3 b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }


        private matrix<float> vector 
        {
            get
            {
                matrix<float> tmp = new matrix<float>(4, 1);
                tmp.m[0, 0] = coordinates.x - RotationCenter.x;
                tmp.m[1, 0] = coordinates.y - RotationCenter.y;
                tmp.m[2, 0] = coordinates.z - RotationCenter.z;
                tmp.m[3, 0] = coordinates.g;
                return tmp;
            }
        }

        

        public matrix<float> make_rotations()
        {
            matrix<float> P = new matrix<double>(4, 4);
            P[0, 0] = (float)max_y / (float)max_x;
            P[1, 1] = 1;
            P[2, 3] = 1;
            P[3, 2] = -1;
            matrix<float> T = new matrix<float>(4, 4);
            T[0, 0] = 1;
            T[1, 1] = 1;
            T[2, 2] = 1;
            T[3, 3] = 1;
            T[2, 3] = 4;
            matrix<float> M = P * T;
            M.rotate_y(rad.x);
            M.rotate_x(rad.y);
            M.rotate_z(rad.z);
            return M;

        }

        // funkcje rotacji
        public void rotate_x(float rad)
        {
            this.rad.x = rad;
        }

        public void rotate_y(float rad)
        {
            this.rad.y = rad;
        }

        public void rotate_z(float rad)
        {
            this.rad.z = rad;
        }

        


    }
}
