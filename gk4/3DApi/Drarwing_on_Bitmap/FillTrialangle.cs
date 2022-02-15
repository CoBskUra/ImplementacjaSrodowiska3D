using gk4._3DApi.Components;
using gk4._3DApi.Components.Objects;
using gk4._3DApi.Components.Objects.Components;
using gk4.Matrix;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace gk4._3DApi.Drarwing
{
    public class FillTrialangle
    {
        public ShadingOption shading = ShadingOption.None;
        public Trialagle trialagleToFill;
        public List<Light> lights;
        public Material Material;
        public CameraPointer cameraPointer;

        private void sort(ref (int x, int y, int z, Point3 p) theHighest, ref (int x, int y, int z, Point3 p) theLowest, ref (int x, int y, int z, Point3 p) medium)
        {
            if (theHighest.y < theLowest.y)
            {
                var tmp = theHighest;
                theHighest = theLowest;
                theLowest = tmp;
            }
            if (medium.y < theLowest.y)
            {
                var tmp = medium;
                medium = theLowest;
                theLowest = tmp;
            }
            else if (medium.y > theHighest.y)
            {
                var tmp = medium;
                medium = theHighest;
                theHighest = tmp;
            }
        }

        public void fill_me(ref Bitmap Whitheboard, Color fillColor, Color LineColor)
        {
            (int x, int y, int z, Point3 p) theHighest = (trialagleToFill.x1, trialagleToFill.y1, trialagleToFill.z1, trialagleToFill.a);
            (int x, int y, int z, Point3 p) theLowest = (trialagleToFill.x2, trialagleToFill.y2, trialagleToFill.z2, trialagleToFill.b);
            (int x, int y, int z, Point3 p) medium = (trialagleToFill.x3, trialagleToFill.y3, trialagleToFill.z2, trialagleToFill.c);

            sort(ref theHighest, ref theLowest, ref medium);

            int TheLowestTheHighests_Highte = theHighest.y - theLowest.y;
            int theLowestMedium_Highte = medium.y - theLowest.y;
            int MediumTheHighests_Highte = theHighest.y - medium.y;


            double theLowestMediumSkalar = ((double)medium.x - (double)theLowest.x) / (double)theLowestMedium_Highte;
            double theLowestTheHighestSkalar = ((double)theHighest.x - (double)theLowest.x) / (double)TheLowestTheHighests_Highte;
            double MediumTheHighestsSkalar = ((double)theHighest.x - (double)medium.x) / (double)MediumTheHighests_Highte;


            this.fill(ref Whitheboard, fillColor, LineColor, theLowest, medium, theHighest, TheLowestTheHighests_Highte, theLowestMedium_Highte,
                            MediumTheHighests_Highte, theLowestMediumSkalar, theLowestTheHighestSkalar, MediumTheHighestsSkalar);


            


        }


        private void fill(ref Bitmap whitheBoard, Color fillColor, Color LineColor, (int x, int y, int z, Point3 p) theLowest, (int x, int y, int z, Point3 p) medium, (int x, int y, int z, Point3 p) theHighest,
                                int TheLowestTheHighests_Highte, int theLowestMedium_Highte, int MediumTheHighests_Highte,
                                double theLowestMediumSkalar, double theLowestTheHighestSkalar, double MediumTheHighestsSkalar)
        {

            if (shading == ShadingOption.None)
                fillLineNone(ref  whitheBoard,  fillColor, LineColor, theLowest, medium,
                                 TheLowestTheHighests_Highte,  theLowestMedium_Highte,  MediumTheHighests_Highte,
                                 theLowestMediumSkalar,  theLowestTheHighestSkalar,  MediumTheHighestsSkalar);
            else if (shading == ShadingOption.Constant)
                fillLineConstant(ref whitheBoard, theLowest, medium,
                                 TheLowestTheHighests_Highte, theLowestMedium_Highte, MediumTheHighests_Highte,
                                 theLowestMediumSkalar, theLowestTheHighestSkalar, MediumTheHighestsSkalar);
            else if (shading == ShadingOption.Gouraud)
                fillLineGouraud(ref whitheBoard, fillColor, theLowest, medium,
                                 TheLowestTheHighests_Highte, theLowestMedium_Highte, MediumTheHighests_Highte,
                                 theLowestMediumSkalar, theLowestTheHighestSkalar, MediumTheHighestsSkalar);
            else if (shading == ShadingOption.Phonge)
                fillLinePhonge(ref whitheBoard, fillColor, theLowest, medium, theHighest,
                                 TheLowestTheHighests_Highte, theLowestMedium_Highte, MediumTheHighests_Highte,
                                 theLowestMediumSkalar, theLowestTheHighestSkalar, MediumTheHighestsSkalar);
            
            
        }


        private void fillLineNone(ref Bitmap whitheBoard, Color fillColor, Color LineColor, (int x, int y, int z, Point3 p) theLowest, (int x, int y, int z, Point3 p) medium,
                                int TheLowestTheHighests_Highte, int theLowestMedium_Highte, int MediumTheHighests_Highte,
                                double theLowestMediumSkalar, double theLowestTheHighestSkalar, double MediumTheHighestsSkalar)
        {
            if (TheLowestTheHighests_Highte != 0 && theLowestMedium_Highte != 0)
            {
                for (int i = 0; i <= theLowestMedium_Highte; i++)
                {
                    int x1 = (int)(theLowest.x + theLowestTheHighestSkalar * i);
                    int x2 = (int)(theLowest.x + theLowestMediumSkalar * i);
                    int y = i + theLowest.y;
                    
                    

                    if (y < 0 || y >= whitheBoard.Height)
                        continue;

                    if (x1 > x2)
                    {
                        var tmp = x1;
                        x1 = x2;
                        x2 = tmp;
                    }

                    if (x2 >= whitheBoard.Width)
                        x2 = whitheBoard.Width - 1;
                    for (int j = x1 > 0 ? x1 : 0; j <= x2; j++)
                    {
                        whitheBoard.SetPixel(j, y, fillColor);
                    }
                }
            }

            if (MediumTheHighests_Highte != 0 && TheLowestTheHighests_Highte != 0)
            {
                for (int i = theLowestMedium_Highte; i <= TheLowestTheHighests_Highte; i++)
                {
                    int x1 = (int)(theLowest.x + theLowestTheHighestSkalar * i);
                    int x2 = (int)(medium.x + MediumTheHighestsSkalar * (i - theLowestMedium_Highte));
                    int y = i + theLowest.y;

                    if (y < 0 || y >= whitheBoard.Height)
                        continue;

                    if (x1 > x2)
                    {
                        var tmp = x1;
                        x1 = x2;
                        x2 = tmp;
                    }

                    if (x2 >= whitheBoard.Width)
                        x2 = whitheBoard.Width - 1;
                    for (int j = x1 > 0 ? x1 : 0; j <= x2; j++)
                    {
                        whitheBoard.SetPixel(j, y, fillColor);
                    }
                }
            }

            if (LineColor != fillColor)
            {
                drawing_lines.drawe(trialagleToFill.x1, trialagleToFill.y1, trialagleToFill.x2, trialagleToFill.y2, ref whitheBoard, LineColor);
                drawing_lines.drawe(trialagleToFill.x2, trialagleToFill.y2, trialagleToFill.x3, trialagleToFill.y3, ref whitheBoard, LineColor);
                drawing_lines.drawe(trialagleToFill.x3, trialagleToFill.y3, trialagleToFill.x1, trialagleToFill.y1, ref whitheBoard, LineColor);
            }

        }

        private void fillLineConstant(ref Bitmap whitheBoard, (int x, int y, int z, Point3 p) theLowest, (int x, int y, int z, Point3 p) medium,
                                int TheLowestTheHighests_Highte, int theLowestMedium_Highte, int MediumTheHighests_Highte,
                                double theLowestMediumSkalar, double theLowestTheHighestSkalar, double MediumTheHighestsSkalar)
        {

            Color newColor = CountPixelColor(trialagleToFill.TrialagleCenterInWorld);

            fillLineNone(ref whitheBoard, newColor, newColor, theLowest, medium,
                                 TheLowestTheHighests_Highte, theLowestMedium_Highte, MediumTheHighests_Highte,
                                 theLowestMediumSkalar, theLowestTheHighestSkalar, MediumTheHighestsSkalar);
        }

        private void fillLineGouraud(ref Bitmap whitheBoard, Color fillColor, (int x, int y, int z, Point3 p) theLowest, (int x, int y, int z, Point3 p) medium,
                                int TheLowestTheHighests_Highte, int theLowestMedium_Highte, int MediumTheHighests_Highte,
                                double theLowestMediumSkalar, double theLowestTheHighestSkalar, double MediumTheHighestsSkalar)
        {

            drawing_lines.drawe(trialagleToFill.x1, trialagleToFill.y1, trialagleToFill.x2, trialagleToFill.y2, ref whitheBoard, CountPixelColor(trialagleToFill.a.WorldCoordinates), CountPixelColor(trialagleToFill.b.WorldCoordinates));
            drawing_lines.drawe(trialagleToFill.x2, trialagleToFill.y2, trialagleToFill.x3, trialagleToFill.y3, ref whitheBoard, CountPixelColor(trialagleToFill.b.WorldCoordinates), CountPixelColor(trialagleToFill.c.WorldCoordinates));
            drawing_lines.drawe(trialagleToFill.x3, trialagleToFill.y3, trialagleToFill.x1, trialagleToFill.y1, ref whitheBoard, CountPixelColor(trialagleToFill.c.WorldCoordinates), CountPixelColor(trialagleToFill.a.WorldCoordinates));

            if (TheLowestTheHighests_Highte != 0 && theLowestMedium_Highte != 0)
            {
                for (int i = 0; i <= theLowestMedium_Highte; i++)
                {
                    int x1 = (int)(theLowest.x + theLowestTheHighestSkalar * i);
                    int x2 = (int)(theLowest.x + theLowestMediumSkalar * i);
                    int y = i + theLowest.y;



                    if (y < 0 || y >= whitheBoard.Height)
                        continue;

                    if (x1 > x2)
                    {
                        var tmp = x1;
                        x1 = x2;
                        x2 = tmp;
                    }

                    if (x2 >= whitheBoard.Width)
                        x2 = whitheBoard.Width - 1;
                    x1 = x1 > 0 ? x1 : 0;

                    Color c1 = whitheBoard.GetPixel(x1, y);
                    Color c2 = whitheBoard.GetPixel(x2, y);
                    double lenght = x2 - x1;
                    if (lenght == 0)
                    {
                        for (int j = x1; j <= x2; j++)
                        {
                            whitheBoard.SetPixel(j, y, c2);
                        }
                    }
                    else
                        for (int j = x1; j <= x2; j++)
                        {
                            whitheBoard.SetPixel(j, y, drawing_lines.ColorInterpolation(c1, c2, (j - x1) / lenght));
                        }
                }
            }

            if (MediumTheHighests_Highte != 0 && TheLowestTheHighests_Highte != 0)
            {
                for (int i = theLowestMedium_Highte; i <= TheLowestTheHighests_Highte; i++)
                {
                    int x1 = (int)(theLowest.x + theLowestTheHighestSkalar * i);
                    int x2 = (int)(medium.x + MediumTheHighestsSkalar * (i - theLowestMedium_Highte));
                    int y = i + theLowest.y;

                    if (y < 0 || y >= whitheBoard.Height)
                        continue;

                    if (x1 > x2)
                    {
                        var tmp = x1;
                        x1 = x2;
                        x2 = tmp;
                    }

                    if (x2 >= whitheBoard.Width)
                        x2 = whitheBoard.Width - 1; 
                    x1 = x1 > 0 ? x1 : 0;
                    Color c1 = whitheBoard.GetPixel(x1, y);
                    Color c2 = whitheBoard.GetPixel(x2, y);
                    double lenght = x2 - x1;
                    if (lenght == 0)
                    {
                        for (int j = x1; j <= x2; j++)
                        {
                            whitheBoard.SetPixel(j, y, c2);
                        }
                    }
                    else
                        for (int j = x1; j <= x2; j++)
                        {
                            whitheBoard.SetPixel(j, y, drawing_lines.ColorInterpolation(c1, c2, (j - x1) / lenght));
                        }

                    
                }
            }
        }

        private void fillLinePhonge(ref Bitmap whitheBoard, Color fillColor,
                                (int x, int y, int z, Point3 p) theLowest, (int x, int y, int z, Point3 p) medium, (int x, int y, int z, Point3 p) theHighest,
                                int TheLowestTheHighests_Highte, int theLowestMedium_Highte, int MediumTheHighests_Highte,
                                double theLowestMediumSkalar, double theLowestTheHighestSkalar, double MediumTheHighestsSkalar)
        {

            float3 theLowestTheHighestIn3D = theHighest.p.WorldCoordinates - theLowest.p.WorldCoordinates;
            float3 theLowestMediumIn3D = medium.p.WorldCoordinates - theLowest.p.WorldCoordinates;
            float3 MediumTheHighestsIn3D = theHighest.p.WorldCoordinates - medium.p.WorldCoordinates;


            
            if (TheLowestTheHighests_Highte != 0 && theLowestMedium_Highte != 0)
            {
                for (int i = 0; i <= theLowestMedium_Highte; i++)
                {
                    int x1 = (int)(theLowest.x + theLowestTheHighestSkalar * i);
                    int x2 = (int)(theLowest.x + theLowestMediumSkalar * i);
                    int y = i + theLowest.y;
                    float3 a = theLowestTheHighestIn3D/ TheLowestTheHighests_Highte;
                    float3 b = theLowestMediumIn3D / theLowestMedium_Highte;

                    if (y < 0 || y >= whitheBoard.Height)
                        continue;

                    if (x1 > x2)
                    {
                        var tmp = x1;
                        x1 = x2;
                        x2 = tmp;
                        var tmp2 = a;
                        a = b;
                        b = tmp2;
                    }

                    float3 fromInWorld = theLowest.p.WorldCoordinates + a * i;
                    float3 ToInWorld = theLowest.p.WorldCoordinates + b * i;
                    float3 vectorFromTo = x2 != x1 ?(ToInWorld - fromInWorld)/(x2-x1):(0,0,0);

                    if (x2 >= whitheBoard.Width)
                        x2 = whitheBoard.Width - 1;
                    for (int j = x1 > 0 ? x1 : 0; j <= x2; j++)
                    {
                        float3 pixelInWorld = fromInWorld + vectorFromTo * (j - x1);
                        whitheBoard.SetPixel(j, y, CountPixelColor(pixelInWorld));
                    }
                }
            }

            if (MediumTheHighests_Highte != 0 && TheLowestTheHighests_Highte != 0)
            {
                for (int i = theLowestMedium_Highte; i <= TheLowestTheHighests_Highte; i++)
                {
                    int x1 = (int)(theLowest.x + theLowestTheHighestSkalar * i);
                    int x2 = (int)(medium.x + MediumTheHighestsSkalar * (i - theLowestMedium_Highte));
                    int y = i + theLowest.y;
                    float3 a = theLowestTheHighestIn3D / TheLowestTheHighests_Highte;
                    float3 b = MediumTheHighestsIn3D / MediumTheHighests_Highte;
                    float3 startA = theLowest.p.WorldCoordinates;
                    float3 startB = medium.p.WorldCoordinates;

                    if (y < 0 || y >= whitheBoard.Height)
                        continue;

                    if (x1 > x2)
                    {
                        var tmp = x1;
                        x1 = x2;
                        x2 = tmp;
                        var tmp2 = a;
                        a = b;
                        b = tmp2;
                        var tmp3 = startA;
                        startA = startB;
                        startB = tmp3;
                    }

                    float3 fromInWorld = startA + a * i;
                    float3 ToInWorld = startB + b * i;
                    float3 vectorFromTo = x2 != x1 ? (ToInWorld - fromInWorld) / (x2 - x1) : (0, 0, 0);

                    if (x2 >= whitheBoard.Width)
                        x2 = whitheBoard.Width - 1;
                    for (int j = x1 > 0 ? x1 : 0; j <= x2; j++)
                    {
                        float3 pixelInWorld = fromInWorld + vectorFromTo * (j - x1);
                        whitheBoard.SetPixel(j, y, CountPixelColor(pixelInWorld));
                    }
                }
            }
        }

        private Color CountPixelColor(float3 pixelLokation)
        {
            float4 newColor;
            float4 ambient = Material.ambient * lights[0].ambient;
            newColor = ambient;

            foreach (var light in lights)
            {
                float4 fromPixelToLighte = (float4)(light.FigureCenter - pixelLokation);
                float dist = MathF.Sqrt(MathF.Pow(fromPixelToLighte.x, 2) + MathF.Pow(fromPixelToLighte.y, 2) + MathF.Pow(fromPixelToLighte.z, 2));
                Matrix<float> tmp = fromPixelToLighte;
                tmp.Normalization_4x1();
                fromPixelToLighte = tmp;
                float4 VersorNormalny = (float4)(trialagleToFill.normalVector);
                float4 diffuse = Material.diffuse * floats.Cos(fromPixelToLighte, VersorNormalny) * light.diffuse;

                float4 fromPixelToViver = (float4)(cameraPointer.Camera.Position - pixelLokation);
                float4 R = VersorNormalny * 2 * floats.Cos(fromPixelToLighte, VersorNormalny) - fromPixelToLighte;
                float4 specular = Material.specular * light.specular * MathF.Pow(floats.Cos(R, fromPixelToViver), Material.shininess);

                float If = 1 / (light.Ac + light.Ac * dist + light.Aq * MathF.Pow(dist, 2));
                newColor += (diffuse + specular) * If;
            }
            newColor *= 255;

            return ConvertToColor(newColor);
        }

        private Color ConvertToColor(float4 ColorInFloat)
        {
            ColorInFloat.g = ColorInFloat.g > 255 ? 255 : ColorInFloat.g;
            ColorInFloat.x = ColorInFloat.x > 255 ? 255 : ColorInFloat.x;
            ColorInFloat.y = ColorInFloat.y > 255 ? 255 : ColorInFloat.y;
            ColorInFloat.z = ColorInFloat.z > 255 ? 255 : ColorInFloat.z;

            ColorInFloat.g = ColorInFloat.g < 0 ? 0 : ColorInFloat.g;
            ColorInFloat.x = ColorInFloat.x < 0 ? 0 : ColorInFloat.x;
            ColorInFloat.y = ColorInFloat.y < 0 ? 0 : ColorInFloat.y;
            ColorInFloat.z = ColorInFloat.z < 0 ? 0 : ColorInFloat.z;

            return Color.FromArgb((int)ColorInFloat.x, (int)ColorInFloat.y, (int)ColorInFloat.z, (int)ColorInFloat.g);
        }


    }
}
