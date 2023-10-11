using Riptide;

namespace castledice_matchmaker;

public interface IMessageSender
{
    void Send(Message message);
}