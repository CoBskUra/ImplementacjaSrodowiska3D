using g4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gk4
{
    public static class MatrixEquations
    {
        public static matrix<float> cross_product(matrix<float> a, matrix<float> b)
        {
            if (a.GetLength(1) != 1 || a.GetLength(0) != 3 || b.GetLength(1) != 1 || b.GetLength(0) != 3)
                throw new Exception("macierz nie jest 3x1");
            else
            {
                a.Normalization_3x1();
                b.Normalization_3x1();
                matrix<float> result = new matrix<float>(3, 1);
                result[0, 0] = a[1, 0] * b[2, 0] - a[2, 0] * b[1, 0];
                result[1, 0] = a[2, 0] * b[0, 0] - a[0, 0] * b[2, 0];
                result[2, 0] = a[0, 0] * b[1, 0] - a[1, 0] * b[0, 0];
                result.Normalization_3x1();
                return result;
            }
        }
    }
}
