using epam_task_4.MatrixProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace epam_task_4.GaussProcessor
{
    /// <summary>
    /// class for calculating the gauss method
    /// </summary>
    public static class GaussProcessor
    {
        /// <summary>
        /// a method that sorts strings
        /// </summary>
        /// <param name="slae">matrixs</param>
        /// <param name="SortIndex">number sorting lines</param>
        private static void SortRows(ref Slae slae, int SortIndex)
        {

            double MaxElement = slae.Matrix[SortIndex][SortIndex];
            int MaxElementIndex = SortIndex;
            for (int i = SortIndex + 1; i < slae.N; i++)
            {
                if (slae.Matrix[i][SortIndex] > MaxElement)
                {
                    MaxElement = slae.Matrix[i][SortIndex];
                    MaxElementIndex = i;
                }
            }

            if (MaxElementIndex > SortIndex)
            {
                double Temp;

                Temp = slae.Columns[MaxElementIndex];
                slae.Columns[MaxElementIndex] = slae.Columns[SortIndex];
                slae.Columns[SortIndex] = Temp;

                for (int i = 0; i < slae.N; i++)
                {
                    Temp = slae.Matrix[MaxElementIndex][i];
                    slae.Matrix[MaxElementIndex][i] = slae.Matrix[SortIndex][i];
                    slae.Matrix[SortIndex][i] = Temp;
                }
            }
        }

        /// <summary>
        /// linear gauss method
        /// </summary>
        /// <param name="slae">matrixs</param>
        /// <returns>answer</returns>
        public static double[] Solve(Slae slae)
        {

            for (int i = 0; i < slae.N - 1; i++)
            {
                SortRows(ref slae, i);
                for (int j = i + 1; j < slae.N; j++)
                {
                    if (slae.Matrix[i][i] != 0)
                    {
                        double MultElement = slae.Matrix[j][i] / slae.Matrix[i][i];
                        for (int k = i; k < slae.N; k++)
                            slae.Matrix[j][k] -= slae.Matrix[i][k] * MultElement;
                        slae.Columns[j] -= slae.Columns[i] * MultElement;
                    }
                    
                }
            }
            double[] answer = new double[slae.N];
          
            for (int i = (int)(slae.N - 1); i >= 0; i--)
            {
                answer[i] = slae.Columns[i];

                for (int j = (int)(slae.N - 1); j > i; j--)
                    answer[i] -= slae.Matrix[i][j] * answer[j];

                answer[i] /= slae.Matrix[i][i];
                answer[i] = Math.Round(answer[i], 2);
            }
            return answer;
        }

        /// <summary>
        /// distributed gaussian method
        /// </summary>
        /// <param name="slae">matrixs</param>
        /// <returns>answers</returns>
        public static double[] SolveParallel(Slae slae)
        {

            for (int i = 0; i < slae.N - 1; i++)
            {
                SortRows(ref slae, i);
                for (int j = i + 1; j < slae.N; j++)
                {
                    if (slae.Matrix[i][i] != 0)
                    {
                        double MultElement = slae.Matrix[j][i] / slae.Matrix[i][i];
                        Parallel.For(i, slae.N, (k) => slae.Matrix[j][k] -= slae.Matrix[i][k] * MultElement);

                        slae.Columns[j] -= slae.Columns[i] * MultElement;
                    }

                }
            }
            double[] answer = new double[slae.N];

            for (int i = (int)(slae.N - 1); i >= 0; i--)
            {
                answer[i] = slae.Columns[i];

                for (int j = (int)(slae.N - 1); j > i; j--)
                    answer[i] -= slae.Matrix[i][j] * answer[j];

                answer[i] /= slae.Matrix[i][i];
                answer[i] = Math.Round(answer[i], 2);
            }
            return answer;
        }
    }
}
