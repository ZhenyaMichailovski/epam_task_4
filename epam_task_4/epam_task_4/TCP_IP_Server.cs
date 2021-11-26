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
    class TCP_IP_Server
    {
        const string ip = "127.0.0.1";

        const int port = 8080;

        Event.Event myEvent = new Event.Event();

        public string ServerData(int clientNum, Event.Result result)
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

                return data.ToString();
            }
        }

        public double[] GaussCalculate(StringBuilder data)
        {
            MatrixProcessor.MatrixProcessor.GetDate(data.ToString(), out double[,] a, out double[] b);

            GaussProcessor.GaussProcessor gaussMethod = new GaussProcessor.GaussProcessor();
            Slae slae = new Slae(a, b);

            return gaussMethod.Solve(slae);
        }
    }
}
