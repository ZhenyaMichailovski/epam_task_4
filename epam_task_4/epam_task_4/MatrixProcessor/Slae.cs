using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace epam_task_4.MatrixProcessor
{
    class Slae
    {
        public int N { get { return Columns.Length; } }
        private double[,] matrix;
        public double[] Columns { get; private set; }
        public double[,] Matrix
        {
            get { return matrix; }
            private set
            {
                matrix = value;
            }
        }

        public Slae(double[,] matrix, double[] b)
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
