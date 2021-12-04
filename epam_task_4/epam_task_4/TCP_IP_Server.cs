using epam_task_4.MatrixProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace epam_task_4
{
    /// <summary>
    /// Server class
    /// </summary>
    public class TCP_IP_Server
    {
        /// <summary>
        /// Ip of server
        /// </summary>
        const string ip = "127.0.0.1";

        /// <summary>
        /// Port of server
        /// </summary>
        const int port = 8080;

        Event.Event myEvent = new Event.Event();

        /// <summary>
        /// a method for transmitting data from the server and receiving data from the client
        /// </summary>
        /// <param name="clientNum">clien num</param>
        /// <param name="result">event</param>
        /// <returns></returns>
        public double[] ServerData(int clientNum, Event.Result result)
        {
            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpSocket.Bind(tcpEndPoint);
            tcpSocket.Listen(clientNum);

            while (true)
            {
                var listener = tcpSocket.Accept();
                var buffer = new byte[256];
                var size = 0;
                var data = new StringBuilder();
                do
                {
                    size = listener.Receive(buffer);
                    data.Append(Encoding.UTF8.GetString(buffer, 0, size));
                }
                while (listener.Available > 0);

                var answer = GaussCalculate(data);
                
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < answer.Length - 1; i++)
                {
                    stringBuilder.Append(answer[i] + "\n");
                }

                myEvent.ResultEvent += delegate () {
                    result();
                };
                myEvent.OnResultEvent();

                listener.Send(Encoding.UTF8.GetBytes(stringBuilder.ToString()));
                listener.Shutdown(SocketShutdown.Both);
                listener.Close();

                return answer;
            }
        }

        /// <summary>
        /// matrix processing method
        /// </summary>
        /// <param name="data">data form client</param>
        /// <returns></returns>
        public double[] GaussCalculate(StringBuilder data)
        {
            MatrixProcessor.MatrixProcessor.GetDate(data.ToString(), out double[][] a, out double[] b);

            Slae slae = new Slae(a, b);
            
            return GaussProcessor.GaussProcessor.Solve(slae);
        }
    }
}
