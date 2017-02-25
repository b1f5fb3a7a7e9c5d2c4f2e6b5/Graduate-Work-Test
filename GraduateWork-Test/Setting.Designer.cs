using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace GraduateWork_Test
{
    partial class Setting
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
            this.Ok = new Button();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.Cancel = new Button();
            this.ipAddress = new TextBox();
            this.сloudList = new ListBox();
            this.port = new NumericUpDown();
            ((ISupportInitialize)(this.port)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Address";
            // 
            // ipAddress
            // 
            this.ipAddress.Enabled = false;
            this.ipAddress.Location = new Point(77, 10);
            this.ipAddress.Name = "ipAddress";
            this.ipAddress.Size = new Size(177, 20);
            this.ipAddress.TabIndex = 1;
            // 
            // port
            // 
            this.port.Location = new Point(276, 10);
            this.port.Maximum = new decimal(new int[] {4599,0,0,0});
            this.port.Minimum = new decimal(new int[] {4590,0,0,0});
            this.port.Name = "port";
            this.port.Size = new Size(71, 20);
            this.port.TabIndex = 2;
            this.port.Value = new decimal(new int[] {4590,0,0,0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new Point(260, 13);
            this.label2.Name = "label2";
            this.label2.Size = new Size(10, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = ":";
            // 
            // сloudList
            // 
            this.сloudList.BorderStyle = BorderStyle.None;
            this.сloudList.FormattingEnabled = true;
            this.сloudList.Location = new Point(16, 59);
            this.сloudList.Name = "сloudList";
            this.сloudList.Size = new Size(331, 117);
            this.сloudList.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new Point(13, 43);
            this.label3.Name = "label3";
            this.label3.Size = new Size(214, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Коллекция облаков одноранговых узлов";
            // 
            // Ok
            // 
            this.Ok.DialogResult = DialogResult.OK;
            this.Ok.Location = new Point(165, 349);
            this.Ok.Name = "Ok";
            this.Ok.Size = new Size(89, 23);
            this.Ok.TabIndex = 6;
            this.Ok.Text = "Подтвердить";
            this.Ok.UseVisualStyleBackColor = true;
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = DialogResult.Cancel;
            this.Cancel.Location = new Point(265, 349);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new Size(89, 23);
            this.Cancel.TabIndex = 7;
            this.Cancel.Text = "Отменить";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new Size(366, 384);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Ok);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.сloudList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.port);
            this.Controls.Add(this.ipAddress);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Setting";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Setting";
            ((ISupportInitialize)(this.port)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private NumericUpDown port;
        private ListBox сloudList;
        private TextBox ipAddress;
        private Button Cancel;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button Ok;
    }
}