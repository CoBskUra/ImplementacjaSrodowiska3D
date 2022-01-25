using g4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gk4._3DApi.Components
{
    public class Camera
    {
        private matrix<float> VIEW = new matrix<float>(4, 4);
        private matrix<float> PROJ = new matrix<float>(4, 4);
        private float aspect;
        private float FOV;
        private float N, F;

        public float n
        {
            set
            {
                if (value <= 0)
                    throw new Exception("Wprowadzono wartość mniejszą od zera");
                N = value;
                RefreshPerspective();
            }
            get
            {
                return N;
            }
        }

        public float f
        {
            set
            {
                if (value <= N )
                    throw new Exception("Wprowadzono wartość mniejszą od n");
                F = value;
                RefreshPerspective();
            }
            get
            {
                return F;
            }
        }

        public float fov
        {
            set
            {
                FOV = MathF.Asin(MathF.Sin(value - MathF.PI/2)) + MathF.PI / 2;
                RefreshPerspective();
            }
            get
            {
                return FOV;
            }
        }

        public matrix<float> View
        {
            get
            {
                return VIEW;
            }
        }

        public matrix<float> Proj
        {
            get
            {
                return PROJ;
            }
        }

        private float position_x, position_y, position_z;
        private float lookAt_x, lookAt_y, lookAt_z;

        public Camera(float position_x, float position_y, float position_z,
                        float lookAt_x, float lookAt_y, float lookAt_z,
                        int wide, int hieght, float n, float f, float fov)
        {
            if (n <= 0)
                throw new Exception("Wprowadzono n mniejszą od 0");
            if (f <= n)
                throw new Exception("Wprowadzono f mniejszą od n");
            
            
            N = n;
            F = f;
            aspect = (float)wide / (float)hieght;
            FOV = MathF.Asin(MathF.Sin(fov - MathF.PI / 2)) + MathF.PI / 2;

            this.position_x = position_x;
            this.position_y = position_y;
            this.position_z = position_z;

            this.lookAt_x = lookAt_x;
            this.lookAt_y = lookAt_y;
            this.lookAt_z = lookAt_z;

            refreshViever();
            RefreshPerspective();
        }

        public (float x, float y, float z) Position
        {
            set
            {
                this.position_x = value.x;
                this.position_y = value.y;
                this.position_z = value.z;

                refreshViever();
            }
        }

        public (float x, float y, float z) lookAt
        {
            set
            {
                this.lookAt_x = value.x;
                this.lookAt_y = value.y;
                this.lookAt_z = value.z;

                refreshViever();
            }
        }

        private void refreshViever()
        {
            matrix<float> Uworld = new matrix<float>(3, 1);
            Uworld[0, 0] = 0; Uworld[1, 0] = 1; Uworld[2, 0] = 0;

            matrix<float> D = new matrix<float>(3, 1);
            D[0, 0] = position_x - lookAt_x; D[1, 0] = position_y - lookAt_y; D[2, 0] = position_z - lookAt_z;

            matrix<float> R = MatrixTransformationNeededTo3DModeling.cross_product(Uworld, D);

            matrix<float> U = MatrixTransformationNeededTo3DModeling.cross_product(D, R);

            VIEW[0, 0] = R[0, 0];
            VIEW[0, 1] = R[1, 0];
            VIEW[0, 2] = R[2, 0];

            VIEW[1, 0] = U[0, 0];
            VIEW[1, 1] = U[1, 0];
            VIEW[1, 2] = U[2, 0];

            VIEW[2, 0] = D[0, 0];
            VIEW[2, 1] = D[1, 0];
            VIEW[2, 2] = D[2, 0];

            VIEW[3, 3] = 1;

            matrix<float> P = new matrix<float>(4, 4);

            P.ReduceToDiagonal();

            P[0, 3] = -position_x;
            P[1, 3] = -position_y;
            P[2, 3] = -position_z;


            VIEW = VIEW * P;

        }

        private void RefreshPerspective()
        {
            PROJ[0, 0] = 1 / (MathF.Tan(fov / 2) * aspect);
            PROJ[1, 1] = 1 / MathF.Tan(fov / 2) ;
            PROJ[2, 2] = (f + n) /(f - n);
            PROJ[2, 3] = -2 * f * n / (f - n);
            PROJ[3, 2] = 1;
        }




    }
}
