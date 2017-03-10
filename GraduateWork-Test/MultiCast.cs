using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GraduateWork_Test
{
    /// <summary>
    /// Multicast — групповая передача;
    /// Форма широковещания, при которой адресом назначения сетевого пакета является мультикастная группа (один ко многим).
    /// </summary>
    public class MultiCast
    {
        private Home _home;

        /// <summary>
        /// Мультикаст-группа
        /// </summary>
        internal IPAddress Ip { private set; get; }

        /// <summary>
        /// (Port) - Порт для отправки; (Port++) - порт для прослушки;
        /// </summary>
        internal int Port { private set; get; }

        /// <summary>
        /// Время жизни пакета
        /// </summary>
        internal int Ttl { private set; get; }

        /// <summary>
        /// Кол-во сообщений
        /// </summary>
        internal int Rep { private set; get; }

        public MultiCast(Home home, string ip, int port = 4001, int ttl = 5, int rep = 1)
        {
            this._home = home;
            this.Ip = IPAddress.Parse(ip);
            this.Port = port;
            this.Ttl = ttl;
            this.Rep = rep;

            var thread = new Task(Read);
            thread.Start();
        }

        /// <summary>
        /// Отправка сообщения
        /// </summary>
        /// <param name="text">Текст сообщения</param>
        public void Write(string text)
        {
            try
            {
                var socket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Dgram, ProtocolType.Udp);

                socket.SetSocketOption(SocketOptionLevel.IP,
                    SocketOptionName.AddMembership, new MulticastOption(Ip));

                socket.SetSocketOption(SocketOptionLevel.IP,
                    SocketOptionName.MulticastTimeToLive, Ttl);

                socket.Connect(new IPEndPoint(Ip, Port));

                var byteText = Encoding.ASCII.GetBytes(text);
                for (var x = 0; x < Rep; x++)
                {
                    socket.Send(byteText, byteText.Length, SocketFlags.None);
                }

                socket.Close();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        /// Чтение сообщения
        /// </summary>
        public void Read()
        {
            try
            {
                var s = new Socket(AddressFamily.InterNetwork, 
                    SocketType.Dgram, ProtocolType.Udp);

                s.Bind(new IPEndPoint(IPAddress.Any, Port));

                s.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership,
                    new MulticastOption(Ip, IPAddress.Any));

                while (true)
                {
                    var byteText = new byte[1024];

                    s.Receive(byteText);

                    var text = Encoding.ASCII.GetString(byteText, 0, byteText.Length);

                    _home.Invoke(new Action(() => _home.SetData(text)));
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
