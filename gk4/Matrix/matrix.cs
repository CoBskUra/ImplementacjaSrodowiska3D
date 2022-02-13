using System;
using System.Text;

namespace g4
{

    public class matrix<T> where T : IComparable
    {
        public T[,] m;

        public matrix(int height, int widght)
        {
            m = new T[height, widght];
        }
            
        public T this[int i, int j]
        {
            get
            {
                return m[i, j];
            }
            set
            {
                m[i, j] = value;
            }
        }

        public T this[int it]
        {
            get
            {
                int i = it / (m.GetLength(0));
                return m[i, it - i*m.GetLength(0)];
            }
            set
            {
                int i = it / (m.GetLength(0));
                m[i, it - i * m.GetLength(0)] = value;
            }
        }

        public int GetLength(int dim)
        {
            return m.GetLength(dim);
        }

        // dodawanie macierzy
        public static matrix<T> operator +(matrix<T> m1, matrix<T> m2)
        {
            if (m1.GetLength(0) != m2.GetLength(0) || m1.GetLength(1) != m2.GetLength(1))
                return null;
            else
            {
                matrix<T> result = new matrix<T>(m1.GetLength(0), m2.GetLength(1));
                for (int i = 0; i < m1.GetLength(0); i++)
                {
                    for (int j = 0; j < m1.GetLength(1); j++)
                    {
                        result[i, j] = (dynamic)m1[i, j] + (dynamic)m2[i, j];
                    }
                }
                return result;
            }
        }

        // mnożenie macierzy
        public static matrix<T> operator *(matrix<T> m1, matrix<T> m2)
        {
            if (m1.GetLength(1) != m2.GetLength(0))
                return null;
            else
            {
                matrix<T> result = new matrix<T>(m1.GetLength(0), m2.GetLength(1));
                for(int i = 0; i<result.GetLength(0); i++)
                {
                    for(int j = 0; j<result.GetLength(1); j++)
                    {
                        for(int ii = 0; ii<m1.GetLength(1); ii++)
                        {
                            result[i, j] += (dynamic)m1[i, ii] * m2[ii, j];
                        }
                    }
                }

                return result;
            }
        }

        public static matrix<double> operator /(matrix<T> m1, double c)
        {
            matrix<double> result = new matrix<double> (m1.GetLength(0), m1.GetLength(1));
            for (int i = 0; i < m1.GetLength(0); i++)
            {
                for (int j = 0; j < m1.GetLength(1); j++)
                {
                    result[i, j] = (dynamic)m1[i, j] / c;
                }
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder a = new StringBuilder(m.Length+m.GetLength(0));
            for(int i = 0; i<m.GetLength(0); i++)
            {
                for(int j = 0; j<m.GetLength(1); j++)
                {
                    a.Append($"{m[i, j]} ");
                }
                a.Append("\n");
            }
            return a.ToString();
        }

        // poniższe funkcje rzutują (wiem że brzydkie ale nie wiedziałem jak inaczej)
        public static implicit operator matrix<float>(matrix<T> m1)
        {
            matrix<float> result = new matrix<float>(m1.GetLength(0), m1.GetLength(1));
            for (int i = 0; i < m1.GetLength(0); i++)
            {
                for (int j = 0; j < m1.GetLength(1); j++)
                {
                    result[i, j] = (float)(dynamic)m1[i, j];
                }
            }
            return result;
        }

        public static implicit operator matrix<int>(matrix<T> m1)
        {
            matrix<int> result = new matrix<int>(m1.GetLength(0), m1.GetLength(1));
            for (int i = 0; i < m1.GetLength(0); i++)
            {
                for (int j = 0; j < m1.GetLength(1); j++)
                {
                    result[i, j] = (int)Math.Round((dynamic)m1[i, j]);
                }
            }
            return result;
        }

        public static implicit operator matrix<double>(matrix<T> m1)
        {
            matrix<double> result = new matrix<double>(m1.GetLength(0), m1.GetLength(1));
            for (int i = 0; i < m1.GetLength(0); i++)
            {
                for (int j = 0; j < m1.GetLength(1); j++)
                {
                    result[i, j] = (double)(dynamic)m1[i, j];
                }
            }
            return result;
        }

        public void ReduceToDiagonal()
        {
            if (m.GetLength(0) != m.GetLength(1))
                throw new Exception("To nie jest macierz kwadratowa!");
            else
            {
                for (int i = 0; i < m.GetLength(0); i++)
                {
                    for (int j = 0; j < m.GetLength(1); j++)
                    {
                        m[i, j] = (dynamic)0;
                    }
                }
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    m[j, j] = (dynamic)1;
                }
            }
        }

        public void Transform()
        {
            T[,] transformMatrix = new T[this.GetLength(1), this.GetLength(0)];
            for(int i = 0; i<this.GetLength(0); i++)
            {
                for (int j = 0; j < this.GetLength(1); j++)
                {
                    transformMatrix[j,i] = this[i, j];
                }
            }
            this.m = transformMatrix;
        }



    }
}
