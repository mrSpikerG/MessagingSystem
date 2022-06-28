using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MessagingServer
{
    class Program
    {
        static void Main(string[] args)
        {
            const int PORT = 8008;
            IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse("127.0.0.1"), PORT);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // Console.WriteLine("SERVER START");
            try
            {
                socket.Bind(iPEnd);
                socket.Listen(10);


                //берем клиента
                Socket clientSocket = socket.Accept();
                //Console.WriteLine("SERVER CATCH");
                int byteCount = 0;
                byte[] buffer = new byte[256];
                StringBuilder stringBuilder = new StringBuilder();
                //do
                //{

                do
                {
                   // Task.Factory.StartNew(() =>
                  //  {
                        // do
                        // {
                            stringBuilder.Clear();
                        byteCount = clientSocket.Receive(buffer);
                        stringBuilder.Append(Encoding.Unicode.GetString(buffer, 0, byteCount));
                        // } while (clientSocket.Available > 0);
                        string command = stringBuilder.ToString();

                        if (command != String.Empty)
                        {
                            Console.WriteLine($"Client msg:\t{command}");
                            clientSocket.Send(buffer);
                            stringBuilder.Clear();
                            if (command.Equals("\\end"))
                            {
                                
                            }
                        }
                   // });
                } while (true);

                clientSocket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("SERVER END");
            Console.ReadKey();
        }
    }
}
