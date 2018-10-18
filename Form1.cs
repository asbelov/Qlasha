using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Qlasha;

namespace WindowsFormsQlasha
{
    public partial class Form1 : Form
    {
        private Point MouseLocation, BodyLocation;
        private Memory QlashaMemory;
        private Mouth QlashaMouth;
        private HTTP ListeNet;

        private DateTime LastMouseHoverTime;
        private DateTime LastMouseMoveTime;
        private DateTime LastMouseDownTime;
        private DateTime LastMouseUpTime;

        delegate void SetTextCallback(String message, Int32 Severity);

        public Form1()
        {
            InitializeComponent();

            
            this.toolStripMenuVoiceSelector.Text = Qlasha.Properties.Resources.MenuStrVoice;
            this.volumeToolStripMenuItem.Text = Qlasha.Properties.Resources.MenuStrVolume;
            this.rateToolStripMenuItem.Text = Qlasha.Properties.Resources.MenuStrRate;
            this.lastMessageToolStripMenuItem.Text = Qlasha.Properties.Resources.MenuStrLastMessage;
            this.exitToolStripMenuItem.Text = Qlasha.Properties.Resources.MenuStrExit;
            this.brainToolStripMenuItem.Text = Qlasha.Properties.Resources.MenuStrBrowser;
            try { this.Opacity = Convert.ToDouble(Qlasha.Properties.Resources.CfgOpacityTransparent); }
            catch { };

            LastMouseHoverTime = DateTime.Now;
            LastMouseMoveTime = DateTime.Now;
            LastMouseDownTime = DateTime.Now;
            LastMouseUpTime = DateTime.Now;

            QlashaMemory = new Memory();
            QlashaMouth = new Mouth(QlashaMemory);
            ListeNet = new HTTP(Qlasha.Properties.Resources.CfgHTTPProtocol,
                Qlasha.Properties.Resources.CfgHTTPHosts.Split(new string[] { Qlasha.Properties.Resources.CfgSeparator }, System.StringSplitOptions.RemoveEmptyEntries), 
                Qlasha.Properties.Resources.CfgHTTPPort,
                Qlasha.Properties.Resources.StrErrorInitHTTP,
                QlashaMouth, QlashaMemory);
            ListeNet.NewMessage += new MessageEventHandler(ListeNet_NewMessage);

        }

        private void Stop()
        {
            ListeNet.Stop();
            QlashaMouth.Stop();
            Application.Exit();
        }
        
        private void CreateVoiceContextMenu()
        {
            Int32 SysVoiceCnt = QlashaMouth.GetInstalledVoicesCount;

            ToolStripMenuItem[] VoiceMenu = new ToolStripMenuItem[SysVoiceCnt];
            String CurVoice = QlashaMouth.GetVoice;

            for (Int32 i = 0; i < SysVoiceCnt; i++)
            {
                String Voice = "   " + QlashaMouth.GetInstalledVoicesDescription[i];
                if(CurVoice == QlashaMouth.GetInstalledVoicesNames[i]) Voice = "• " + QlashaMouth.GetInstalledVoicesDescription[i];
                VoiceMenu[i] = new ToolStripMenuItem(Voice, null, new EventHandler(VoiceToolStripMenuItem_Click));
                this.toolStripMenuVoiceSelector.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { VoiceMenu[i] });
            }
        }

