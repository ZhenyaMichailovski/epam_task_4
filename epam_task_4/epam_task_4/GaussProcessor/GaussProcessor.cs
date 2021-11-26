using epam_task_4.MatrixProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace epam_task_4.GaussProcessor
{
    class GaussProcessor
    {
        public double[] Solve(Slae slae)
        {
            // Preparation : copying arrays of slae
            // cuz, u know, it changes data in Slae 
            // object if we'll use data from slae object properly
            double[,] a = new double[slae.N, slae.N];
          
            Array.Copy(slae.Matrix, a, slae.N);

            double[] b = new double[slae.N];
            Array.Copy(slae.Columns, b, slae.N);

            ForwardElimination(a, b, slae.N);
            return BackSubstitution(a, b, slae.N);
        }
        private static void ForwardElimination(double[,] a, double[] b, int n)
        {
            for (int k = 0; k < n; k++)
            {
                Parallel.For(k + 1, n, j =>
                {
                    double d = a[j, k] / a[k, k];

                    for (int i = k; i < n; i++)
                    {
                        a[j, i] = a[j, i] - d * a[k, i];
                    }

                    b[j] = b[j] - d * b[k];
                });
            }
        }
        private static double[] BackSubstitution(double[,] a, double[] b, int n)
        {
            double[] x = new double[n];

            for (int k = n - 1; k >= 0; k--)
            {
                double d = 0;

                for (int j = k + 1; j < n; j++)
                {
                    double s = a[k, j] * x[j];
                    d = d + s;
                }

                x[k] = (b[k] - d) / a[k, k];
            }

            return x;
        }
    }
}
