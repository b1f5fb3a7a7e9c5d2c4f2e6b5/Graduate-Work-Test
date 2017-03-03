using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;

namespace GraduateWork_Test
{
    partial class Home
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.peerList = new System.Windows.Forms.ListBox();
            this.eventLog = new System.Diagnostics.EventLog();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.connectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusTextStrip = new System.Windows.Forms.ToolStripStatusLabel();
            this.peerDataBox = new System.Windows.Forms.RichTextBox();
            this.disconnectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.timerUpdateData = new System.Windows.Forms.Timer(this.components);
            this.timerSetData = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.eventLog)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // peerList
            // 
            this.peerList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.peerList.FormattingEnabled = true;
            this.peerList.Location = new System.Drawing.Point(0, 27);
            this.peerList.Name = "peerList";
            this.peerList.Size = new System.Drawing.Size(313, 351);
            this.peerList.TabIndex = 2;
            this.peerList.SelectedIndexChanged += new System.EventHandler(this.peerList_SelectedIndexChanged);
            // 
            // eventLog
            // 
            this.eventLog.SynchronizingObject = this;
            this.eventLog.EntryWritten += new System.Diagnostics.EntryWrittenEventHandler(this.eventLog_EntryWritten);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.refreshMenu,
            this.connectMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(976, 24);
            this.menuStrip.TabIndex = 1;
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitMenu});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(37, 20);
            this.fileMenu.Text = "&File";
            // 
            // exitMenu
            // 
            this.exitMenu.Name = "exitMenu";
            this.exitMenu.Size = new System.Drawing.Size(92, 22);
            this.exitMenu.Text = "&Exit";
            this.exitMenu.Click += new System.EventHandler(this.exitMenu_Click);
            // 
            // refreshMenu
            // 
            this.refreshMenu.Name = "refreshMenu";
            this.refreshMenu.Size = new System.Drawing.Size(58, 20);
            this.refreshMenu.Text = "&Refresh";
            this.refreshMenu.Click += new System.EventHandler(this.refresh_Click);
            // 
            // connectMenu
            // 
            this.connectMenu.Name = "connectMenu";
            this.connectMenu.Size = new System.Drawing.Size(64, 20);
            this.connectMenu.Text = "&Сonnect";
            this.connectMenu.Click += new System.EventHandler(this.connectMenu_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusTextStrip});
            this.statusStrip.Location = new System.Drawing.Point(0, 381);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(976, 22);
            this.statusStrip.TabIndex = 0;
            // 
            // statusTextStrip
            // 
            this.statusTextStrip.Name = "statusTextStrip";
            this.statusTextStrip.Size = new System.Drawing.Size(16, 17);
            this.statusTextStrip.Text = "...";
            // 
            // peerDataBox
            // 
            this.peerDataBox.BackColor = System.Drawing.SystemColors.Info;
            this.peerDataBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.peerDataBox.Location = new System.Drawing.Point(312, 27);
            this.peerDataBox.Name = "peerDataBox";
            this.peerDataBox.Size = new System.Drawing.Size(664, 351);
            this.peerDataBox.TabIndex = 4;
            this.peerDataBox.Text = "";
            // 
            // disconnectMenu
            // 
            this.disconnectMenu.Name = "disconnectMenu";
            this.disconnectMenu.Size = new System.Drawing.Size(70, 20);
            this.disconnectMenu.Text = "&Disconnect";
            this.disconnectMenu.Click += new System.EventHandler(this.disconnectMenu_Click);
            // 
            // timerUpdateData
            // 
            this.timerUpdateData.Enabled = true;
            this.timerUpdateData.Interval = 1000;
            this.timerUpdateData.Tick += new System.EventHandler(this.timerUpdateData_Tick);
            // 
            // timerSetData
            // 
            this.timerSetData.Enabled = false;
            this.timerSetData.Interval = 1000;
            this.timerSetData.Tick += new System.EventHandler(this.peerList_SelectedIndexChanged);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 403);
            this.Controls.Add(this.peerDataBox);
            this.Controls.Add(this.peerList);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "Home";
            this.ShowIcon = false;
            this.Text = "GraduateWork";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Home_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.eventLog)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private ToolStripStatusLabel statusTextStrip;
        private ToolStripMenuItem disconnectMenu;
        private ToolStripMenuItem connectMenu;
        private ToolStripMenuItem refreshMenu;
        private ToolStripMenuItem fileMenu;
        private ToolStripMenuItem exitMenu;
        private RichTextBox peerDataBox;
        private StatusStrip statusStrip;
        private Timer timerUpdateData;
        private MenuStrip menuStrip;
        private EventLog eventLog;
        private ListBox peerList;

        //test
        private Timer timerSetData;
    }
}