using BackgammonProj.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackgammonProj.GameManager
{
    class Client
    {
        public static Client Instance = new Client();
        private Socket _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private const int PORT = 100;
        private const string IP = "127.0.0.1";
        private byte[] _buffer = new byte[2048];
        private bool _connected = false;



        public void StartClient()
        {
            Debug.Write("Loging to server!");
            try
            {
                _server.Connect(IP, PORT);
                BeginRecive();
                _connected = true;
                Debug.Write("Connected!");
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);

            }
        }

        private void BeginRecive()
        {
            try
            {

                _server.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(OnServerPacketRecive), _server);
            }
            catch (Exception)
            {


            }
        }

        private volatile Mutex mutex = new Mutex();
        private object locker = new object();
        private void OnServerPacketRecive(IAsyncResult ar)
        {
            lock (locker)
            {
                Socket session = (Socket)ar.AsyncState;
                int recive = 0;
                try
                {
                    recive = session.EndReceive(ar);
                }
                catch (Exception)
                {

                    return;
                }
                byte[] packet = GetPacket(recive);
                PacketReader reader = new PacketReader(packet);
                PacketHandler handler = new PacketHandler(reader);
                BeginRecive();
            }

        }
        public void SendPacket(byte[] packet)
        {
            try
            {
                _server.Send(packet);
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Server is down!\r\nClosing Client");
                Environment.Exit(0);

            }
            

        }
        public byte[] GetPacket(int recive)
        {

            byte[] databuff = new byte[recive];
            Array.Copy(_buffer, databuff, recive);
            return databuff;
        }



    }
}
