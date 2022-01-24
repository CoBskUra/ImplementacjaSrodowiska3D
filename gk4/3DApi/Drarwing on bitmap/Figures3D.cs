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
            for (int i = 0; i < 2; i++)
            {

                Bitmap_API.Add_rectangle(
                    Bitmap_API.create_point(-A * MathF.Pow((-1), i), -A * MathF.Pow((-1), i), A * MathF.Pow((-1), i)),
                    Bitmap_API.create_point(-A * MathF.Pow((-1), i), A * MathF.Pow((-1), i), A * MathF.Pow((-1), i)),
                    Bitmap_API.create_point(-A * MathF.Pow((-1), i), A * MathF.Pow((-1), i), -A * MathF.Pow((-1), i)),
                    Bitmap_API.create_point(-A * MathF.Pow((-1), i), -A * MathF.Pow((-1), i), -A * MathF.Pow((-1), i))
                    );

                Bitmap_API.Add_rectangle(
                    Bitmap_API.create_point(-A * MathF.Pow((-1), i), A * MathF.Pow((-1), i), A * MathF.Pow((-1), i)),
                    Bitmap_API.create_point(A * MathF.Pow((-1), i), A * MathF.Pow((-1), i), A * MathF.Pow((-1), i)),
                    Bitmap_API.create_point(A * MathF.Pow((-1), i), A * MathF.Pow((-1), i), -A * MathF.Pow((-1), i)),
                    Bitmap_API.create_point(-A * MathF.Pow((-1), i), A * MathF.Pow((-1), i), -A * MathF.Pow((-1), i))
                    );

                Bitmap_API.Add_rectangle(
                    Bitmap_API.create_point(A * MathF.Pow((-1), i), -A * MathF.Pow((-1), i), -A * MathF.Pow((-1), i)),
                    Bitmap_API.create_point(A * MathF.Pow((-1), i), A * MathF.Pow((-1), i), -A * MathF.Pow((-1), i)),
                    Bitmap_API.create_point(-A * MathF.Pow((-1), i), A * MathF.Pow((-1), i), -A * MathF.Pow((-1), i)),
                    Bitmap_API.create_point(-A * MathF.Pow((-1), i), -A * MathF.Pow((-1), i), -A * MathF.Pow((-1), i))
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
                    float x1 = R * MathF.Sin(horizontal_density * j) * MathF.Cos(vertical_density * i );
                    float y1 = R * MathF.Cos(horizontal_density * j);
                    float z1 = R * MathF.Sin(horizontal_density * j) * MathF.Sin(vertical_density * i);

                    float x2 = R * MathF.Sin(horizontal_density * j) * MathF.Cos(vertical_density * (i + 1));
                    float y2 = R * MathF.Cos(horizontal_density * j);
                    float z2 = R * MathF.Sin(horizontal_density * j) * MathF.Sin(vertical_density * (i + 1));

                    float x3 = R * MathF.Sin(horizontal_density * (j - 1)) * MathF.Cos(vertical_density * (i + 1));
                    float y3 = R * MathF.Cos(horizontal_density * (j - 1));
                    float z3 = R * MathF.Sin(horizontal_density * (j - 1)) * MathF.Sin(vertical_density * (i + 1));



                    Bitmap_Api.Add_Trialagle(
                        Bitmap_Api.create_point(x1, y1, z1),
                        Bitmap_Api.create_point(x2, y2, z2),
                        Bitmap_Api.create_point(x3, y3, z3)
                        );

                }

            }

            return Bitmap_Api.FiguresNumber - 1;
        }
    }
}
