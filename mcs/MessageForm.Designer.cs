namespace mcs
{
    partial class MessageForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageForm));
            this.TitleLabel = new CCWin.SkinControl.SkinLabel();
            this.Close = new CCWin.SkinControl.SkinPictureBox();
            this.Button1 = new CCWin.SkinControl.SkinButton();
            this.Button2 = new CCWin.SkinControl.SkinButton();
            this.TextLabel = new CCWin.SkinControl.SkinLabel();
            ((System.ComponentModel.ISupportInitialize)(this.Close)).BeginInit();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.TitleLabel.BorderColor = System.Drawing.Color.White;
            this.TitleLabel.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.TitleLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TitleLabel.ForeColor = System.Drawing.SystemColors.Window;
            this.TitleLabel.Location = new System.Drawing.Point(7, 7);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(48, 17);
            this.TitleLabel.TabIndex = 39;
            this.TitleLabel.Text = "提示 ：";
            // 
            // Close
            // 
            this.Close.BackColor = System.Drawing.Color.Transparent;
            this.Close.Image = ((System.Drawing.Image)(resources.GetObject("Close.Image")));
            this.Close.Location = new System.Drawing.Point(226, -1);
            this.Close.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(31, 31);
            this.Close.TabIndex = 40;
            this.Close.TabStop = false;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            this.Close.MouseLeave += new System.EventHandler(this.Close_MouseLeave);
            this.Close.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Close_MouseMove);
            // 
            // Button1
            // 
            this.Button1.BackColor = System.Drawing.Color.Transparent;
            this.Button1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.Button1.DownBack = null;
            this.Button1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button1.ForeColor = System.Drawing.Color.Snow;
            this.Button1.GlowColor = System.Drawing.Color.Transparent;
            this.Button1.IsDrawGlass = false;
            this.Button1.Location = new System.Drawing.Point(192, 89);
            this.Button1.MouseBack = ((System.Drawing.Image)(resources.GetObject("Button1.MouseBack")));
            this.Button1.Name = "Button1";
            this.Button1.NormlBack = global::mcs.Properties.Resources.buttonback;
            this.Button1.Size = new System.Drawing.Size(60, 25);
            this.Button1.TabIndex = 49;
            this.Button1.Text = "保存";
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Button2
            // 
            this.Button2.BackColor = System.Drawing.Color.Transparent;
            this.Button2.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.Button2.DownBack = null;
            this.Button2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button2.ForeColor = System.Drawing.Color.Snow;
            this.Button2.GlowColor = System.Drawing.Color.Transparent;
            this.Button2.IsDrawGlass = false;
            this.Button2.Location = new System.Drawing.Point(126, 89);
            this.Button2.MouseBack = ((System.Drawing.Image)(resources.GetObject("Button2.MouseBack")));
            this.Button2.Name = "Button2";
            this.Button2.NormlBack = global::mcs.Properties.Resources.buttonback;
            this.Button2.Size = new System.Drawing.Size(60, 25);
            this.Button2.TabIndex = 50;
            this.Button2.Text = "保存";
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // TextLabel
            // 
            this.TextLabel.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.TextLabel.BackColor = System.Drawing.Color.Transparent;
            this.TextLabel.BorderColor = System.Drawing.Color.White;
            this.TextLabel.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.TextLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TextLabel.ForeColor = System.Drawing.Color.Black;
            this.TextLabel.Location = new System.Drawing.Point(7, 34);
            this.TextLabel.Name = "TextLabel";
            this.TextLabel.Size = new System.Drawing.Size(245, 45);
            this.TextLabel.TabIndex = 51;
            this.TextLabel.Text = "Text";
            this.TextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::mcs.Properties.Resources.back;
            this.CanResize = false;
            this.ClientSize = new System.Drawing.Size(258, 122);
            this.ControlBox = false;
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.TextLabel);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.TitleLabel);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageForm";
            this.RoundStyle = CCWin.SkinClass.RoundStyle.None;
            this.ShowDrawIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "";
            ((System.ComponentModel.ISupportInitialize)(this.Close)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinLabel TitleLabel;
        private CCWin.SkinControl.SkinPictureBox Close;
        private CCWin.SkinControl.SkinButton Button1;
        private CCWin.SkinControl.SkinButton Button2;
        private CCWin.SkinControl.SkinLabel TextLabel;
    }
}