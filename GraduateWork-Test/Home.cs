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
        private readonly MultiCast _multiCast;
        internal Setting DlgSetting { private set; get; }
        internal PerfomanceInfoData Perfomance { private set; get; }

        public Home()
        {
            InitializeComponent();

            DlgSetting = new Setting();
            Perfomance = PsApiWrapper.GetPerformanceInfo();

            _multiCast = new MultiCast(this, "224.5.6.7");

            base.Text = DlgSetting.UserName;
        }

        public void SetData(string text)
        {
            dataBox.Text = text;
        }

        private void ExitMenu_Click(object sender, EventArgs e) => Close();

        private void Home_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode & Keys.Escape) == Keys.Escape) Close();
        }

        public DialogResult ShowMessage(string text, string caption, MessageBoxIcon icon = MessageBoxIcon.Stop)
        {
            return MessageBox.Show(text, caption, MessageBoxButtons.OK, icon);
        }

        private void TimerUpdateData_Tick(object sender, EventArgs e)
        {
            Perfomance = PsApiWrapper.GetPerformanceInfo();
            statusTextStrip.Text = string.Format(Resources.Home_timerUpdateData_Tick__0____1____2____3_,
                Perfomance.PhysicalTotalMb, Perfomance.PhysicalAvailableMb,
                Perfomance.PhysicalPercentFree, Perfomance.PhysicalOccupied); //избыточные данные

            _multiCast.Write($"PhysicalTotalMb: {Perfomance.PhysicalTotalMb};\n" +
                             $"PhysicalAvailableMb: {Perfomance.PhysicalAvailableMb};\n" +
                             $"PhysicalPercentFree: {Perfomance.PhysicalPercentFree};\n" +
                             $"PhysicalOccupied: {Perfomance.PhysicalOccupied}");
        }

        private void EventLog_EntryWritten(object sender, EntryWrittenEventArgs e)
        {
            //Что-то должно происходить
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            if (MessageBox.Show(string.Format(Resources.Home_OnFormClosing_, DlgSetting.UserName),
                    @"Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes) return;

            e.Cancel = true;
        }
    }
}