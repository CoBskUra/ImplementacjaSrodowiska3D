﻿using g4;
using gk4._3DApi.Components;
using gk4.Matrix;
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
            RotationCenter = transform;
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

        // środek figury 
        private float3 FigureCenter;

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
            vector[0, 0] -= RotationCenter.x;
            vector[1, 0] -= RotationCenter.y;
            vector[2, 0] -= RotationCenter.z;

            vector /= vector[3, 0];

            visableCoordinates.x = vector[0, 0];
            visableCoordinates.y = vector[1, 0];
            visableCoordinates.z = vector[2, 0];


            //vector[3, 0] += RotationCenter.z;
            vector = Camera.View * vector;
            vector = Camera.Proj * vector;
            return vector;
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


        private matrix<float> vector 
        {
            get
            {
                matrix<float> tmp = new matrix<float>(4, 1);
                tmp.m[0, 0] = coordinates.x + RotationCenter.x;
                tmp.m[1, 0] = coordinates.y + RotationCenter.y;
                tmp.m[2, 0] = coordinates.z + RotationCenter.z;
                tmp.m[3, 0] = coordinates.g;
                return tmp;
            }
        }




        public matrix<float> make_rotations()
        {
            matrix<float> P = new matrix<float>(4, 4);
            P[0, 0] = 1;
            P[1, 1] = 1;
            P[2, 2] = 1;
            P[3, 3] = 1;
            matrix<float> T = new matrix<float>(4, 4);
            T[0, 0] = 1;
            T[1, 1] = 1;
            T[2, 2] = 1;
            T[3, 3] = 1;
            T[0, 3] = 1;
            T[1, 3] = 1;
            T[2, 3] = 1;
            matrix<float> M = P * T;
            M.rotate_y(Rads.y);
            M.rotate_x(Rads.x);
            M.rotate_z(Rads.z);
            return M;

        }


        


    }
}
