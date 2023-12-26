using Exiled.API.Features;
using Exiled.API.Interfaces;
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
using Log = Exiled.API.Features.Log;
using Player = Exiled.API.Features.Player;
using Server = Exiled.API.Features.Server;

namespace SocketServer
{
    public class Config : IConfig
    { 
        public bool CXEnable { get; set; } = true;
        public int TcpPort { get; set; } = 7778;
        public string IP { get; set; } = "127.0.0.1";
        bool IConfig.IsEnabled { get; set; }= true;
        bool IConfig.Debug { get; set; } = false;
    }
    public class Main:Plugin<Config>
    {
        public static Main GetMain;
        public CoroutineHandle coroutine { get; set; } = new CoroutineHandle();


        public override void OnEnabled()
        {
            GetMain = this;
            Log.Info("Loaded plugin, register events...");
            Exiled.Events.Handlers.Server.WaitingForPlayers += this.Wait;
            base.OnEnabled();
        }
       public void Wait() 
        {
            if (coroutine.IsRunning)
            {
                Timing.KillCoroutines(coroutine);
                coroutine = Timing.RunCoroutine(SocketServer());
                Log.Debug("Started");
            }
            else
            {
                coroutine = Timing.RunCoroutine(SocketServer());
                Log.Debug("Started");
            }
        }

        public static IEnumerator<float> SocketServer()
        {
            yield return Timing.WaitForSeconds(1f);
            int port2 =  GetMain.Config.TcpPort;
            IPAddress any = IPAddress.Parse(GetMain.Config.IP);
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
                    a += $"\r\n在线人数:{Player.List.Count().ToString()}/{Server.MaxPlayerCount}";
                    a += $"\r\n在线管理:{Player.List.ToList().FindAll(x => x.RemoteAdminAccess).Count}人";
                    a += $"\r\nIP:{Server.IpAddress}";
                    a += "\r\n查询时间" + DateTime.Now;
                    byte[] bytes = Encoding.UTF8.GetBytes(a);
                    socket.Send(bytes);
                    socket.Close();

                }



            }
        
        
        }
    }


}
