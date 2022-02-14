
using gk4.Matrix;
using System;
using System.Diagnostics;

namespace gk4._3DApi.Components.Objects.Components
{
    
    // punkt w przestrzeni 3d
    public struct Point3
    {
        public Camera Camera;

        public Point3(float4 coordinates,
                        float3 normalvector, float3 tangentialVector, float3 binormal,
                        int mx, int my, int mz,
                        float3 transform,
                        Camera c)
        {
            this.coordinates = coordinates;
            this.normal_vector = normalvector;
            this.tangentialVector = tangentialVector;
            this.binormal = binormal;
            max_z = mz;
            max_y = my;
            max_x = mx;
            Rads = new float3();
            FigureCenter = transform;
            CurenntFigureCenter = FigureCenter;
            RotationCenter = FigureCenter;
            visableCoordinates.x = coordinates.x;
            visableCoordinates.y = coordinates.y;
            visableCoordinates.z = coordinates.z;
            Camera = c;
        }

        // wymiary przestrzeni 3d
        private int max_z, max_y, max_x;
        

        // położenie w przestrzeni
        private float4 coordinates;

        public float3 visableCoordinates;

        // rotacja o dany kąt 
        public float3 Rads;

        // defaultowy środek figury 
        private float3 FigureCenter;

        // obecny środek
        public float3 CurenntFigureCenter;

        // wektor normalny
        private float3 normal_vector, tangentialVector, binormal;


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
                return (int)((1 + vector[2, 0]) / 2);

            }
        }

        // w jakim miejsu na bitmapie musie pojawić się x by była iluzja 3d
        public int x_parm_on_bitmap
        {
            get
            {

                var vector = CoordinateMulipleByProjViewModel();
                return (int)(max_x * (1 + vector[0, 0]) / 2);

            }
        }

        // w jakim miejsu na bitmapie musie pojawić się y by była iluzja 3d
        public int y_parm_on_bitmap
        {
            get
            {
                var vector = CoordinateMulipleByProjViewModel();
                return (int)(max_y * (1 - vector[1, 0]) / 2);


            }
        }

        
        private Matrix<float> CoordinateMulipleByProjViewModel()
        {
            var vector = this.RotatedCordinates;

            vector = Camera.View * vector;
            vector = Camera.Proj * vector;
            vector /= vector[3, 0];

            visableCoordinates.x = vector[0, 0];
            visableCoordinates.y = vector[1, 0];
            visableCoordinates.z = vector[2, 0];

            return vector;
        }

        public float3 NormalVector
        {
            get
            {
                Matrix<float> tmp = new Matrix<float>(4, 1);

                tmp.m[0, 0] = normal_vector.x + RotationCenter.x;
                tmp.m[1, 0] = normal_vector.y + RotationCenter.y;
                tmp.m[2, 0] = normal_vector.z + RotationCenter.z;
                tmp.m[3, 0] = 0;

                var M = RotationMatrix();
                tmp = M * tmp;
                tmp[0, 0] -= RotationCenter.x;
                tmp[1, 0] -= RotationCenter.y;
                tmp[2, 0] -= RotationCenter.z;

                
                //Debug.WriteLine(tmp.ToString());

                return tmp;
            }
            set
            {
                normal_vector = value;
            }
        }


        public float3 Coordinates
        {
            private set
            {
                this.coordinates.x = value.x;
                this.coordinates.y = value.y;
                this.coordinates.z = value.z;
                FigureCenter.x += value.x - this.coordinates.x;
                FigureCenter.y += value.y - this.coordinates.y;
                FigureCenter.z += value.z - this.coordinates.z;
            }
            get
            {
                return new float3(this.coordinates.x, this.coordinates.y, this.coordinates.z);
            }
        }

        public float3 Rotation_Center
        {
            set
            {
                RotationCenter.x = value.x;
                RotationCenter.y = value.y;
                RotationCenter.z = value.z;
            }
            get
            {
                return new float3(RotationCenter.x, RotationCenter.y, RotationCenter.z);
            }
        }

        


        public void ResetRotationCenter()
        {
            RotationCenter.x = FigureCenter.x;
            RotationCenter.y = FigureCenter.y;
            RotationCenter.z = FigureCenter.z;
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


        private Matrix<float> RotatedCordinates 
        {
            get
            {
                Matrix<float> tmp = new Matrix<float>(4, 1);

                tmp.m[0, 0] = coordinates.x + RotationCenter.x;
                tmp.m[1, 0] = coordinates.y + RotationCenter.y;
                tmp.m[2, 0] = coordinates.z + RotationCenter.z;
                tmp.m[3, 0] = coordinates.g;

                var M = RotationMatrix();
                tmp = M * tmp;
                tmp[0, 0] -= RotationCenter.x;
                tmp[1, 0] -= RotationCenter.y;
                tmp[2, 0] -= RotationCenter.z;

                CurenntFigureCenter.x = tmp[0, 0] + FigureCenter.x - coordinates.x;
                CurenntFigureCenter.y = tmp[1, 0] + FigureCenter.y - coordinates.y;
                CurenntFigureCenter.z = tmp[2, 0] + FigureCenter.z - coordinates.z;


                return tmp;
            }
        }




        public Matrix<float> RotationMatrix()
        {
            Matrix<float> P = new Matrix<float>(4, 4);
            P[0, 0] = 1;
            P[1, 1] = 1;
            P[2, 2] = 1;
            P[3, 3] = 1;
            Matrix<float> T = new Matrix<float>(4, 4);
            T[0, 0] = 1;
            T[1, 1] = 1;
            T[2, 2] = 1;
            T[3, 3] = 1;
            T[0, 3] = 1;
            T[1, 3] = 1;
            T[2, 3] = 1;
            Matrix<float> M = P * T;
            M.rotate_x(Rads.x);
            M.rotate_y(Rads.y);
            M.rotate_z(Rads.z);
            return M;

        }

        public Matrix<float> reverseMatrixRotation()
        {
            Matrix<float> M = new Matrix<float> (4, 4);
            M.ReduceToDiagonal();
            M.reverse_rotate_z(Rads.x);
            M.reverse_rotate_y(Rads.y);
            M.reverse_rotate_x(Rads.z);
            Matrix<float> P = new Matrix<float>(4, 4);
            P[0, 0] = 1;
            P[1, 1] = 1;
            P[2, 2] = 1;
            P[3, 3] = 1;
            Matrix<float> T = new Matrix<float>(4, 4);
            T[0, 0] = 1;
            T[1, 1] = 1;
            T[2, 2] = 1;
            T[3, 3] = 1;
            T[0, 3] = -1;
            T[1, 3] = -1;
            T[2, 3] = -1;

            M = M * T * P;
            
            M.Transform();

            return M;

        }





    }
}
