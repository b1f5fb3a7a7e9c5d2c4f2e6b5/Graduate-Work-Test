using System;
using System.Linq;
using System.Net;
using System.Net.PeerToPeer;
using System.Net.Sockets;
using System.ServiceModel;
using System.Windows.Forms;

namespace GraduateWork_Test
{
    public class PeerEntry
    {
        public PeerName PeerName { get; set; }
        public IP2PService ServiceProxy { get; set; }
        public string DisplayString { get; set; }
        public bool ButtonsEnabled { get; set; }
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class P2PService : IP2PService
    {
        private readonly Home _hostReference;
        private readonly string _username;

        public P2PService(Home hostReference, string username)
        {
            _hostReference = hostReference;
            _username = username;
        }

        public string GetName()
        {
            return _username;
        }

        public void SendMessage(string message, string from)
        {
            _hostReference.ShowMessage(message, from, MessageBoxIcon.Information);
        }
    }

    [ServiceContract]
    public interface IP2PService
    {
        [OperationContract]
        string GetName();

        [OperationContract(IsOneWay = true)]
        void SendMessage(string message, string from);
    }

    public class PeerToPeer
    {
        private Home _home;
        private P2PService localService;
        private string serviceUrl;
        private ServiceHost host;
        private PeerName peerName;
        private PeerNameRegistration peerNameRegistration;

        public PeerToPeer(Home home)
        {
            _home = home;
        }

        internal void Opening()
        {
            //  Получение URL-адреса службы с использованием адреса IPv6 
            serviceUrl = $"net.tcp://[{_home.DlgSetting.IpAddress}]:{_home.DlgSetting.Port}/P2PService";


            // Выполнение проверки, не является ли адрес null
            if (serviceUrl == null)
            {
                // Отображение ошибки и завершение работы приложения
                _home.ShowMessage("Не удается определить адрес конечной точки", "Networking Error");
                Application.Restart();
            }

            // Регистрация и запуск службы
            localService = new P2PService(_home, _home.DlgSetting.UserName);
            host = new ServiceHost(localService, new Uri(serviceUrl));
            var binding = new NetTcpBinding {Security = {Mode = SecurityMode.Transport}};
            host.AddServiceEndpoint(typeof(IP2PService), binding, serviceUrl);

            try
            {
                host.Open();
            }
            catch (AddressAlreadyInUseException ex)
            {
                // Отображение ошибки и завершение работы приложения c последующим перезапуском
                _home.ShowMessage("Не удается начать прослушивание, порт занят.", $"Error: {ex.Source}");
                Application.Restart();
            }

            // Создание имени равноправного участника (пира)
            peerName = new PeerName("P2P Sample", PeerNameType.Secured);

            // Подготовка процесса регистрации имени равноправного участника в локальном облаке
            peerNameRegistration = new PeerNameRegistration(peerName, _home.DlgSetting.Port, 
                //Cloud.GetCloudByName(_home.DlgSetting.CloudName) ?? 
                Cloud.Available);

            // Запуск процесса регистрации
            peerNameRegistration.Start();
        }

        internal void Closing()
        {
            try
            {
                // Остановка регистрации
                peerNameRegistration?.Stop();

                // Остановка сервиса
                host?.Close();
            }
            catch (InvalidOperationException)
            {
                // ... 
            }

        }

        internal void Refresh()
        {
            // Создание распознавателя и добавление обработчиков событий
            var resolver = new PeerNameResolver();
            resolver.ResolveProgressChanged += ResolveProgressChanged;
            resolver.ResolveCompleted += ResolveCompleted;

            // Подготовка к добавлению новых пиров
            _home.ListPeerEntryClear();
            _home.SetRefreshMenuEnabled(false);

            // Преобразование незащищенных имен пиров асинхронным образом
            resolver.ResolveAsync(new PeerName("P2P Sample", PeerNameType.Secured), _home.DlgSetting.Id);
        }

        private void ResolveProgressChanged(object sender, ResolveProgressChangedEventArgs e)
        {
            var peer = e.PeerNameRecord;

            foreach (var ep in peer.EndPointCollection)
            {
                if (ep.Address.AddressFamily != AddressFamily.InterNetworkV6) continue;

                try
                {
                    string endpointUrl = $"net.tcp://[{ep.Address}]:{ep.Port}/P2PService";
                    var binding = new NetTcpBinding {Security = {Mode = SecurityMode.Transport}};
                    var serviceProxy = ChannelFactory<IP2PService>.CreateChannel(
                        binding, new EndpointAddress(endpointUrl));
                    _home.ListPeerEntryAdd(
                        new PeerEntry
                        {
                            PeerName = peer.PeerName,
                            ServiceProxy = serviceProxy,
                            DisplayString = serviceProxy.GetName(),
                            ButtonsEnabled = true
                        });
                }
                catch (EndpointNotFoundException)
                {
                    _home.ListPeerEntryAdd(
                        new PeerEntry
                        {
                            PeerName = peer.PeerName,
                            DisplayString = "Неизвестный пир",
                            ButtonsEnabled = false
                        });
                }
            }
        }

        private void ResolveCompleted(object sender, ResolveCompletedEventArgs e)
        {
            // Сообщение об ошибке, если в облаке не найдены пиры
            if (_home.ListPeerEntry.Count == 0)
            {
                _home.ListPeerEntryAdd(
                   new PeerEntry
                   {
                       DisplayString = "Пиры не найдены!",
                       ButtonsEnabled = false
                   });
            }

            // Повторно включаем кнопку "обновить"
            _home.SetRefreshMenuEnabled(true);
        }
    }
}