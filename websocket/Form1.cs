using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocket4Net;

namespace websocket
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        WebSocket webSocket; //переменная для общения по Websocket
        string _url = string.Empty; //строка соединения
        public bool isConnected = false;

        void websocket_Opened(object sender, EventArgs e)
        {
            MessageBox.Show("Websocket is opened.");
            this.isConnected = true;
        }

        void websocket_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);

            if (this.webSocket.State != WebSocketState.Open)
            {
                this.isConnected = false;
            }

            if (!this.isConnected)
            {
                webConnect();
            }
        }

        void websocket_Closed(object sender, EventArgs e)
        {
            MessageBox.Show("Websocket is closed.");
            webConnect();
        }

        void websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            MessageBox.Show("Message received: " + e.Message);
        }

        public void webConnect()//соединение с сервером по WebSocket
        {
            #region Соединение с сервером по WebSocket

            try
            {
                if (_url == string.Empty)
                {
                    _url = "wss://"; //строка запроса
                }

                if (webSocket == null)
                {
                    webSocket = new WebSocket(_url);
                    webSocket.Opened += new EventHandler(websocket_Opened);
                    webSocket.Closed += new EventHandler(websocket_Closed);
                    webSocket.Error += new EventHandler<SuperSocket.ClientEngine.ErrorEventArgs>(websocket_Error);
                    webSocket.MessageReceived += new EventHandler<MessageReceivedEventArgs>(websocket_MessageReceived);
                }


                if (!isConnected && webSocket.State != WebSocketState.Open)
                {
                    webSocket.Open();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ошибка websocket: " + ex.ToString());
            }

            #endregion
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webConnect();//соединение с сервером по WebSocket
        }
    }


}
