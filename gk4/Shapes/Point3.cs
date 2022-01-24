using g4;
using gk4._3DApi.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gk4.Shapes
{
    // punkt w przestrzeni 3d
    public struct Point3
    {
        public Camera Camera;

        public Point3(float x, float y, float z, float g,
                        int mx, int my, int mz,
                        float transform_x, float transform_y, float transform_z,
                        Camera c)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.g = g;
            max_z = mz;
            max_y = my;
            max_x = mx;
            rad_y = 0;
            rad_x = 0;
            rad_z = 0;
            center_x = transform_x;
            center_y = transform_y;
            center_z = transform_z;
            rotationCenter_x = center_x;
            rotationCenter_y = center_y;
            rotationCenter_z = center_z;
            Camera = c;
        }

        // wymiary przestrzeni 3d
        private int max_z, max_y, max_x;

        // położenie w przestrzeni
        private float x, y, z, g;

        // rotacja o dany kąt 
        float rad_y, rad_x, rad_z;

        // środek figury 
        private float center_x, center_y, center_z;
        

        // środek figury 
        private float rotationCenter_x, rotationCenter_y, rotationCenter_z;

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
            vector[0, 0] += rotationCenter_x / 2;
            vector[1, 0] += rotationCenter_y / 2;
            vector[2, 0] += rotationCenter_z / 2;
            vector = Camera.View * vector;
            return Camera.View * vector;
        }


        private (float x, float y, float z) Coordinates
        {
            set
            {
                this.x = value.x;
                this.y = value.y;
                this.z = value.z;
                center_x += value.x - this.x;
                center_y += value.y - this.y;
                center_z += value.z - this.z;
            }
            get
            {
                return (this.x, this.y, this.z);
            }
        }

        public (float x, float y, float z) Rotation_Center
        {
            set
            {
                rotationCenter_x = value.x;
                rotationCenter_y = value.y;
                rotationCenter_z = value.z;
            }
            get
            {
                return (rotationCenter_x, rotationCenter_y, rotationCenter_z);
            }
        }

        

        public void ResetRotationCenter()
        {

            rotationCenter_x = center_x;
            rotationCenter_y = center_y;
            rotationCenter_z = center_z;
        }

        public void Move(float x, float y, float z)
        {
            Coordinates = (x + this.x, y + this.y, z + this.z);
        }
       

        

        public static bool operator == (Point3 a, Point3 b)
        {
            return a.x == b.x && a.y == b.y && a.z == b.z;
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
                tmp.m[0, 0] = x - rotationCenter_x;
                tmp.m[1, 0] = y - rotationCenter_y;
                tmp.m[2, 0] = z - rotationCenter_z;
                tmp.m[3, 0] = g;
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
            M.rotate_y(rad_y);
            M.rotate_x(rad_x);
            M.rotate_z(rad_z);
            return M;

        }

        // funkcje rotacji
        public void rotate_x(float rad)
        {
            this.rad_x = rad;
        }


        public void rotate_z(float rad)
        {
            this.rad_z = rad;
        }

        public void rotate_y(float rad)
        {
            this.rad_y = rad;
        }


    }
}
