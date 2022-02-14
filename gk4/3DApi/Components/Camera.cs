using gk4.Matrix;
using System;

namespace gk4._3DApi.Components
{
    public class CameraPointer
    {
        public Camera Camera;
    }

    public class Camera
    {
        private Matrix<float> VIEW = new Matrix<float>(4, 4);
        private Matrix<float> PROJ = new Matrix<float>(4, 4);
        private float aspect;
        private float FOV;
        private float N, F;
        private float3 POSITION;
        private float3 LOOK_AT;

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

        public Matrix<float> View
        {
            get
            {
                return VIEW;
            }
        }

        public Matrix<float> Proj
        {
            get
            {
                return PROJ;
            }
        }

        public float3 Position
        {
            set
            {
                this.POSITION.x = value.x;
                this.POSITION.y = value.y;
                this.POSITION.z = value.z;

                refreshViever();
            }
            get
            {
                return POSITION;
            }
        }

        public float3 LookAt
        {
            set
            {
                this.LOOK_AT.x = value.x;
                this.LOOK_AT.y = value.y;
                this.LOOK_AT.z = value.z;

                refreshViever();
            }
        }


        public Camera(float position_x, float position_y, float position_z,
                        float lookAt_x, float lookAt_y, float lookAt_z,
                        int wide, int hieght, float n, float f, float fov): this((float3)(position_x, position_y, position_z), (float3)(lookAt_x, lookAt_y, lookAt_z), wide, hieght, n,  f, fov)
        {}

            public Camera(float3 position, float3 lookAt,
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

            this.Position = position;
            this.LookAt = lookAt;
            RefreshPerspective();
            refreshViever();
        }

        public void refreshViever()
        {
            Matrix<float> Uworld = new Matrix<float>(3, 1);
            Uworld[0, 0] = 0; Uworld[1, 0] = 1; Uworld[2, 0] = 0;

            Matrix<float> D = new Matrix<float>(3, 1);
            D[0, 0] = POSITION.x - LOOK_AT.x; D[1, 0] = POSITION.y - LOOK_AT.y; D[2, 0] = POSITION.z - LOOK_AT.z;

            Matrix<float> R = MatrixTransformationNeededTo3DModeling.cross_product(Uworld, D);

            Matrix<float> U = MatrixTransformationNeededTo3DModeling.cross_product(D, R);

            VIEW = new Matrix<float>(4,4);

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

            Matrix<float> P = new Matrix<float>(4, 4);

            P.ReduceToDiagonal();

            P[0, 3] = -POSITION.x;
            P[1, 3] = -POSITION.y;
            P[2, 3] = -POSITION.z;

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
