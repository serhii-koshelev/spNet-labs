using CNnet_Lab1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UDPSocket;

namespace UDPSocket
{
    class Program
    {

        static void Main(string[] args)

        {
    
            Console.Write("Please enter port number for incoming messages: ");
            SocketLogic.localPort = Int32.Parse(Console.ReadLine());
            Console.Write("Please enter port number for outcoming messages: ");
            SocketLogic.remotePort = Int32.Parse(Console.ReadLine());
            Console.WriteLine("To send a message type it and hit Enter");
            Console.WriteLine();

            try
            {
                SocketLogic.listeningSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                Task listeningTask = new Task(SocketLogic.ListenSocket);
                listeningTask.Start();

                // Send messages to different ports
                while (true)
                {
                    string message = Console.ReadLine();

                    byte[] data = Encoding.Unicode.GetBytes(message);
                    EndPoint remotePoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), SocketLogic.remotePort);
                    SocketLogic.listeningSocket.SendTo(data, remotePoint);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                SocketLogic.CloseSocket();
            }
        }
    }
}
