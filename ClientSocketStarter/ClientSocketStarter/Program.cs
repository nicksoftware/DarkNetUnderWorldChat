using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ClientSocketStarter
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket client = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);

            IPAddress iP = null;

            try
            {
                Console.WriteLine("Welcome to The Path way to the Dark Under world of Hackers Created by Nick the Coder ");
                Console.WriteLine("Please type in a valid Dark World server IP Address and press enter to continue :");
                string strIpAddress = Console.ReadLine();

                while (!IPAddress.TryParse(strIpAddress, out iP))
                {
                    Console.WriteLine("Please type in a valid server IP Address and press enter to continue :");
                    strIpAddress = Console.ReadLine();
                };

                Console.WriteLine("Please enter a valid port number of the server the valid port range is  between 0 - 65535) and press enter to continue : ");
                string strPort = Console.ReadLine();

                int intPort = 0;
        
                if (!int.TryParse(strPort, out intPort))
                {
                    Console.WriteLine("Please type in a valid integer Port number and press enter to continue :");
                    strPort = Console.ReadLine();

                }
                if (intPort < 0 || intPort > 65535)
                {   
                    Console.WriteLine("Please type in a valid integer Port number and press enter to continue :");
                    strIpAddress = Console.ReadLine();
                   
                }

                Console.WriteLine($"IPAddress : {strIpAddress} - Port : {strPort}");

                client.Connect(iP, intPort);
                Console.WriteLine("You are now connected to the Dark Net server, type your Message and press enter to send it to the under world, or type <Exit> to close  ");

                while (true)
                {
                    var inputCommand = Console.ReadLine();
                    if (inputCommand == "<Exit>" || inputCommand == "<exit>" || inputCommand == "<EXIT>")
                    {
                        break;
                    }
                    var buffSend = Encoding.ASCII.GetBytes(inputCommand);
                    client.Send(buffSend);
                    byte[] buffRecieved = new byte[128];
                    var numberOfBytesRecieved =  client.Receive(buffRecieved);
                    var recievedMessage = Encoding.ASCII.GetString(buffRecieved, 0, numberOfBytesRecieved);

                    Console.WriteLine($"Data recieved :{recievedMessage}");
                }
                  
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if(client != null)
                {
                    if (client.Connected)
                    {
                        client.Shutdown(SocketShutdown.Both);
                    }
                    client.Close();
                    client.Dispose();
                }
       

            }
            Console.WriteLine("Press a key to exit");
            Console.ReadKey();


        }
    }
}