        private void CreateVolumeContextMenu()
        {
            ToolStripMenuItem[] VolumeMenu = new ToolStripMenuItem[11];
            Int32 CurVolume = QlashaMouth.GetVolume / 10;

            for (Int32 i = 0; i < 11; i++)
            {
                String MenuItemName = "   " + Convert.ToString(i * 10);
                if (i == CurVolume) MenuItemName = "• " + Convert.ToString(i * 10);
                VolumeMenu[i] = new ToolStripMenuItem(MenuItemName, null, new EventHandler(VolumeToolStripMenuItem_Click));
                this.volumeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { VolumeMenu[i] });
            }
        }

        private void CreateRateContextMenu()
        {
            ToolStripMenuItem[] RateMenu = new ToolStripMenuItem[21];
            Int32 CurRate = QlashaMouth.GetRate;

            for (int i = -10; i < 10; i++)
            {
                String MenuItemName = "   " + i.ToString();
                if (i == CurRate) MenuItemName = "• " + i.ToString();
                RateMenu[i+10] = new ToolStripMenuItem(MenuItemName, null, new EventHandler(RateToolStripMenuItem_Click));
                this.rateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { RateMenu[i+10] });
            }
        }

        private void SetText(String Message, Int32 Severity)
        {
            if (this.linkLabel.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { Message, Severity });
            }
            else
            {
                if (Message.Length == 0) return;
                QlashaMemory.ToRemember(Message, Severity);
                if (QlashaMemory.PeekNextMessageSeverity() > 0) this.linkLabel.Text = "";
                QlashaMouth.Speak();
            }
        }

        private void SpeekLastMessage()
        {
            if (QlashaMemory.GetLastMessage.Length == 0) return;
            this.linkLabel.Text = QlashaMemory.GetLastMessage;
            QlashaMemory.ToRemember(String.Format(Qlasha.Properties.Resources.StrLastMessage, QlashaMemory.GetLastMessage), -1);
        }

        void ListeNet_NewMessage(object sender, MessageEventArgs e)
        {
            SetText(e.GetMessage, e.GetSeverity);
        }

        private void Body_Click(object sender, EventArgs e)
        {
            MoveSomwere();
        }

         private void Body_MouseEnter(object sender, EventArgs e)
         {
             try { this.Opacity = Convert.ToDouble(Qlasha.Properties.Resources.CfgOpacityNoTransparent); }
             catch { };
         }

         private void Body_MouseLeave(object sender, EventArgs e)
         {
             try { this.Opacity = Convert.ToDouble(Qlasha.Properties.Resources.CfgOpacityTransparent); }
             catch { };
         }

         private void Body_MouseDown(object sender, MouseEventArgs e)
         {
             MouseLocation = e.Location;
             if (LastMouseDownTime.AddSeconds(3) < DateTime.Now)
             {
                 this.SetText(Qlasha.Properties.Resources.StrMouseDown, -1);
                 LastMouseDownTime = DateTime.Now;
             }
             MoveSomwere();
         }

         private void Body_MouseUp(object sender, MouseEventArgs e)
         {
             if (LastMouseUpTime.AddSeconds(10) < DateTime.Now)
             {
                 this.SetText(Qlasha.Properties.Resources.StrMouseUp, -1);
                 LastMouseUpTime = DateTime.Now;
             }
         }

         private void Body_MouseMove(object sender, MouseEventArgs e)
         {
             if(e.Button == MouseButtons.Left)
             {
                 if (LastMouseMoveTime.AddSeconds(5) < DateTime.Now)
                 {
                     this.SetText(Qlasha.Properties.Resources.StrMouseMove, -1);
                     LastMouseMoveTime = DateTime.Now;
                 }
                 BodyLocation.X = this.Location.X + e.Location.X - MouseLocation.X;
                 BodyLocation.Y = this.Location.Y + e.Location.Y - MouseLocation.Y;
                 this.Location = BodyLocation;
             }
         }

         private void Body_MouseHover(object sender, EventArgs e)
         {
             if (LastMouseHoverTime.AddSeconds(3) < DateTime.Now)
             {
                 this.SetText(Qlasha.Properties.Resources.StrMouseHover, -1);
                 LastMouseHoverTime = DateTime.Now;
                 MoveSomwere();
             }
         }

         private void Body_DoubleClick(object sender, EventArgs e)
         {
             SpeekLastMessage();
             try { this.Opacity = Convert.ToDouble(Qlasha.Properties.Resources.CfgOpacityTransparent); }
             catch { }
         }

         private void Form1_FormClosing(object sender, FormClosingEventArgs e)
         {
             this.Stop();
         }

        private void VoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem) sender;
            String VoiceName = Regex.Replace(item.Text, @"^.*\]\s", "");
            if (QlashaMouth.SetVoice(VoiceName))
            {
                this.SetText(String.Format(Qlasha.Properties.Resources.StrVoiceChanded, VoiceName), 9);
            }
        }
        private void VolumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            QlashaMouth.SetVolume(item.Text);
            this.SetText(String.Format(Qlasha.Properties.Resources.StrVolumeChanged, item.Text), 9);
        }

        private void RateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            QlashaMouth.SetRate(item.Text);
            this.SetText(String.Format(Qlasha.Properties.Resources.StrRateChanged, item.Text), 9);
        }

        private void lastMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpeekLastMessage();
        }

        private void volumeToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            this.volumeToolStripMenuItem.DropDownItems.Clear();
            CreateVolumeContextMenu();
        }

        private void rateToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            this.rateToolStripMenuItem.DropDownItems.Clear();
            CreateRateContextMenu();
        }

        private void toolStripMenuVoiceSelector_DropDownOpened(object sender, EventArgs e)
        {
            this.toolStripMenuVoiceSelector.DropDownItems.Clear();
            CreateVoiceContextMenu();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Stop();
        }

        private bool MoveLeft(UInt16 dist)
        {
            Point NewLocation = this.Location;
            NewLocation.X -= dist;
            if (NewLocation.X < SystemInformation.VirtualScreen.Location.X) return false;
            this.Location = NewLocation;
            return true;
        }

        private bool MoveRight(UInt16 dist)
        {
            Point NewLocation = this.Location;
            NewLocation.X += dist;
            if (NewLocation.X > SystemInformation.VirtualScreen.Width - SystemInformation.VirtualScreen.Location.X - this.Size.Width) return false;
            this.Location = NewLocation;
            return true;
        }

        private bool MoveDown(UInt16 dist)
        {
            Point NewLocation = this.Location;
            NewLocation.Y += dist;
            if (NewLocation.Y > SystemInformation.VirtualScreen.Height - SystemInformation.VirtualScreen.Location.Y - this.Size.Height) return false;
            this.Location = NewLocation;
            return true;
        }
        
        private bool MoveUp(UInt16 dist)
        {
            Point NewLocation = this.Location;
            NewLocation.Y -= dist;
            if (NewLocation.Y < SystemInformation.VirtualScreen.Location.Y) return false;
            this.Location = NewLocation;
            return true;
        }

        private void MoveSomwere()
        {
            Random rnd = new Random();
            bool moved = false;
            UInt16 dist;

            try { dist = Convert.ToUInt16(rnd.Next(30, 100)); }
            catch { dist = 10; }

            while (!moved)
            {
                switch (rnd.Next(0,5))
                {
                    case 1:
                        moved = this.MoveLeft(dist);
                        break;
                    case 2:
                        moved = this.MoveRight(dist);
                        break;
                    case 3:
                        moved = this.MoveDown(dist);
                        break;
                    case 4:
                        moved = this.MoveUp(dist);
                        break;
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Random rnd = new Random();
            Int32 Volume = QlashaMouth.GetVolume;
            QlashaMouth.SetVolume(rnd.Next(20, 100));

            this.SetText(Qlasha.Properties.Resources.StrOnTimer, -1);
            this.MoveSomwere();
            this.timer.Interval = rnd.Next(10, 60) * 1000;
            System.Threading.Thread.Sleep(20);
            QlashaMouth.SetVolume(Volume);
        }

        private void brainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { System.Diagnostics.Process.Start(ListeNet.GetPrefix); }
            catch { };
        }
    }
}
