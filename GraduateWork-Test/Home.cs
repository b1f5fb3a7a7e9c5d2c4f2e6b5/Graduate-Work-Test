using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using GraduateWork_Test.Properties;

namespace GraduateWork_Test
{
    public partial class Home : Form
    {
        internal Setting DlgSetting { private set; get; }
        internal PeerToPeer P2P { private set; get; }
        internal ZedGraphControler AllPeerData { private set; get; }
        internal PerfomanceInfoData Perfomance { private set; get; }
        internal List<PeerEntry> ListPeerEntry { private set; get; }

        public Home()
        {
            InitializeComponent();

            DlgSetting = new Setting();
            Perfomance = PsApiWrapper.GetPerformanceInfo();
            P2P = new PeerToPeer(this);
            ListPeerEntry = new List<PeerEntry>();
            AllPeerData = new ZedGraphControler();

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
            if ((e.KeyCode & Keys.Escape) == Keys.Escape) Close();
        }

        public DialogResult ShowMessage(string text, string caption, MessageBoxIcon icon = MessageBoxIcon.Stop)
        {
            return MessageBox.Show(text, caption, MessageBoxButtons.OK, icon);
        }

        private void timerUpdateData_Tick(object sender, EventArgs e)
        {
            Perfomance = PsApiWrapper.GetPerformanceInfo();
            statusTextStrip.Text = string.Format(Resources.Home_timerUpdateData_Tick__0____1____2____3_,
                Perfomance.PhysicalTotalMb, Perfomance.PhysicalAvailableMb,
                Perfomance.PhysicalPercentFree, Perfomance.PhysicalOccupied); //избыточные данные

            P2P.SendMessage();
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
            timerSetData.Stop();

            P2P.Closing();

            menuStrip.Items.Insert(menuStrip.Items.IndexOf(disconnectMenu), connectMenu);
            menuStrip.Items.Remove(disconnectMenu);

            refreshMenu.Enabled = false;
        }

        internal void SetRefreshMenuEnabled(bool enabled) //Нужно будет переделать
        {
            refreshMenu.Enabled = enabled;
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            timerSetData.Stop();
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

            if (MessageBox.Show(string.Format(Resources.Home_OnFormClosing_, DlgSetting.UserName), 
                @"Внимание", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                e.Cancel = true;
                return;
            }

            P2P.Closing();
        }

        private void peerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            timerSetData.Start();

            var peerName = peerList.GetItemText(peerList.SelectedItem);
            if (peerName == "Пиры не найдены!") return;

            var peerData = AllPeerData.GetPeerData(peerName);
            if (peerData == "_Please_Update_Peer_")
            {
                P2P.Refresh();
                //return;
            }
            else
            {
                peerDataBox.Text = peerData;
            }
        }
    }
}