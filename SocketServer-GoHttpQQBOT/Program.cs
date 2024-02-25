using EleCho.GoCqHttpSdk;
using System.Net.Sockets;
using System.Net;
using System.Text;

 static string SocketServer(string ServerIP,int Port,string Text)
{
    try
    {
    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    socket.Connect(IPAddress.Parse(ServerIP), Port);
    byte[] bytes = Encoding.UTF8.GetBytes(Text);
    socket.Send(bytes);
    byte[] array = new byte[1024];
    int count = socket.Receive(array);
    string text = Encoding.UTF8.GetString(array, 0, count);
    return text;
    }
    catch (Exception)
    {

        return "null";
    }


}

CqWsSession session = new CqWsSession(new CqWsSessionOptions
{
    BaseUri = new Uri("ws://localhost:8080"),  // WebSocket 地址
});
Console.WriteLine("\r\nWelcome DIRSYSTEM-V1.0.1 Byyudir");
Console.WriteLine("\r\n[DIRSystem]: 使用前请确保Gohttp客户端是打开的并且打开了正向WebSocket8080端口 服务器端打开10087TCP端口");
Console.WriteLine("\r\n[DIRSystem]: 使用Q群指令:cx,info(没有限制是哪一个群 所以指令是用于账号所在的所有q群)");
Console.WriteLine("\r\n[DIRSystem]: 正在连接WebSK-Success-Sk8080端口");
session.UseGroupMessage(async context =>
{
    if (context.Message.Text == "cx")
    {
        string a = SocketServer("127.0.0.1", 10087, "cx");
        if (a == "null")
        {
            a = "服务器不在线 awa";

        }

        await session.SendGroupMessageAsync(context.GroupId, new EleCho.GoCqHttpSdk.Message.CqMessage(a));
        //Console.WriteLine($"收到来自{context.GroupId}的{context.UserId}的查询消息返回{a}");
    }
    else if (context.Message.Text == "info")
    {
        string a = SocketServer("127.0.0.1", 10087, "info");
        if (a == "null")
        {
            a = "服务器不在线 awa";

        }

        await session.SendGroupMessageAsync(context.GroupId, new EleCho.GoCqHttpSdk.Message.CqMessage(a));

    }
        else if (context.Message.Text.Contains("round"))
    {
        if (context.Sender.Role == CqRole.Owner)
        {
            string[] sad = context.Message.Text.Split(Array.Empty<char>());
            if (sad[1] == "start")
            {
                string a = SocketServer("127.0.0.1", 10087, "start");
                if (a == "null")
                {
                    a = "服务器不在线 awa";

                }
                await session.SendGroupMessageAsync(context.GroupId, new EleCho.GoCqHttpSdk.Message.CqMessage(a));
            }
            else if (sad[1] == "rest")
            {
                string a = SocketServer("127.0.0.1", 10087, "rest");
                if (a == "null")
                {
                    a = "服务器不在线 awa";

                }
                await session.SendGroupMessageAsync(context.GroupId, new EleCho.GoCqHttpSdk.Message.CqMessage(a));
            }
            else if (sad[1] == "allrest")
            {
                string a = SocketServer("127.0.0.1", 10087, "allrest");
                if (a == "null")
                {
                    a = "服务器不在线 awa";

                }
                await session.SendGroupMessageAsync(context.GroupId, new EleCho.GoCqHttpSdk.Message.CqMessage(a));
            }
            else if (sad[1] == "bc")
            {
                string a = SocketServer("127.0.0.1", 10087, $"bc&{sad[2]}");
                if (a == "null")
                {
                    a = "服务器不在线 awa";

                }
                await session.SendGroupMessageAsync(context.GroupId, new EleCho.GoCqHttpSdk.Message.CqMessage(a));
            }
            else if (sad[1] == "list")
            {
                string a = SocketServer("127.0.0.1", 10087, "list");
                if (a == "null")
                {
                    a = "服务器不在线 awa";

                }
                await session.SendGroupMessageAsync(context.GroupId, new EleCho.GoCqHttpSdk.Message.CqMessage(a));
            }
            else if (sad[1] == "kick")
            {
                string a = SocketServer("127.0.0.1", 10087, $"kick&{sad[2]}");
                if (a == "null")
                {
                    a = "服务器不在线 awa";

                }
                await session.SendGroupMessageAsync(context.GroupId, new EleCho.GoCqHttpSdk.Message.CqMessage(a));
            }

        }
        else
        {
            await session.SendGroupMessageAsync(context.GroupId, new EleCho.GoCqHttpSdk.Message.CqMessage("你没权限hahah"));
        }

    }

});
await session.RunAsync();

