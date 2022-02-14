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
        public Color ambient;
        public Color diffuse;
        public Color specular;

        public void SetVarbles(float Ac, float Al, float Aq, Color ambient, Color diffuse, Color specular)
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
