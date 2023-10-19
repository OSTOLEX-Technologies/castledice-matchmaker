using Riptide;

namespace castledice_matchmaker;

public class ServerWrapper : IMessageSenderById
{
    public Server Server { get; private set; }

    public ServerWrapper(Server server)
    {
        Server = server;
    }

    public void Send(Message message, ushort clientId)
    {
        Server.Send(message, clientId);
    }

    public void SendToAll(Message message, ushort except)
    {
        Server.SendToAll(message, except);
    }

    public void SendToAll(Message message)
    {
        Server.SendToAll(message);
    }
}