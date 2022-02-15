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
        public float3 ambient;
        public float3 diffuse;
        public float3 specular;

        public void SetVarbles(float Ac, float Al, float Aq, float3 ambient, float3 diffuse, float3 specular)
        {
            this.Ac = Ac;
            this.Al = Al;
            this.Aq = Aq;
            this.ambient = ambient;
            this.diffuse = diffuse;
            this.specular = specular;
        }

        public virtual bool isEfectingPixel()
        {
            return true;
        }

    }
}
