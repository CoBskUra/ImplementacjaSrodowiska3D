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
    }

    public struct float4
    {
        public float x, y, z, g;
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
