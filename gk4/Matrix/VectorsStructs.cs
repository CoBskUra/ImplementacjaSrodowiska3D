using g4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gk4.Matrix
{
    public struct float3
    {
        public float x, y, z;

        public float3(float a, float b, float c)
        {
            x = a; y = b; z = c;
        }

        public override string ToString()
        {
            return $"{x} {y} {z}";
        }

        public static implicit operator float3((float a, float b, float c) krotka)
        {
            return new float3(krotka.a, krotka.b, krotka.c);
        }

        public static bool operator ==(float3 a, float3 b)
        {
            return a.x == b.x && a.y == b.y && a.z == b.z;
        }

        public static bool operator !=(float3 a, float3 b)
        {
            return !(a==b);
        }

        public static implicit operator (float a, float b, float c)(float3 f3)
        {
            return (f3.x, f3.y, f3.z);
        }


        public static implicit operator matrix<float>(float3 f3)
        {
            matrix<float> result = new matrix<float>(3, 1);
            result[0, 0] = f3.x;
            result[1, 0] = f3.y;
            result[2, 0] = f3.z;
            return result;
        }

        public static implicit operator float3(matrix<float> f3)
        {
            if (f3.GetLength(0) < 3 || f3.GetLength(1) != 1)
                throw new Exception("Matrix has incorrect dimensions");
            return new float3(f3[0, 0], f3[1, 0], f3[2, 0]);
        }

    }

    public struct float4
    {
        public float x, y, z, g;

        public float4(float a, float b, float c, float d)
        {
            x = a; y = b; z = c; g = d;
        }

        public static implicit operator float4((float a, float b, float c, float d) krotka)
        {
            return new float4(krotka.a, krotka.b, krotka.c, krotka.d);
        }

        public static implicit operator (float a, float b, float c, float d)(float4 f4)
        {
            return (f4.x, f4.y, f4.z, f4.g);
        }

        public static implicit operator matrix<float>(float4 f4)
        {
            matrix<float> result = new matrix<float>(4, 1);
            result[0, 0] = f4.x;
            result[1, 0] = f4.y;
            result[2, 0] = f4.z;
            result[3, 0] = f4.g;
            return result;
        }

        public static implicit operator float4(matrix<float> f4)
        {
            if (f4.GetLength(0) != 4 || f4.GetLength(1) != 1)
                throw new Exception("Matrix has incorrect dimensions");
            return new float4(f4[0, 0], f4[1, 0], f4[2, 0], f4[3,0]);
        }

    }
    
    public static class krotki
    {
        public static matrix<float> Diffrence((float x, float y, float z) a, (float x, float y, float z) b)
        {
            matrix<float> result = new matrix<float>(3, 1);
            result[0, 0] = a.x - b.x;
            result[1, 0] = a.y - b.y;
            result[2, 0] = a.z - b.z;
            return result;
        }
    }

}
