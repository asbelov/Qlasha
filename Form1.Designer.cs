namespace WindowsFormsQlasha
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.linkLabel = new System.Windows.Forms.LinkLabel();
            this.Body = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuVoiceSelector = new System.Windows.Forms.ToolStripMenuItem();
            this.noVoicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.volumeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noVolumeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noRateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lastMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.brainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Body)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.linkLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Body, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(676, 535);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // linkLabel
            // 
            this.linkLabel.AutoSize = true;
            this.linkLabel.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLabel.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabel.Location = new System.Drawing.Point(3, 0);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.Size = new System.Drawing.Size(670, 40);
            this.linkLabel.TabIndex = 0;
            this.linkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel.UseMnemonic = false;
            // 
            // Body
            // 
            this.Body.BackColor = System.Drawing.Color.Transparent;
            this.Body.ContextMenuStrip = this.contextMenuStrip;
            this.Body.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Body.Image = ((System.Drawing.Image)(resources.GetObject("Body.Image")));
            this.Body.InitialImage = null;
            this.Body.Location = new System.Drawing.Point(3, 43);
            this.Body.Name = "Body";
            this.Body.Size = new System.Drawing.Size(670, 490);
            this.Body.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Body.TabIndex = 1;
            this.Body.TabStop = false;
            this.Body.Click += new System.EventHandler(this.Body_Click);
            this.Body.DoubleClick += new System.EventHandler(this.Body_DoubleClick);
            this.Body.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Body_MouseDown);
            this.Body.MouseEnter += new System.EventHandler(this.Body_MouseEnter);
            this.Body.MouseLeave += new System.EventHandler(this.Body_MouseLeave);
            this.Body.MouseHover += new System.EventHandler(this.Body_MouseHover);
            this.Body.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Body_MouseMove);
            this.Body.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Body_MouseUp);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuVoiceSelector,
            this.volumeToolStripMenuItem,
            this.rateToolStripMenuItem,
            this.lastMessageToolStripMenuItem,
            this.brainToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(153, 164);
            this.contextMenuStrip.Text = "Настройка";
            // 
            // toolStripMenuVoiceSelector
            // 
            this.toolStripMenuVoiceSelector.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noVoicesToolStripMenuItem});
            this.toolStripMenuVoiceSelector.Name = "toolStripMenuVoiceSelector";
            this.toolStripMenuVoiceSelector.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuVoiceSelector.Text = "Voice";
            this.toolStripMenuVoiceSelector.DropDownOpened += new System.EventHandler(this.toolStripMenuVoiceSelector_DropDownOpened);
            // 
            // noVoicesToolStripMenuItem
            // 
            this.noVoicesToolStripMenuItem.Name = "noVoicesToolStripMenuItem";
            this.noVoicesToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.noVoicesToolStripMenuItem.Text = "NoVoices";
            // 
            // volumeToolStripMenuItem
            // 
            this.volumeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noVolumeToolStripMenuItem});
            this.volumeToolStripMenuItem.Name = "volumeToolStripMenuItem";
            this.volumeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.volumeToolStripMenuItem.Text = "Volume";
            this.volumeToolStripMenuItem.DropDownOpened += new System.EventHandler(this.volumeToolStripMenuItem_DropDownOpened);
            // 
            // noVolumeToolStripMenuItem
            // 
            this.noVolumeToolStripMenuItem.Name = "noVolumeToolStripMenuItem";
            this.noVolumeToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.noVolumeToolStripMenuItem.Text = "NoVolume";
            // 
            // rateToolStripMenuItem
            // 
            this.rateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noRateToolStripMenuItem});
            this.rateToolStripMenuItem.Name = "rateToolStripMenuItem";
            this.rateToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rateToolStripMenuItem.Text = "Rate";
            this.rateToolStripMenuItem.DropDownOpened += new System.EventHandler(this.rateToolStripMenuItem_DropDownOpened);
            // 
            // noRateToolStripMenuItem
            // 
            this.noRateToolStripMenuItem.Name = "noRateToolStripMenuItem";
            this.noRateToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.noRateToolStripMenuItem.Text = "NoRate";
            // 
            // lastMessageToolStripMenuItem
            // 
            this.lastMessageToolStripMenuItem.Name = "lastMessageToolStripMenuItem";
            this.lastMessageToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.lastMessageToolStripMenuItem.Text = "LastMessage";
            this.lastMessageToolStripMenuItem.Click += new System.EventHandler(this.lastMessageToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 10000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // brainToolStripMenuItem
            // 
            this.brainToolStripMenuItem.Name = "brainToolStripMenuItem";
            this.brainToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.brainToolStripMenuItem.Text = "GoToMyBrain";
            this.brainToolStripMenuItem.Click += new System.EventHandler(this.brainToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(676, 535);
            this.Controls.Add(this.tableLayoutPanel1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(80, 80);
            this.Name = "Form1";
            this.Opacity = 0.6D;
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Body)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.LinkLabel linkLabel;
        private System.Windows.Forms.PictureBox Body;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuVoiceSelector;
        private System.Windows.Forms.ToolStripMenuItem volumeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lastMessageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noVoicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noVolumeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noRateToolStripMenuItem;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem brainToolStripMenuItem;


    }
}

