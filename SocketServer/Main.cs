using Exiled.API.Features;
using Exiled.API.Interfaces;
using Exiled.Events.Handlers;
using MEC;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Log = Exiled.API.Features.Log;
using Player = Exiled.API.Features.Player;
using Respawn = Exiled.API.Features.Respawn;
using Round = Exiled.API.Features.Round;
using Server = Exiled.API.Features.Server;

namespace SocketServer
{
    public class Config:IConfig
    { 
        public int TcpPort { get; set; } = 10087;
        public string IP { get; set; } = "127.0.0.1";
        public string name { get; set; } = "1服";
        public bool IsEnabled { get ; set ; }=true;
        public bool Debug { get ; set ; } = false;
    }
    public class Main:Plugin<Config>
    {
        public override string Author => "yudir";
        public override string Name => "CX";
        public override Version Version =>new Version(1,2,0);
        public static Main Maina;

        public override void OnEnabled()
        {
            Maina = this;
            Log.Info("Loaded plugin, register events...");
            Exiled.Events.Handlers.Server.WaitingForPlayers += Wait;
            base.OnEnabled();
        }

      public  void Wait() 
        {
            Task.Run(delegate ()
            {
                int port2 = Maina.Config.TcpPort;
                IPAddress any = IPAddress.Parse(Maina.Config.IP);
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
                        a += $"{Config.name} - 查询Success!!";
                        a += $"\r\n在线人数:{Player.List.Count().ToString()}/{Server.MaxPlayerCount}";
                        a += $"\r\n在线管理:{Player.List.ToList().FindAll(x => x.RemoteAdminAccess).Count}人";
                        a += $"\r\nIP:{Server.IpAddress}:{Server.Port}";
                        a += "\r\n查询时间" + DateTime.Now;
                        byte[] bytes = Encoding.UTF8.GetBytes(a);
                        socket.Send(bytes);
                        socket.Close();
                        Log.Debug($"接收消息{returna} - {a}");

                    }
                    else if (returna == "info")
                    {
                        string a = string.Empty;
                        a += $"{Config.name} - 查询Success!!";
                        a += $"\r\nDD人数:{Player.Get(PlayerRoles.RoleTypeId.ClassD).Count()}";
                        a += $"\r\n博士人数:{Player.Get(PlayerRoles.RoleTypeId.Scientist).Count()}人";
                        a += $"\r\nSCP人数:{Player.Get(PlayerRoles.Team.SCPs).Count()}";
                        a += $"\r\n回合进行时间：{Round.ElapsedTime.Minutes}/{Round.ElapsedTime.Seconds}";
                        a += $"\r\n回合次数：{Round.UptimeRounds}";
                        a += $"\r\n下一波刷新时间：{Respawn.NextTeamTime.Minute}min/{Respawn.NextTeamTime.Second}s";
                        a += "\r\n查询时间" + DateTime.Now; 
                        byte[] bytes = Encoding.UTF8.GetBytes(a);
                        socket.Send(bytes);
                        socket.Close();
                        Log.Debug($"接收消息{returna} - {a}");

                    }
                    else if (returna == "start")
                    {
                        string aaa = string.Empty;
                        if (Round.IsStarted)
                        {
                            aaa = "回合已经开启了";
                        }
                        else
                        {
                            Round.Start();
                            aaa = "回合启动成功";
                        }
                        byte[] bytes = Encoding.UTF8.GetBytes(aaa);
                        socket.Send(bytes);
                        socket.Close();
                    }
                    else if (returna == "rest")
                    {
                        string aaa = string.Empty;
                        Round.Restart();
                        aaa = "回合启动成功";
                        byte[] bytes = Encoding.UTF8.GetBytes(aaa);
                        socket.Send(bytes);
                        socket.Close();
                    }
                    else if (returna == "allrest")
                    {
                        string aaa = string.Empty;
                        Server.Restart();
                        aaa = "服务器启动成功";
                        byte[] bytes = Encoding.UTF8.GetBytes(aaa);
                        socket.Send(bytes);
                        socket.Close();
                    }
                    else if (returna.Contains("bc"))
                    {
                        string[] array2 = returna.Split('&');
                        string aaa = string.Empty;
                        Exiled.API.Features.Map.Broadcast(10, "[管理员消息]" + array2[1]);
                        aaa = "bc发送成功";
                        byte[] bytes = Encoding.UTF8.GetBytes(aaa);
                        socket.Send(bytes);
                        socket.Close();
                    }
                    else if (returna == ("list"))
                    {
                        string aaa = string.Empty;
                        foreach (var item in Player.List)
                        {
                            aaa += $"\r\n{item.Nickname}-{item.Id}";
                        }
                        byte[] bytes = Encoding.UTF8.GetBytes(aaa);
                        socket.Send(bytes);
                        socket.Close();
                    }
                    else if (returna.Contains("kick"))
                    {
                        string[] array2 = returna.Split('&');
                        string aaa = string.Empty;
                        Player a2a = Player.List.ToList().Find(x => x.Id.ToString() == array2[1]);
                        if (a2a == null)
                        { aaa = "踢出失败"; }
                        else
                        {
                            a2a.Kick("你已经被服务器踢出 原因请加q群询问");
                            aaa = "踢出玩家成功";
                        }

                        byte[] bytes = Encoding.UTF8.GetBytes(aaa);
                        socket.Send(bytes);
                        socket.Close();
                    }
                }

            });
                Log.Debug("Started");

       



                    
        
        }
    }


}
