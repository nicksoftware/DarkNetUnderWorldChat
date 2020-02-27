using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace SocketServerStarter
{
    class Program
    {

        static void Main(string[] args)
        {
            Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress iP = IPAddress.Any;

            IPEndPoint iPEndPoint = new IPEndPoint(iP, 52000);
            try
            {

                listenerSocket.Bind(iPEndPoint);

                listenerSocket.Listen(5);

                Console.WriteLine("Waitin to accept connections");

                var client = listenerSocket.Accept();

                Console.WriteLine($"Client Connected. {client.ToString()} - IP End Point : {client.RemoteEndPoint.ToString()}");

                byte[] buff = new byte[128];

                int recievedBytes = 0;

                while (true)
                {
                    recievedBytes = client.Receive(buff);
                    /*  Console.WriteLine($"Recieved {recievedBytes} bytes")*/
                    ;


                    string recievedText = Encoding.ASCII.GetString(buff, 0, recievedBytes);

                    Console.WriteLine($"Data sent by {client.RemoteEndPoint.ToString()} :{recievedText }");

                    string inputCommand = Console.ReadLine();

                    client.Send(Encoding.ASCII.GetBytes(inputCommand));

                    if (recievedText == "x")
                    {
                        break;
                    }
                    Array.Clear(buff, 0, buff.Length);
                    recievedBytes = 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
