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
        public float3 abient;

        private struct verticle
        {
            public int x;
            public int y;
            public int z;
            public Point3 p;
            public char id;
            public List<Color> colors;
            public verticle(int x, int y, int z, Point3 p, char id)
            {
                this.x = x; this.y = y; this.z = z;
                this.p = p; this.id = id;
                colors = new List<Color>();
            }
        }

        private void sort(ref verticle theHighest, ref verticle theLowest, ref verticle medium)
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
            verticle theHighest = new verticle(trialagleToFill.x1, trialagleToFill.y1, trialagleToFill.z1, trialagleToFill.a, 'a');
            verticle theLowest = new verticle(trialagleToFill.x2, trialagleToFill.y2, trialagleToFill.z2, trialagleToFill.b, 'b');
            verticle medium = new verticle(trialagleToFill.x3, trialagleToFill.y3, trialagleToFill.z2, trialagleToFill.c, 'c');

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


        private void fill(ref Bitmap whitheBoard, Color fillColor, Color LineColor, verticle theLowest, verticle medium, verticle theHighest,
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
                fillLineGouraud(ref whitheBoard, fillColor, theLowest, medium, theHighest,
                                 TheLowestTheHighests_Highte, theLowestMedium_Highte, MediumTheHighests_Highte,
                                 theLowestMediumSkalar, theLowestTheHighestSkalar, MediumTheHighestsSkalar);
            else if (shading == ShadingOption.Phonge)
                fillLinePhonge(ref whitheBoard, theLowest, medium, theHighest,
                                 TheLowestTheHighests_Highte, theLowestMedium_Highte, MediumTheHighests_Highte,
                                 theLowestMediumSkalar, theLowestTheHighestSkalar, MediumTheHighestsSkalar);
            
            
        }


        private void fillLineNone(ref Bitmap whitheBoard, Color fillColor, Color LineColor, verticle theLowest, verticle medium,
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

        private void fillLineConstant(ref Bitmap whitheBoard, verticle theLowest, verticle medium,
                                int TheLowestTheHighests_Highte, int theLowestMedium_Highte, int MediumTheHighests_Highte,
                                double theLowestMediumSkalar, double theLowestTheHighestSkalar, double MediumTheHighestsSkalar)
        {

            Color newColor = CountPixelColor(trialagleToFill.TrialagleCenterInWorld);

            fillLineNone(ref whitheBoard, newColor, newColor, theLowest, medium,
                                 TheLowestTheHighests_Highte, theLowestMedium_Highte, MediumTheHighests_Highte,
                                 theLowestMediumSkalar, theLowestTheHighestSkalar, MediumTheHighestsSkalar);
        }

        private void fillLineGouraud(ref Bitmap whitheBoard, Color fillColor, verticle theLowest, verticle medium, verticle theHighest,
                                int TheLowestTheHighests_Highte, int theLowestMedium_Highte, int MediumTheHighests_Highte,
                                double theLowestMediumSkalar, double theLowestTheHighestSkalar, double MediumTheHighestsSkalar)
        {
            
            drawing_lines.drawe(theLowest.x, theLowest.y, theHighest.x, theHighest.y,
                ref whitheBoard, CountPixelColor(theLowest.p.WorldCoordinates),
                CountPixelColor(theHighest.p.WorldCoordinates), theLowest.colors);
            drawing_lines.drawe(theLowest.x, theLowest.y, medium.x, medium.y,
                ref whitheBoard, CountPixelColor(theLowest.p.WorldCoordinates),
                CountPixelColor(medium.p.WorldCoordinates), medium.colors);
            drawing_lines.drawe(medium.x, medium.y, theHighest.x, theHighest.y,
                ref whitheBoard, CountPixelColor(medium.p.WorldCoordinates),
                CountPixelColor(theHighest.p.WorldCoordinates), theHighest.colors);




            if (TheLowestTheHighests_Highte != 0 && theLowestMedium_Highte != 0)
            {
                for (int i = 0; i <= theLowestMedium_Highte; i++)
                {
                    int x1 = (int)(theLowest.x + theLowestTheHighestSkalar * i);
                    int x2 = (int)(theLowest.x + theLowestMediumSkalar * i);
                    int y = i + theLowest.y;



                    if (y < 0 || y >= whitheBoard.Height)
                        continue;

                    Color c1 = theLowest.colors[y - theLowest.y];
                    Color c2 = medium.colors[y - theLowest.y< medium.colors.Count? y - theLowest.y: medium.colors.Count-1];

                    if (x1 > x2)
                    {
                        var tmp = x1;
                        x1 = x2;
                        x2 = tmp;
                        var tmp2 = c1;
                        c1 = tmp2;
                        c2 = c1;
                    }

                    if (x2 >= whitheBoard.Width)
                        x2 = whitheBoard.Width - 1;
                    x1 = x1 > 0 ? x1 : 0;

                    
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

                    Color c1 = theLowest.colors[y - theLowest.y < theLowest.colors.Count ? y - theLowest.y : theLowest.colors.Count - 1 ];
                    Color c2 = theHighest.colors[y - medium.y];

                    if (x1 > x2)
                    {
                        var tmp = x1;
                        x1 = x2;
                        x2 = tmp;
                        var tmp2 = c1;
                        c1 = tmp2;
                        c2 = c1;
                    }

                    if (x2 >= whitheBoard.Width)
                        x2 = whitheBoard.Width - 1; 
                    x1 = x1 > 0 ? x1 : 0;
                    
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

        

        private void fillLinePhonge(ref Bitmap whitheBoard,
                                verticle theLowest, verticle medium, verticle theHighest,
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
            float3 newColor;
            float3 ambient = this.abient;
            newColor = ambient * Material.ambient;
            float3 fromPixelToViver = (0,0,0);

            foreach (var light in lights)
            {
                float3 vectorFromPixelToLighte = (float3)(light.FigureCenter - pixelLokation);
                float dist = this.dis(vectorFromPixelToLighte);
                Matrix<float> tmp = vectorFromPixelToLighte;
                tmp.Normalization_3x1();
                vectorFromPixelToLighte = tmp;
                float3 VersorNormalny = (float3)(trialagleToFill.normalVector);
                float3 diffuse = Material.diffuse * floats.Cos(vectorFromPixelToLighte, VersorNormalny) * light.diffuse;

                fromPixelToViver = (float3)(cameraPointer.Camera.Position - pixelLokation);
                float3 R = VersorNormalny * 2 * floats.Cos(vectorFromPixelToLighte, VersorNormalny) - vectorFromPixelToLighte;
                float3 specular = Material.specular * light.specular * MathF.Pow(floats.Cos(R, fromPixelToViver), Material.shininess);

                float If = 1 / (light.Ac + light.Ac * dist + light.Aq * MathF.Pow(dist, 2));
                newColor += (diffuse + specular) * If;
            }

            return ConvertToColor(newColor, this.dis(fromPixelToViver));
        }

        private float dis(float3 vector)
        {
            return MathF.Sqrt(MathF.Pow(vector.x, 2) + MathF.Pow(vector.y, 2) + MathF.Pow(vector.z, 2));
        }

        private Color ConvertToColor(float3 ColorInFloat, float dis)
        {
            // Mgła
            ColorInFloat *= MathF.Exp(-MathF.Pow((dis * 0.2f),2));

            ColorInFloat *= 255;
            ColorInFloat.x = ColorInFloat.x > 255 ? 255 : ColorInFloat.x;
            ColorInFloat.y = ColorInFloat.y > 255 ? 255 : ColorInFloat.y;
            ColorInFloat.z = ColorInFloat.z > 255 ? 255 : ColorInFloat.z;

            ColorInFloat.x = ColorInFloat.x < 0 ? 0 : ColorInFloat.x;
            ColorInFloat.y = ColorInFloat.y < 0 ? 0 : ColorInFloat.y;
            ColorInFloat.z = ColorInFloat.z < 0 ? 0 : ColorInFloat.z;

            return Color.FromArgb((int)ColorInFloat.x, (int)ColorInFloat.y, (int)ColorInFloat.z);
        }


    }
}
