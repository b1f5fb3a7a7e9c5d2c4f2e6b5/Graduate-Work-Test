using System;
using System.Net;
using System.Linq;
using System.Threading;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Net.PeerToPeer;
using System.Security.Principal;

namespace GraduateWork_Test
{
    public partial class Setting : Form
    {
        internal Guid Id { private set; get; }
        internal WindowsIdentity Identity { private set; get; }

        public Setting()
        {
            InitializeComponent();

            ipAddress.Text = (from address in Dns.GetHostAddresses(Dns.GetHostName())
                              where address.AddressFamily == AddressFamily.InterNetworkV6
                              select $"{address}").FirstOrDefault();

            foreach (var сloud in Cloud.GetAvailableClouds())
            {
                сloudList.Items.Add(сloud.Name);
            }

            сloudList.SelectedIndex = 0;

            Id = Guid.NewGuid();

            //Выдергиваем токен и имя пользоветеля
            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            Identity = (WindowsIdentity)((WindowsPrincipal)Thread.CurrentPrincipal).Identity;
        }

        public string IpAddress => ipAddress.Text;
        public int Port => int.Parse(port.Text);
        public string CloudName => сloudList.GetItemText(сloudList.SelectedItem);
        //public string UserName => $"{Id}";
        public string UserName => $"{Identity.Name.Replace('\\', '/')}/{Identity.Token}/{Id}";
    }
}