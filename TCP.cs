using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Windows.Forms;

namespace TCP
{
    class Server
    {
        private TcpListener tcpListener;
        private Thread listenThread;
        
        public Server()
        {
            this.tcpListener = new TcpListener(IPAddress.Any, 10152);
            this.listenThread = new Thread(new ThreadStart(ListenForClients));
            this.listenThread.Start();
        }

        private void ListenForClients()
        {
            this.tcpListener.Start();

            while (true)
            {
                //blocks until a client has connected to the server
                TcpClient client = this.tcpListener.AcceptTcpClient();

                //create a thread to handle communication 
                //with connected client
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(client);
            }
        }

        public event MessageEventHandler NewMessage;

        protected virtual void OnMessage(MessageEventArgs e)
        {
            MessageEventHandler handler = NewMessage;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        private void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();

            byte[] netdata = new byte[4096];
            int bytesRead;
            string message = "";

            while (true)
            {
                bytesRead = 0;

                try
                {
                    //blocks until a client sends a message
                    bytesRead = clientStream.Read(netdata, 0, 4096);
                }
                catch
                {
                    //a socket error has occured
                    break;
                }

                if (bytesRead == 0)
                {
                    //the client has disconnected from the server
                    break;
                }
                //message has successfully been received
                ASCIIEncoding encoder = new ASCIIEncoding();
                message += encoder.GetString(netdata, 0, bytesRead);
            }
            tcpClient.Close();

            if (message.Length > 0)
            {
                int Severity = 1;
                MessageEventArgs e = new MessageEventArgs(message, Severity, true);
                OnMessage(e);
            }
        }
    }

    public class MessageEventArgs : EventArgs
    {
        private String Message;
        private Int32 Severity;
        private bool Speak;

        public MessageEventArgs(String Message, Int32 Severity, bool Speak)
        {
            this.Message = Message;
            this.Severity = Severity;
            this.Speak = Speak;
        }

        public String GetMessage
        {
            get { return Message; }
        }

        public Int32 GetSeverity
        {
            get { return Severity; }
        }

        public bool IsSpeak
        {
            get { return Speak; }
        }
    }

    public delegate void MessageEventHandler(object sender, MessageEventArgs e);
}
