using Riptide;

namespace castledice_matchmaker;

public interface IMessageSenderById
{
    void Send(Message message, ushort clientId);
    void SendToAll(Message message, ushort except);
    void SendToAll(Message message);
}