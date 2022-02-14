using System;

namespace gk4.Matrix
{
    public static class MatrixTransformationNeededTo3DModeling
    {
        public static Matrix<float> cross_product(Matrix<float> a, Matrix<float> b)
        {
            if (a.GetLength(1) != 1 || a.GetLength(0) != 3 || b.GetLength(1) != 1 || b.GetLength(0) != 3)
                throw new Exception("macierz nie jest 3x1");
            else
            {
                a.Normalization_3x1();
                b.Normalization_3x1();
                Matrix<float> result = new Matrix<float>(3, 1);
                result[0, 0] = a[1, 0] * b[2, 0] - a[2, 0] * b[1, 0];
                result[1, 0] = a[2, 0] * b[0, 0] - a[0, 0] * b[2, 0];
                result[2, 0] = a[0, 0] * b[1, 0] - a[1, 0] * b[0, 0];
                result.Normalization_3x1();
                return result;
            }
        }

        public static void Normalization_3x1(this Matrix<float> m)
        {
            if (m.GetLength(1) != 1 || m.GetLength(0) != 3)
                throw new Exception("macierz nie jest 3x1");
            else
            {
                float norma = MathF.Sqrt(
                    MathF.Pow(m[0, 0], 2) +
                    MathF.Pow(m[1, 0], 2) +
                    MathF.Pow(m[2, 0], 2));

                m[0, 0] /= norma;
                m[1, 0] /= norma;
                m[2, 0] /= norma;
            }
        }

        public static void Normalization_4x1(this Matrix<float> m)
        {
            if (m.GetLength(1) != 1 || m.GetLength(0) != 4)
                throw new Exception("macierz nie jest 4x1");
            else
            {
                float norma = MathF.Sqrt(
                    MathF.Pow(m[0, 0], 2) +
                    MathF.Pow(m[1, 0], 2) +
                    MathF.Pow(m[2, 0], 2) +
                    MathF.Pow(m[3, 0], 2));

                m[0, 0] /= norma;
                m[1, 0] /= norma;
                m[2, 0] /= norma;
                m[3, 0] /= norma;
            }
        }


        // obracają macierz wzglądem danej osi
        public static void rotate_x(this Matrix<float> M, float rad)
        {
            if (M.GetLength(0) != 4 || M.GetLength(1) > 4)
                throw new Exception("macierz nie jest 4x4");
            else
            {
                Matrix<float> rotete = new Matrix<float>(4, 4);
                rotete[0, 0] = 1;

                rotete[1, 1] = MathF.Cos(rad);
                rotete[1, 2] = -MathF.Sin(rad);

                rotete[2, 1] = MathF.Sin(rad);
                rotete[2, 2] = MathF.Cos(rad);

                rotete[3, 3] = 1;

                Matrix<float> r = (dynamic)(M * rotete);
                M.m = r.m;
            }
        }

        public static void rotate_z(this Matrix<float> M, float rad)
        {
            if (M.GetLength(0) != 4 || M.GetLength(1) > 4)
                throw new Exception("macierz nie jest 4x4");
            else
            {
                Matrix<float> rotete = new Matrix<float>(4, 4);
                rotete[0, 0] = MathF.Cos(rad);
                rotete[0, 1] = -MathF.Sin(rad);


                rotete[1, 0] = MathF.Sin(rad);
                rotete[1, 1] = MathF.Cos(rad);

                rotete[2, 2] = 1;

                rotete[3, 3] = 1;

                Matrix<float> r = (dynamic)(M * rotete);
                M.m = r.m;
            }
        }



        public static void rotate_y(this Matrix<float> M, float rad)
        {
            if (M.GetLength(0) != 4 || M.GetLength(1) > 4)
                throw new Exception("macierz nie jest 4x4");
            else
            {
                Matrix<float> rotete = new Matrix<float>(4, 4);
                rotete[0, 0] = MathF.Cos(rad);
                rotete[0, 2] = -MathF.Sin(rad);

                rotete[1, 1] = 1;

                rotete[2, 0] = MathF.Sin(rad);
                rotete[2, 2] = MathF.Cos(rad);

                rotete[3, 3] = 1;

                Matrix<float> r = (M * rotete);
                M.m = r.m;
            }
        }

        public static void rotate(this Matrix<float> M, float rad)
        {
            M.rotate_x(rad);
            M.rotate_y(rad);
            M.rotate_z(rad);
        }


        // obracają macierz wzglądem danej osi
        public static void reverse_rotate_x(this Matrix<float> M, float rad)
        {
            if (M.GetLength(0) != 4 || M.GetLength(1) > 4)
                throw new Exception("macierz nie jest 4x4");
            else
            {
                Matrix<float> rotete = new Matrix<float>(4, 4);
                rotete[0, 0] = 1;

                rotete[1, 1] = MathF.Cos(rad);
                rotete[1, 2] = MathF.Sin(rad);

                rotete[2, 1] = -MathF.Sin(rad);
                rotete[2, 2] = MathF.Cos(rad);

                rotete[3, 3] = 1;

                Matrix<float> r = (dynamic)(M * rotete);
                M.m = r.m;
            }
        }

        public static void reverse_rotate_z(this Matrix<float> M, float rad)
        {
            if (M.GetLength(0) != 4 || M.GetLength(1) > 4)
                throw new Exception("macierz nie jest 4x4");
            else
            {
                Matrix<float> rotete = new Matrix<float>(4, 4);
                rotete[0, 0] = MathF.Cos(rad);
                rotete[0, 1] = MathF.Sin(rad);


                rotete[1, 0] = -MathF.Sin(rad);
                rotete[1, 1] = MathF.Cos(rad);

                rotete[2, 2] = 1;

                rotete[3, 3] = 1;

                Matrix<float> r = (dynamic)(M * rotete);
                M.m = r.m;
            }
        }



        public static void reverse_rotate_y(this Matrix<float> M, float rad)
        {
            if (M.GetLength(0) != 4 || M.GetLength(1) > 4)
                throw new Exception("macierz nie jest 4x4");
            else
            {
                Matrix<float> rotete = new Matrix<float>(4, 4);
                rotete[0, 0] = MathF.Cos(rad);
                rotete[0, 2] = MathF.Sin(rad);

                rotete[1, 1] = 1;

                rotete[2, 0] = -MathF.Sin(rad);
                rotete[2, 2] = MathF.Cos(rad);

                rotete[3, 3] = 1;

                Matrix<float> r = (M * rotete);
                M.m = r.m;
            }
        }

    }
}
