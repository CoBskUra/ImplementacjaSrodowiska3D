using gk4.Matrix;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gk4._3DApi.Components.Objects
{
    public class Light:Figure
    {
        public float Ac;
        public float Al;
        public float Aq;
        public float4 ambient;
        public float4 diffuse;
        public float4 specular;

        public void SetVarbles(float Ac, float Al, float Aq, float4 ambient, float4 diffuse, float4 specular)
        {
            this.Ac = Ac;
            this.Al = Al;
            this.Aq = Aq;
            this.ambient = ambient;
            this.diffuse = diffuse;
            this.specular = specular;
        }

    }
}
