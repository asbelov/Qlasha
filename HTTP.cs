using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Specialized;
using System.Collections;
using System.Resources;
using System.Configuration;
using System.Web;
using System.Web.Configuration;

namespace Qlasha
{

    class HTTP
    {
        private HttpListener listener;
        private Thread listenThread;
        private Mouth MyMouth;
        private Memory MyMemory;
        public event MessageEventHandler NewMessage;
        private String Prefix;


        public HTTP(String prot, String[] hosts, String port, String ErrorMessage, Mouth MyMouth, Memory MyMemory)
        {
            this.listener = new HttpListener();
            this.MyMouth = MyMouth;
            this.MyMemory = MyMemory;

            try { Convert.ToUInt16(port); }
            catch { port = "10160"; }

            if (!prot.Equals("http") && !prot.Equals("https")) prot = "http";

            this.Prefix = prot + "://localhost:" + port + '/';

            this.listener.Prefixes.Add(Prefix);

            foreach (String Host in hosts)
            {
                try
                {
                    if (Host != "localhost") this.listener.Prefixes.Add(prot + "://" + Host + ':' + port + '/');
                }
                catch { };
            }
            try { this.listener.Start(); }
            catch (Exception ex) 
            {
                MessageEventArgs e = new MessageEventArgs(String.Format(ErrorMessage, ex.Message), 0);
                OnMessage(e);
                this.listener = new HttpListener();
                this.listener.Prefixes.Add(prot + "://localhost:" + port + '/');

                try { this.listener.Start(); }
                catch (Exception ex1) 
                {
                    MessageEventArgs e1 = new MessageEventArgs(String.Format(ErrorMessage, ex1.Message), 0);
                    OnMessage(e1);
                    return;
                }
            }

            this.listenThread = new Thread(new ThreadStart(ListenForClient));
            this.listenThread.Start();
        }

        public void Stop()
        {
            listener.Abort();
        }

        public String GetPrefix
        {
            get { return Prefix; }
        }

        private void ListenForClient()
        {

            IAsyncResult result;
            while (true)
            {
                try { result = listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener); }
                catch { break; }
                result.AsyncWaitHandle.WaitOne();
            }
        }

        public void ListenerCallback(IAsyncResult result)
        {
            HttpListener listener = (HttpListener)result.AsyncState;
            // Call EndGetContext to complete the asynchronous operation.
            HttpListenerContext context = listener.EndGetContext(result);
            HttpListenerRequest request = context.Request;

            // Obtain a response object.
            HttpListenerResponse response = context.Response;

            String responseString = ParseQuery(request);
            
            byte[] buffer = request.ContentEncoding.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
        }

        protected virtual void OnMessage(MessageEventArgs e)
        {
            MessageEventHandler handler = NewMessage;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private String ParseQuery(HttpListenerRequest request)
        {
            String Rate;
            String Volume;
            Int32 Severity;
            String Message;
            NameValueCollection Params = request.QueryString;
            String CodePage;

            if ((CodePage = Params[Properties.Resources.CfgURLQueryCharset]) == null)
            {
                if ((CodePage = request.Headers["Accept-Charset"]) == null)
                    CodePage = request.ContentEncoding.WebName;
            }

            foreach (String Key in Params.AllKeys)
            {
                byte[] sourceBytes =request.ContentEncoding.GetBytes(Params[Key]);
                
                try { Params[Key] = Encoding.GetEncoding(CodePage).GetString(sourceBytes); }
                catch { }
            }

            if ((Rate = Params[Properties.Resources.CfgURLRate]) != null)
                MyMouth.SetRate(Rate);
            else Rate = MyMouth.GetRate.ToString();
            if ((Volume = Params[Properties.Resources.CfgURLVolume]) != null)
                MyMouth.SetVolume(Volume);
            else Volume = MyMouth.GetVolume.ToString();

            try { Severity = Convert.ToUInt16(Params[Properties.Resources.CfgURLSeverity]); }
            catch { Severity = 5; };

            Message = Params[Properties.Resources.CfgURLMessage];
            if (Params.HasKeys())
            {
                if (Message == null || Message.Length == 0)
                { Message = Params[0]; }

                if (Message.Length > 0)
                {                    
                    MessageEventArgs e = new MessageEventArgs(Message, Severity);
                    OnMessage(e);
                }
                if (Params[Properties.Resources.CfgURLQuiet] != null) return Properties.Resources.CfgHTMLEmpty;
            }
            Message = Properties.Resources.CfgHTMLFull;

            foreach (String Key in Params.AllKeys)
            {
                Message = Message.Replace('%' + Key + '%', Params[Key]);
            }

            Params.Add(Properties.Resources.CfgURLRate, MyMouth.GetRate.ToString());
            Params.Add(Properties.Resources.CfgURLVolume, MyMouth.GetVolume.ToString());
            Params.Add(Properties.Resources.CfgURLVoice, MyMouth.GetVoice);
            Params.Add(Properties.Resources.CfgURLMessage, "");
            Params.Add(Properties.Resources.CfgURLSeverity, "");
            Params.Add(Properties.Resources.CfgURLEncoding, request.ContentEncoding.WebName);
            Params.Add(Properties.Resources.CfgURLLastMessage, MyMemory.GetLastMessage);
            String Prefixes = "";

            foreach (String prefix in listener.Prefixes)
            {
                Prefixes = prefix + Properties.Resources.CfgSeparator + Prefixes;
            }

            Params.Add(Properties.Resources.CfgURLPrefix, this.Prefix);
            Params.Add(Properties.Resources.CfgURLPrefixes, Prefixes);

            foreach (String Key in Params.AllKeys)
            {
                Message = Message.Replace('%' + Key + '%', Params[Key]);
            }

            return Message;
        }

    }

    public delegate void MessageEventHandler(object sender, MessageEventArgs e);

    public class MessageEventArgs : EventArgs
    {
        private String Message;
        private Int32 Severity;

        public MessageEventArgs(String Message, Int32 Severity)
        {
            this.Message = Message;
            this.Severity = Severity;
        }

        public String GetMessage
        {
            get { return Message; }
        }

        public Int32 GetSeverity
        {
            get { return Severity; }
        }
    }

}
