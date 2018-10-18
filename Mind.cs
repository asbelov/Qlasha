using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Qlasha
{
    class Memory
    {
        private SortedSet<String> Crinkle;
        private Int32 CrinkleCount;
        private Int32 ForgetMessages;
        private String LastMessage;

        public Memory()
        {
            Init();
            try { this.CrinkleCount = Convert.ToInt32(Properties.Resources.CfgCrinkleCount); }
            catch { this.CrinkleCount = 5; }
        }

        private void Init()
        {
            this.Crinkle = new SortedSet<String>();
            this.ForgetMessages = 0;
            this.LastMessage = "";
        }

        private String PrepareMessage(String garbage)
        {
            String[] Messages = garbage.Split(new String[] {Properties.Resources.CfgSeparator }, System.StringSplitOptions.None);
            if (Messages.Length == 0) return "";
            String Message;
            if (Messages.Length > 1)
            {
                Random rnd = new Random();
                Message = Messages[rnd.Next(0, Messages.Length)];
            }
            else Message = Messages[0];

            if (Message == null) return "";
            Message = Regex.Replace(Message, Properties.Resources.CfgRegExpNormalizeMessage, " ");
            Message = Regex.Replace(Message, @"\s+", " ");
            Message.Trim();

            return Message;
        }
        
        public void ToRemember(String Message, Int32 Severity)
        {
            Message = this.PrepareMessage(Message);
            if (Message == "") return;

            if (this.Crinkle.Count > 0) Message = this.PrepareMessage(Properties.Resources.StrMessageConnectors) + Message;

            String Element = Convert.ToString(Severity+1) + '.' + Message;

            if(this.Crinkle.Contains(Element)) return;

            this.Crinkle.Add(Element);
            if (this.Crinkle.Count() >= this.CrinkleCount)
            {
                this.Crinkle.Remove(this.Crinkle.First());
                ++this.ForgetMessages;
            }
        }
        
        public void ToRemember(String Message, String SSeverity)
        {
            Int32 Severity;
            try { Severity = Convert.ToInt32(SSeverity); }
            catch { return; }

            ToRemember(Message, Severity);
        }

        public String Remember()
        {
            if (this.Crinkle.Count() == 0)
            {
                if (this.ForgetMessages > 0)
                {
                    Int32 n = this.ForgetMessages;
                    this.ForgetMessages = 0;
                    return this.PrepareMessage(String.Format(Properties.Resources.StrForgetNMessages, n));
                }
                else return this.PrepareMessage(Properties.Resources.StrEmpty);
            }
            String Message = this.Crinkle.Last();
            bool SkeepLastMessage = false;
            this.Crinkle.Remove(Message);
            if (Message[0] == '0') SkeepLastMessage = true;
            Message = Message.Remove(0, Message.IndexOf('.') + 1);
            if(!SkeepLastMessage) LastMessage = Message;
            return Message;
        }

        public String GetLastMessage
        {
            get { return LastMessage; }
        }

        public String PeekNextMessage()
        {
            String Message;
            try { Message = this.Crinkle.Last(); }
            catch { return ""; }

            return Message.Remove(0, Message.IndexOf('.') + 1);
        }

        public Int32 PeekNextMessageSeverity()
        {
            String Message;
            try { Message = this.Crinkle.Last(); }
            catch { return 0; }

            Int32 Severity;
            try { Severity = Convert.ToInt32(Message.Remove(Message.IndexOf('.'))); }
            catch { Severity = 0; };

            return Severity;
        }
    
    }

    class Mood
    {
        private Int32 mood;

        public Mood()
        {
            mood = 0;
        }

        public Int32 MoodChanger(Int32 Count)
        {
            this.mood += Count;
            return this.mood;
        }

        public Int32 GetMood
        {
            get { return this.mood; }
        }
    }

    class Actions
    {
        public Actions()
        {

        }
    }
}
