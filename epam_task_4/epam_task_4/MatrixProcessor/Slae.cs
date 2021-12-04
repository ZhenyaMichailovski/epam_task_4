using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace epam_task_4.MatrixProcessor
{
    /// <summary>
    /// class with matrix data
    /// </summary>
    public class Slae
    {
        /// <summary>
        /// dimension of the matrix
        /// </summary>
        public int N { get { return Columns.Length; } }

        /// <summary>
        /// main matrix
        /// </summary>
        private double[][] matrix;

        /// <summary>
        /// free members
        /// </summary>
        public double[] Columns { get; private set; }

        public double[][] Matrix
        {
            get { return matrix; }
            private set
            {
                matrix = value;
            }
        }

        public Slae(double[][] matrix, double[] b)
        {
            if (matrix.Length == b.Length)
            {
                Matrix = matrix;
                Columns = b;
            }
            else
                throw new ArgumentException();
        }
        
    }
}
