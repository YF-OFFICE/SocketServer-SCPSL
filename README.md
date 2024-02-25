# SocketServer-SCPSL
一个可以将QQ群与服务端连接的插件

目前所有拥有的指令:cx,info，round

增加round指令
round list查询玩家列表
round rest 重启回合
round start 启动回合
round allrest 重启服务器
round kick+id 踢出对应id玩家
round bc+text 向服务器发送广播
Warn:目前round指令只允许Q群群主使用 如有其他需求请在issues里提出


说明：
本插件运用Socket将[SCPSL](scpslgame.com)

基于[EXILED](https://github.com/Exiled-Team/EXILED/)开发

基于[EleCho.GoCqHttpSdk](https://github.com/OrgEleCho/EleCho.GoCqHttpSdk)开发

查询功能与QQ群里连接起来;（也就是CX功能）



使用方法:


在[Releases](https://github.com/NLK-TeamOffice/SocketServer-SCPSL/releases/)里找到最新版本并下载


1.将SocketServer.dll文件放到exiled/plugin里


2.打开服务器(确保10078Tcp端口是打开的)



3.打开[Go-HttpQQ](https://docs.go-cqhttp.org/)客户端(确保已经将WS正向(Socket)端口设置为8080)


4.解压GoHttpqq-Socket.zip


5.打开GoHttpqq-Socket.exe挂在后台 


6.在群中发送cx即可获取服务器信息

注意:本插件没有指定QQ群 也就是说所登陆的QQ账号的所有群都适用




如有bug请在Iss里提出
