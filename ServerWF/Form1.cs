using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace ServerWF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            size();
        }
        Socket s = null;
        private  void button1_Click(object sender, EventArgs e)
        {           
            Task.Run(() => { Lisener(); });                  
        }
       string text = string.Empty;
        string textTmp = string.Empty;
       
        public void Lisener()
        {
            Text = "Сервер запущен";
            try
            {
                s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                IPAddress ip = IPAddress.Parse("127.0.0.1");
                IPEndPoint ep = new IPEndPoint(ip, 7777);
                s.Bind(ep);
                s.Listen(10);
                while (true)
                {
                    textBox_out.Clear();
                    byte[] buffer_input = new byte[1024];
                    Socket ns = s.Accept();
                    var l = ns.Receive(buffer_input);
                    text += "" + text;
                    text += Encoding.UTF8.GetString(buffer_input, 0, l);
                    textTmp = text;
                    ns.Shutdown(SocketShutdown.Both);
                    ns.Close();
                }
            }
            catch { }          
        }
       
         
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (textTmp.Length > 0) {
                textTmp = textTmp.Substring(1) + textTmp[0];
                textBox_out.Text = textTmp;               
                
            }           
        }
        private void size()
        {
           this.Width = 710;
           this.Height = 160;      
           this.MinimumSize = this.MaximumSize = this.Size;
        }
        

    }

}


