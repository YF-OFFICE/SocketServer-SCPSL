using MEC;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class Config
    { 
        public bool CXEnable { get; set; } = true;
        public int TcpPort { get; set; } = 7778;
        public string IP { get; set; } = "127.0.0.1";
    }
    public class Main
    {

        public CoroutineHandle coroutine { get; set; } = new CoroutineHandle();

        [PluginEntryPoint("SocketServer", "1.0.0", "aaaaaaa.", "Tyiuc")]
        void LoadPlugin()
        {
            Log.Info("Loaded plugin, register events...");
            EventManager.RegisterEvents(this);
        }

    


        [PluginEvent(ServerEventType.WaitingForPlayers)]
        void Wait() 
        {
            if (coroutine.IsRunning)
            {
                Timing.KillCoroutines(coroutine);
                coroutine = Timing.RunCoroutine(SocketServer());
            }
            coroutine = Timing.RunCoroutine(SocketServer());
            Log.Debug("Started");
        }

        public static IEnumerator<float> SocketServer()
        {
            yield return Timing.WaitForSeconds(1f);
            int port2 = PluginConfig.TcpPort;
            IPAddress any = IPAddress.Parse(PluginConfig.IP);
            var ReceiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipendPoint = new IPEndPoint(any, port2);
            ReceiveSocket.Bind(new IPEndPoint(any, port2));
            ReceiveSocket.Listen(10);
            Log.Info("SocketConnect !");
            while (true)
             {
                Socket socket = ReceiveSocket.Accept();
                byte[] array = new byte[1024];
                int count = socket.Receive(array);
                string returna = Encoding.UTF8.GetString(array, 0, count);
                if (returna == "cx")
                {
                    string a = string.Empty;
                    a += $"服务器#1 - 查询成功 ";
                    a += $"\r\n在线人数:{Player.GetPlayers().Count().ToString()}/{Server.MaxPlayers}";
                    a += $"\r\n在线管理:{Player.GetPlayers().FindAll(x => x.RemoteAdminAccess).Count}人";
                    a += $"\r\nIP:{Server.ServerIpAddress}";
                    a += "\r\n查询时间" + DateTime.Now;
                    byte[] bytes = Encoding.UTF8.GetBytes(a);
                    socket.Send(bytes);
                    socket.Close();

                }



            }
        
        
        }


        [PluginConfig] public static Config PluginConfig;

    }


}
