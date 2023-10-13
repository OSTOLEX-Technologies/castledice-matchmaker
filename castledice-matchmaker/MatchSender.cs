using casltedice_events_logic.ServerToClient;
using castledice_matchmaker.Matches;
using castledice_riptide_dto_adapters.Extensions;
using Riptide;

namespace castledice_matchmaker;

public class MatchSender : IMatchVisitor<int>, IMatchSender
{
    private readonly IMessageSender _messageSender;

    public MatchSender(IMessageSender messageSender)
    {
        _messageSender = messageSender;
    }

    public void SendMatch(Match match)
    {
        match.Accept(this);
    }
    
    public int VisitDuelMatch(DuelMatch match)
    {
        var matchFoundDTO = new MatchFoundDTO(new List<int>() { match.FirstPlayerId, match.SecondPlayerId });
        var message = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientMessageType.MatchFound);
        message.AddMatchFoundDTO(matchFoundDTO);
        _messageSender.Send(message);
        return 0;
    }
}