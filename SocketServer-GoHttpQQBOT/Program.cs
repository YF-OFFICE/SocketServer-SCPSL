using EleCho.GoCqHttpSdk;
using System.Net.Sockets;
using System.Net;
using System.Text;

 static string SocketServer(string ServerIP,int Port)
{
    try
    {
    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    socket.Connect(IPAddress.Parse(ServerIP), Port);
    byte[] bytes = Encoding.UTF8.GetBytes("cx");
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
Console.WriteLine("\r\n[DIRSystem]: 连接WebSK-Success-Sk8080端口");
session.UseGroupMessage(async context =>
{
    if (context.Message.Text == "cx")
    {
        string a = SocketServer("127.0.0.1", 7778);
        if (a == "null")
        {
            a = "服务器不在线 awa";

        }
        await session.SendGroupMessageAsync(context.GroupId, new EleCho.GoCqHttpSdk.Message.CqMessage(a));
    }

});
await session.RunAsync();

