using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        private const int PORT = 8008;
        private const string IP_ADDR = "127.0.0.1";

        private static Socket socket;
        private static IPEndPoint iPEnd;
        public Form1()
        {
            InitializeComponent();

            iPEnd = new IPEndPoint(IPAddress.Parse(IP_ADDR), PORT);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Connect(iPEnd);
            }
            catch
            {

            }

            this.FormClosed += CloseMySockets;
        }

        private void CloseMySockets(object sender, FormClosedEventArgs e)
        {
            socket.Shutdown(SocketShutdown.Send);
            socket.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                int byteCount = 0;
                byte[] buffer = new byte[256];
                // StringBuilder stringBuilder = new StringBuilder();

                //
                //  Отправка команды
                //
                // Console.Write(" > ");
                if (textBox1.Text != String.Empty)
                {
                    string msg = textBox1.Text;
                    byte[] data = null;
                    data = Encoding.Unicode.GetBytes(msg);
                    socket.Send(data);
                    // stringBuilder.Clear();

                    if (msg.StartsWith("path "))
                    {
                        byte[] FileFata = new byte[256];
                        string TEMP = msg.Remove(0, 5);
                        FileFata = File.ReadAllBytes(TEMP);
                        socket.Send(FileFata);
                    }
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            finally
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                try
                {

                    int byteCount = 0;
                    byte[] buffer = new byte[256];
                    // StringBuilder stringBuilder = new StringBuilder();

                    //
                    //  Отправка команды
                    //
                    //if (textBox1.Text != String.Empty)
                    //{
                       
                        this.textBox1.Text= $"path {openFileDialog1.FileName}";
                        //string msg = $"path {openFileDialog1.FileName}";
                       /* byte[] data = null;
                        data = Encoding.Unicode.GetBytes(msg);
                        socket.Send(data);
                        byte[] FileFata = new byte[256];
                        string TEMP = msg.Remove(0, 5);
                        FileFata = File.ReadAllBytes(TEMP);
                        socket.Send(FileFata);*/

                    //}
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc);
                }



            }
        }
    }
}
