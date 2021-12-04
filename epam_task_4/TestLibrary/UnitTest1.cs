using epam_task_4;
using epam_task_4.MatrixProcessor;
using epam_task_4.GaussProcessor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Sockets;
using System.Text;
using FluentAssertions;

namespace TestLibrary
{
    [TestClass]
    public class UnitTest1
    {
        private double[][] matrix =
        {
            new double[]{ 4, 2, -1 },
            new double[]{ 5, 3, -2 },
            new double[]{ 3, 2, -3 },
        };
        private double[] columns = { 1, 2, 0 };

        private double[] answer = { -1, 3, 1 };

        [TestMethod]
        public void TestGaussMethod()
        {
            Slae slae = new Slae(matrix, columns);

            var result = GaussProcessor.Solve(slae);

            result.Should().BeEquivalentTo(answer);
        }

        [TestMethod]
        public void TestParallelGaussMethod()
        {
            Slae slae = new Slae(matrix, columns);

            var result = GaussProcessor.SolveParallel(slae);

            result.Should().BeEquivalentTo(answer);
        }

        [TestMethod]
        public void TestGetDateMethod()
        {
            var str = " 4,000   2,000  -1,000  \r\n    5,000   3,000  -2,000  \r\n    3,000   2,000  -3,000  \r\n   1,000\r\n    2,000\r\n    0,000\r\n";
            MatrixProcessor.GetDate(str, out double[][] a, out double[] b);

            a.Should().BeEquivalentTo(matrix);
            b.Should().BeEquivalentTo(columns);
        }
    }
}
