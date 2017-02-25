using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.ServiceModel;
using System.Windows.Forms;
using System.Net.PeerToPeer;
using System.Text;
using System.Xml.Serialization;

namespace GraduateWork_Test
{
    public class PeerEntry
    {
        public PeerName PeerName { get; set; }
        public IP2PService ServiceProxy { get; set; }
        public string DisplayString { get; set; }
        public bool ButtonsEnabled { get; set; }
    }

    [ServiceContract]
    public interface IP2PService
    {
        [OperationContract]
        string GetName();

        [OperationContract(IsOneWay = true)]
        void SendMessage(string peerName, byte[] sendBytes);
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

        public void SendMessage(string peerName, byte[] bytes)
        {
            _hostReference.AllPeerData.SetPeerData(peerName, Encoding.UTF8.GetString(bytes));
        }
    }
}