using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using GraduateWork_Test.Properties;

namespace GraduateWork_Test
{
    public partial class Home : Form
    {
        internal Setting DlgSetting { private set; get; }
        internal PeerToPeer P2P { private set; get; }

        private PerfomanceInfoData _perfomance;
        internal List<PeerEntry> ListPeerEntry;

        public Home()
        {
            InitializeComponent();

            DlgSetting = new Setting();
            _perfomance = PsApiWrapper.GetPerformanceInfo();
            P2P = new PeerToPeer(this);
            ListPeerEntry = new List<PeerEntry>();

            base.Text = DlgSetting.UserName;

            refreshMenu.Enabled = false;
        }

        internal void ListPeerEntryAdd(PeerEntry listPeerEntry)
        {
            ListPeerEntry.Add(listPeerEntry);
            peerList.Items.Add(listPeerEntry.DisplayString);
        }

        internal void ListPeerEntryClear()
        {
            ListPeerEntry.Clear();
            peerList.Items.Clear();
        }

        private static void FileWriteAllText(StringBuilder sb)
        {
            File.WriteAllText(@"..\..\Test.txt", sb.ToString());
        }

        private void exitMenu_Click(object sender, EventArgs e) => Close();

        private void Home_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode & Keys.Escape) == Keys.Escape) Close(); ;
        }

        public DialogResult ShowMessage(string text, string caption, MessageBoxIcon icon = MessageBoxIcon.Stop)
        {
            return MessageBox.Show(text, caption, MessageBoxButtons.OK, icon);
        }

        private void timerUpdateData_Tick(object sender, EventArgs e)
        {
            _perfomance = PsApiWrapper.GetPerformanceInfo();
            statusTextStrip.Text = string.Format(Resources.Home_timerUpdateData_Tick__0____1____2____3_,
                _perfomance.PhysicalTotalMb, _perfomance.PhysicalAvailableMb,
                _perfomance.PhysicalPercentFree, _perfomance.PhysicalOccupied); //избыточные данные
        }

        private void eventLog_EntryWritten(object sender, EntryWrittenEventArgs e)
        {
            //Что-то должно происходить
        }

        private void connectMenu_Click(object sender, EventArgs e)
        {
            if (DlgSetting.ShowDialog() != DialogResult.OK) return;

            P2P.Opening();

            menuStrip.Items.Insert(menuStrip.Items.IndexOf(connectMenu), disconnectMenu);
            menuStrip.Items.Remove(connectMenu);

            refreshMenu.Enabled = true;

            P2P.Refresh();
        }

        private void disconnectMenu_Click(object sender, EventArgs e)
        {
            P2P.Closing();

            menuStrip.Items.Insert(menuStrip.Items.IndexOf(disconnectMenu), connectMenu);
            menuStrip.Items.Remove(disconnectMenu);

            refreshMenu.Enabled = false;
        }

        internal void SetRefreshMenuEnabled(bool enabled)
        {
            refreshMenu.Enabled = enabled;
        }

        private void Home_Load(object sender, EventArgs e)
        {
            //connectMenu_Click(sender, e);
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            P2P.Refresh();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                P2P.Closing();
                return;
            }

            if (MessageBox.Show(@"Вы уверены, что хотите выйти?", @"Внимание",
                    MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                e.Cancel = true;
                return;
            }

            P2P.Closing();
        }

        private void messageMenu_Click(object sender, EventArgs e)
        {
            // Получение пира и прокси, для отправки сообщения
            //PeerEntry peerEntry = ((Button)e.OriginalSource).DataContext as PeerEntry;
            var peerEntry = ListPeerEntry[0];

            if (peerEntry?.ServiceProxy == null) return;

            try
            {
                peerEntry.ServiceProxy.SendMessage("Привет друг!", peerList.GetItemText(peerList.Items[0]));
            }
            catch (CommunicationException)
            {
                //...
            }
        }
    }
}