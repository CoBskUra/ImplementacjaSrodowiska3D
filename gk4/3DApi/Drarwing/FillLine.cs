using gk4._3DApi.Components;
using gk4._3DApi.Components.Objects;
using gk4._3DApi.Components.Objects.Components;
using gk4.Matrix;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace gk4._3DApi.Drarwing
{
    public class FillLine
    {
        public ShadingOption shading = ShadingOption.None;
        public Trialagle trialagleToFill;
        public List<Light> lights;
        public Material Material;
        public CameraPointer cameraPointer;

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
            var light = lights[0];
            float4 ambient = Material.ambient * light.ambient;
            

            float4 fromPixelToLighte =(float4)( light.FigureCenter - trialagleToFill.TrialagleCenterInWorld);
            float dist = MathF.Sqrt(MathF.Pow(fromPixelToLighte.x, 2) + MathF.Pow(fromPixelToLighte.y, 2) + MathF.Pow(fromPixelToLighte.z,2));
            Matrix<float> tmp = fromPixelToLighte;
            tmp.Normalization_4x1();
            fromPixelToLighte = tmp;

            float4 VersorNormalny = (float4)(trialagleToFill.normalVector);
            float4 diffuse = Material.diffuse *floats.Cos(fromPixelToLighte, VersorNormalny)* light.diffuse;

            float4 fromPixelToViver = (float4)(cameraPointer.Camera.Position - trialagleToFill.TrialagleCenterInWorld);
            float4 R = VersorNormalny * 2 * floats.Cos(fromPixelToLighte, VersorNormalny) - fromPixelToLighte;
            float4 specular = Material.specular *  light.specular * MathF.Pow(floats.Cos(R, fromPixelToViver), Material.shininess);


            float If = 1/(light.Ac + light.Ac*dist + light.Aq * MathF.Pow(dist, 2));
            float4 newColor = diffuse + (ambient + specular)*If;
            newColor *= 255;

            newColor.g = newColor.g > 255 ? 255 : newColor.g;
            newColor.x = newColor.x > 255 ? 255 : newColor.x;
            newColor.y = newColor.y > 255 ? 255 : newColor.y;
            newColor.z = newColor.z > 255 ? 255 : newColor.z;

            newColor.g = newColor.g < 0 ? 0 : newColor.g;
            newColor.x = newColor.x < 0 ? 0 : newColor.x;
            newColor.y = newColor.y < 0 ? 0 : newColor.y;
            newColor.z = newColor.z < 0 ? 0 : newColor.z;

            Color nc = Color.FromArgb((int)newColor.x, (int)newColor.y, (int)newColor.z, (int)newColor.g);

            fillLineNone(x1, x2, y, ref whitheBoard, nc);
        }

        private void fillLineGouraud(int x1, int x2, int y, ref Bitmap whitheBoard, Color c)
        {
           
        }

        private void fillLinePhonge(int x1, int x2, int y, ref Bitmap whitheBoard, Color c)
        {

        }



    }
}
