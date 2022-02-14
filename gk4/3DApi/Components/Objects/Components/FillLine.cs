using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gk4._3DApi.Components.Objects.Components
{
    public class FillLine
    {
        public ShadingOption shading = ShadingOption.None;
        public Trialagle trialagleToFill;
        public List<Light> lights;

        public void fillLine(int x1, int x2, int y, ref Bitmap whitheBoard, Color c)
        {
            if (shading == ShadingOption.None)
                fillLineNone(x1, x2, y, ref whitheBoard, c);
            else if (shading == ShadingOption.Constant)
                fillLineConstant(x1, x2, y, ref whitheBoard, c);
            else if (shading == ShadingOption.Gouraud)
                fillLineGouraud(x1, x2, y, ref whitheBoard, c);
            else if(shading == ShadingOption.Phonge)
                fillLinePhonge(x1, x2, y, ref whitheBoard, c);
        }

        public void fillLineNone(int x1, int x2, int y, ref Bitmap whitheBoard, Color c)
        {
            if (y < 0 || y >= whitheBoard.Height)
                return;
            if (x1 > x2)
            {
                var tmp = x1;
                x1 = x2;
                x2 = tmp;
            }

            if (x2 >= whitheBoard.Width)
                x2 = whitheBoard.Width - 1;
            for (int i = x1 > 0 ? x1 : 0; i <= x2; i++)
            {
                whitheBoard.SetPixel(i, y, c);
            }
        }

        private void fillLineConstant(int x1, int x2, int y, ref Bitmap whitheBoard, Color c)
        {
            
        }

        private void fillLineGouraud(int x1, int x2, int y, ref Bitmap whitheBoard, Color c)
        {
           
        }

        private void fillLinePhonge(int x1, int x2, int y, ref Bitmap whitheBoard, Color c)
        {

        }



    }
}
