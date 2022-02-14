using gk4._3DApi.Components.Objects.Components;
using gk4.Matrix;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace gk4._3DApi.Drarwing_on_bitmap
{
    public static class Figures3D
    {
        public static int Squer(this Bitmaps_intermediary Bitmap_API, float A)
        {
            A = A / 2;
                float3 normalVactor = new float3( -1, 0, 0);
                Bitmap_API.Add_rectangle(
                    Bitmap_API.create_point(-A , -A , A , 
                                            normalVactor, (0,-1, 0), (0,0, 1)),
                    Bitmap_API.create_point(-A , A , A ,
                                            normalVactor, (0, 1, 0), (0, 0, 1)),
                    Bitmap_API.create_point(-A , A , -A ,
                                            normalVactor, (0,  1, 0), (0, 0, -1)),
                    Bitmap_API.create_point(-A, -A, -A,
                                            normalVactor, (0, -1, 0), (0, 0, -1))
                    );

            normalVactor = new float3(1, 0, 0);
            Bitmap_API.Add_rectangle(
                Bitmap_API.create_point(A, -A, -A,
                                        normalVactor, (0, -1, 0), (0, 0, -1)),
                Bitmap_API.create_point(A, A, -A,
                                        normalVactor, (0, 1, 0), (0, 0, -1)),
                Bitmap_API.create_point(A, A, A,
                                        normalVactor, (0, 1, 0), (0, 0, 1)),
                Bitmap_API.create_point(A, -A, A,
                                        normalVactor, (0, -1, 0), (0, 0, 1))
                );

            normalVactor = new float3(0, 1, 0);
                Bitmap_API.Add_rectangle(
                    Bitmap_API.create_point(-A, A, A,
                                            normalVactor, (0, 0, 1), (-1, 0, 0)),
                    Bitmap_API.create_point(A, A, A,
                                            normalVactor, (0, 0, 1), (1, 0, 0)),
                    Bitmap_API.create_point(A, A, -A,
                                            normalVactor, (0, 0, -1), (1, 0, 0)),
                    Bitmap_API.create_point(-A, A, -A,
                                            normalVactor, (0, 0, -1), (-1, 0, 0))
                    );

            normalVactor = new float3(0, -1, 0);
            Bitmap_API.Add_rectangle(
                Bitmap_API.create_point(-A, -A, -A,
                                        normalVactor, (0, 0, -1), (-1, 0, 0)),
                Bitmap_API.create_point(A, -A, -A,
                                        normalVactor, (0, 0, -1), (1, 0, 0)),
                Bitmap_API.create_point(A, -A, A,
                                        normalVactor, (0, 0, 1), (1, 0, 0)),
                Bitmap_API.create_point(-A, -A, A,
                                        normalVactor, (0, 0, 1), (-1, 0, 0))
                );

            normalVactor = new float3(0, 0, -1);
                Bitmap_API.Add_rectangle(
                    Bitmap_API.create_point(-A, -A, -A,
                                            normalVactor, (-1, 0, 0), (0, -1 , 0)),
                    Bitmap_API.create_point(-A, A, -A ,
                                            normalVactor, (-1, 0, 0), (0, 1, 0)),
                    Bitmap_API.create_point(A, A, -A,
                                            normalVactor, (1, 0, 0), (0, 1, 0)),
                    Bitmap_API.create_point(A, -A , -A,
                                            normalVactor, (1, 0, 0), (0, -1, 0))
                   );

            normalVactor = new float3(0, 0, 1);
            Bitmap_API.Add_rectangle(
                Bitmap_API.create_point(A, -A, A,
                                        normalVactor, (1, 0, 0), (0, -1, 0)),
                Bitmap_API.create_point(A, A, A,
                                        normalVactor, (1, 0, 0), (0, 1, 0)),
                Bitmap_API.create_point(-A, A, A,
                                        normalVactor, (-1, 0, 0), (0, 1, 0)),
                Bitmap_API.create_point(-A, -A, A,
                                        normalVactor, (-1, 0, 0), (0, -1, 0))
               );

            return Bitmap_API.ObjectID;
        }

        

        public static int Sphere(this Bitmaps_intermediary Bitmap_Api, float R, int stackCount, int sectorCount)
        {
            float stackStep = MathF.PI / stackCount;
            float sectorStep = 2*MathF.PI / sectorCount;
            List<Point3> vertixes = new List<Point3>();

            // tworze punkty które tworzą sfere
            for (int i = 1; i <= stackCount-1; i++)
            {

                for (int j = 0; j < sectorCount; j++)
                {
                    
                    float Psi = stackStep * i, Phi = sectorStep * j;
                    Point3 point1 = MakePointOnSphere(Psi, Phi, R, Bitmap_Api);

                    vertixes.Add(point1);
                }

            }


            // łącze wierzchołek na górze i na dole z wierzchołkami poniżej/ powyrzej 
            Point3 North = MakePointOnSphere(0, 0, R, Bitmap_Api);
            Point3 Southe = MakePointOnSphere(MathF.PI, 2 * MathF.PI, R, Bitmap_Api);
            for (int j = 0; j < sectorCount; j++)
            {
                int next;
                if (j == sectorCount - 1)
                    next = 0;
                else
                    next = j + 1;

                Bitmap_Api.Add_Trialagle(
                        North,
                        vertixes[next],
                        vertixes[j]
                        );
                Bitmap_Api.Add_Trialagle(
                    vertixes[vertixes.Count - sectorCount + j],
                    vertixes[vertixes.Count - sectorCount + next],
                    Southe
                    );
            }

            // łącze pozostały punkty
            for (int i = 0; i < stackCount - 2; i++)
            {

                for (int j = 0; j < sectorCount; j++)
                {
                    int next;
                    if (j == sectorCount - 1)
                        next = 0;
                    else
                        next = j + 1;

                    Bitmap_Api.Add_Trialagle(
                        vertixes[i * sectorCount + j],
                        vertixes[i * sectorCount + next],
                        vertixes[(i + 1) * sectorCount + next]
                        );
                    Bitmap_Api.Add_Trialagle(
                        vertixes[i * sectorCount + j],
                        vertixes[(i + 1) * sectorCount + next],
                        vertixes[(i + 1) * sectorCount + j]
                        );
                }

            }

            

            return Bitmap_Api.ObjectID;
        }

        private static Point3 MakePointOnSphere(float Psi, float Phi, float R, Bitmaps_intermediary Bitmap_Api)
        {

            float3 location = new float3(R * MathF.Sin(Psi) * MathF.Cos(Phi),
                                R * MathF.Cos(Psi),
                                R * MathF.Sin(Psi) * MathF.Sin(Phi));
            float3 tangentialVector = new float3(MathF.Cos(Psi) * MathF.Cos(Phi),
                                                    -MathF.Sin(Psi),
                                                    MathF.Cos(Psi) * MathF.Sin(Phi));
            float3 binormal = new float3(-MathF.Sin(Psi) * MathF.Sin(Phi),
                                            0,
                                            MathF.Sin(Psi) * MathF.Cos(Phi));
            return Bitmap_Api.create_point(location, MatrixTransformationNeededTo3DModeling.cross_product(tangentialVector, binormal), tangentialVector, binormal);
        }
    }
}
