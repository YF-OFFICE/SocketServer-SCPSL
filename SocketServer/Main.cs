using MEC;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class Config
    { 
        public bool CXEnable { get; set; } = true;
        public string TcpPort { get; set; } = "7778";
        public string IP { get; set; } = "127.0.0.1";
    }
    public class Main
    {

        [PluginEntryPoint("SocketServer", "1.0.0", "aaaaaaa.", "Tyiuc")]
        void LoadPlugin()
        {
            Log.Info("Loaded plugin, register events...");
            EventManager.RegisterEvents(this);
        }

    


        [PluginEvent(ServerEventType.WaitingForPlayers)]
        void Wait() 
        {
        
        }

        public IEnumerator<float> SocketServer()
        {
            yield return Timing.WaitForSeconds(1f);
            while (true)
            {

            }
        
        
        }


        [PluginConfig] public Config PluginConfig;

    }


}
