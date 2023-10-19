using castledice_matchmaker;
using Riptide;

namespace castledice_matchmaker_tests;

public class TestMessageSenderById : IMessageSenderById
{
    public Message SentMessage;
        
    public void Send(Message message)
    {
        SentMessage = message;
    }

    public void Send(Message message, ushort clientId)
    {
        SentMessage = message;
    }

    public void SendToAll(Message message, ushort except)
    {
        SentMessage = message;
    }

    public void SendToAll(Message message)
    {
        SentMessage = message;
    }
}