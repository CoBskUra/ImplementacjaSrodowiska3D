using gk4.Matrix;
using gk4.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gk4._3DApi.Drarwing_on_bitmap
{
    public static class Figures3D
    {
        public static int Squer(this Bitmaps_intermediary Bitmap_API, float A)
        {
            A = A / 2;
            for (int i = 0; i < 2; i++)
            {
                float3 normalVactor = new float3(MathF.Pow(-1, i) * -1, 0, 0);
                Bitmap_API.Add_rectangle(
                    Bitmap_API.create_point(-A * MathF.Pow(-1, i), -A * MathF.Pow(-1, i), A * MathF.Pow(-1, i), 
                                            normalVactor, (0,-1* MathF.Pow(-1, i), 0), (0,0, MathF.Pow(-1, i))),
                    Bitmap_API.create_point(-A * MathF.Pow(-1, i), A * MathF.Pow(-1, i), A * MathF.Pow(-1, i),
                                            normalVactor, (0, MathF.Pow(-1, i), 0), (0, 0, MathF.Pow(-1, i))),
                    Bitmap_API.create_point(-A * MathF.Pow(-1, i), A * MathF.Pow(-1, i), -A * MathF.Pow(-1, i),
                                            normalVactor, (0,  MathF.Pow(-1, i), 0), (0, 0, -1*MathF.Pow(-1, i))),
                    Bitmap_API.create_point(-A * MathF.Pow(-1, i), -A * MathF.Pow(-1, i), -A * MathF.Pow(-1, i),
                                            normalVactor, (0, -1 * MathF.Pow(-1, i), 0), (0, 0, -1*MathF.Pow(-1, i)))
                    );

                
                Bitmap_API.Add_rectangle(
                    Bitmap_API.create_point(-A * MathF.Pow(-1, i), A * MathF.Pow(-1, i), A * MathF.Pow(-1, i),
                                            normalVactor, (0, 0, MathF.Pow(-1, i)), (-1*MathF.Pow(-1, i), 0, 0)),
                    Bitmap_API.create_point(A * MathF.Pow(-1, i), A * MathF.Pow(-1, i), A * MathF.Pow(-1, i),
                                            normalVactor, (0, 0, MathF.Pow(-1, i)), (MathF.Pow(-1, i), 0, 0)),
                    Bitmap_API.create_point(A * MathF.Pow(-1, i), A * MathF.Pow(-1, i), -A * MathF.Pow(-1, i),
                                            normalVactor, (0, 0, -1*MathF.Pow(-1, i)), (MathF.Pow(-1, i), 0, 0)),
                    Bitmap_API.create_point(-A * MathF.Pow(-1, i), A * MathF.Pow(-1, i), -A * MathF.Pow(-1, i),
                                            normalVactor, (0, 0, -1*MathF.Pow(-1, i)), (-1 * MathF.Pow(-1, i), 0, 0))
                    );

                normalVactor = new float3(0, 0, MathF.Pow(-1, i) * -1);
                Bitmap_API.Add_rectangle(
                    Bitmap_API.create_point(A * MathF.Pow(-1, i), -A * MathF.Pow(-1, i), -A * MathF.Pow(-1, i),
                                            normalVactor, (MathF.Pow(-1, i), 0, 0), (0, -1 * MathF.Pow(-1, i), 0)),
                    Bitmap_API.create_point(A * MathF.Pow(-1, i), A * MathF.Pow(-1, i), -A * MathF.Pow(-1, i),
                                            normalVactor, (MathF.Pow(-1, i), 0, 0), (0, MathF.Pow(-1, i), 0)),
                    Bitmap_API.create_point(-A * MathF.Pow(-1, i), A * MathF.Pow(-1, i), -A * MathF.Pow(-1, i),
                                            normalVactor, (-1*MathF.Pow(-1, i), 0, 0), (0, MathF.Pow(-1, i), 0)),
                    Bitmap_API.create_point(-A * MathF.Pow(-1, i), -A * MathF.Pow(-1, i), -A * MathF.Pow(-1, i),
                                            normalVactor, (-1*MathF.Pow(-1, i), 0, 0), (0, -1 * MathF.Pow(-1, i), 0))
                    );
            }
            return Bitmap_API.FiguresNumber - 1;
        }

        

        public static int Sphere(this Bitmaps_intermediary Bitmap_Api, float R, int number_of_trarangle_at_horyzontal, int  number_of_trarangle_at_vertical)
        {
            float vertical_density = MathF.PI / number_of_trarangle_at_vertical;
            float horizontal_density = 2*MathF.PI / number_of_trarangle_at_horyzontal;
            for (int i = 0; i < number_of_trarangle_at_vertical; i++)
            {

                for (int j = 0; j < number_of_trarangle_at_horyzontal; j++)
                {
                    float Psi = horizontal_density * j, Phi = vertical_density * i;
                    Point3 point1 = MakePointOnSphere(Psi, Phi, R, Bitmap_Api);

                    Psi = horizontal_density * j; Phi = vertical_density * (i + 1);
                    Point3 point2 = MakePointOnSphere(Psi, Phi, R, Bitmap_Api);

                    Psi = horizontal_density * (j - 1); Phi = vertical_density * (i + 1);
                    Point3 point3 = MakePointOnSphere(Psi, Phi, R, Bitmap_Api);


                    Bitmap_Api.Add_Trialagle(point1,point2,point3);

                }

            }

            return Bitmap_Api.FiguresNumber - 1;
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
