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
        public float Ac = 0.1f;
        public float Al = 0.6f;
        public float Aq = 0.032f;
        public float3 ambient = (0, 0, 0);
        public float3 diffuse = (0, 0, 0);
        public float3 specular = (0, 0, 0);

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
