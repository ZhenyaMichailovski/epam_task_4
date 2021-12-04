using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace epam_task_4.MatrixProcessor
{
    public static class MatrixProcessor
    {
        /// <summary>
        /// method for processing free members from strings
        /// </summary>
        /// <param name="s">string of data</param>
        /// <returns></returns>
        private static double[] GetColumns(string s)
        {
            string[] str = s.Split('\n');
            double[] columns = new double[str.Length - 1];
            for (int i = 0; i < str.Length - 1; i++)
            {
                columns[i] = Convert.ToDouble(str[i]);
            }
            return columns;
        }

        /// <summary>
        /// method for processing a main matrix from a string
        /// </summary>
        /// <param name="s">string of data</param>
        /// <returns></returns>
        private static double[][] GetMatrix(string s)
        {
            string[] str = s.Split('\n');
            string rowL = Regex.Replace(str[0], @"\s+", " ", RegexOptions.None);
            rowL = rowL.Substring(1, rowL.Length - 1);
            string[] row = rowL.Split(' ');
            double[][] matrix = new double[str.Length - 1][];
            
            for (int i = 0; i < str.Length - 1; i++)
            {
                rowL = Regex.Replace(str[i], @"\s+", " ", RegexOptions.None);
                rowL = rowL.Substring(1, rowL.Length - 1);
                row = rowL.Split(' ');
                matrix[i] = new double[str.Length - 1];
                for (int j = 0; j < row.Length - 1; j++)
                {
                    
                    matrix[i][j] = Convert.ToDouble(row[j]);
                }
            }
            return matrix;
        }

        /// <summary>
        /// matrix processing method
        /// </summary>
        /// <param name="str">string of data</param>
        /// <param name="matrix">matrix</param>
        /// <param name="columns">columns</param>
        public static void GetDate(string str, out double[][] matrix, out double[] columns)
         {
            string[] mas = str.Split('\n');
            StringBuilder builderA = new StringBuilder();
            for (int i = 0; i < mas.Length / 2; i++)
            {
                builderA.Append(mas[i]);
                builderA.Append("\n");
            }

            matrix = GetMatrix(builderA.ToString());

            StringBuilder builderB = new StringBuilder();
            for (int i = mas.Length / 2; i < mas.Length - 1; i++)
            {
                builderB.Append(mas[i]);
                builderB.Append("\n");
            }

            columns = GetColumns(builderB.ToString());
        }
    }
}
