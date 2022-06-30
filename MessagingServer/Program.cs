using System;
using System.IO;
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
            try
            {
                socket.Bind(iPEnd);
                socket.Listen(10);


                //берем клиента
                Socket clientSocket = socket.Accept();
                int byteCount = 0;
                byte[] buffer = new byte[256];
                StringBuilder stringBuilder = new StringBuilder();

                do
                {

                    stringBuilder.Clear();
                    byteCount = clientSocket.Receive(buffer);

                    stringBuilder.Append(Encoding.Unicode.GetString(buffer, 0, byteCount));

                    string command = stringBuilder.ToString();

                    if (command != String.Empty)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("[");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(DateTime.Now);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"] > {command}");

                        File.AppendAllText("Logs.txt", $"[{DateTime.Now}] > {command}\n");



                        clientSocket.Send(buffer);
                        stringBuilder.Clear();
                        //if (command.Equals("\\end"))
                        //{
                        //}

                        if (command.StartsWith("path "))
                        {
                            int id;
                            if (!Directory.Exists("ReservedFiles"))
                            {
                                Directory.CreateDirectory("ReservedFiles");
                                id = 0;
                            }
                            else
                            {
                                id = Directory.GetFiles("ReservedFiles").Length;
                            }

                            byte[] FileFata = new byte[256];
                            int FileDataCount = clientSocket.Receive(FileFata);
                            File.WriteAllBytes($"ReservedFiles\\copy_{id}_{DateTime.Now.ToShortDateString()}{command.Substring(command.Length - 4)}", FileFata);
                        }

                    }

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
