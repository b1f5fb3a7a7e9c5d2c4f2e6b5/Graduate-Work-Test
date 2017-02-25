using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GraduateWork_Test
{
    public class ZedGraphControler
    {
        private Dictionary<string, string> PeerData { set; get; }

        public ZedGraphControler()
        {
            PeerData = new Dictionary<string, string>();
        }

        public void SetPeerData(string key, string data)
        {
            if (PeerData.ContainsKey(key))
            {
                PeerData[key] = data;
            }
            else
            {
                PeerData.Add(key, data);
            }
        }

        public string GetPeerData(string key)
        {
            return (!PeerData.ContainsKey(key)) ? "_Please_Update_Peer_" : PeerData[key];
        }
    }
}
