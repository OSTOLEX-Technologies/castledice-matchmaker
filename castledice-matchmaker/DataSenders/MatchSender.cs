using casltedice_events_logic.ServerToClient;
using castledice_matchmaker.Matches;
using castledice_riptide_dto_adapters.Extensions;
using Riptide;

namespace castledice_matchmaker.DataSenders;

public class MatchSender : IMatchVisitor<Message>, IMatchSender
{
    private readonly IMessageSenderById _messageSenderById;

    public MatchSender(IMessageSenderById messageSenderById)
    {
        _messageSenderById = messageSenderById;
    }

    public void SendMatch(Match match)
    {
        var message = match.Accept(this);
        _messageSenderById.SendToAll(message); //It is supposed that Matchmaker will have only one client, which is the game server, so there is no need to send message to specific client.
    }
    
    public Message VisitDuelMatch(DuelMatch match)
    {
        var matchFoundDTO = new MatchFoundDTO(new List<int>() { match.FirstPlayerId, match.SecondPlayerId });
        var message = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientMessageType.MatchFound);
        message.AddMatchFoundDTO(matchFoundDTO);
        return message;
    }
}