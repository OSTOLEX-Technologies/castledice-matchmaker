using casltedice_events_logic.ClientToServer;
using castledice_matchmaker.DataSenders;
using castledice_matchmaker.Matches;

namespace castledice_matchmaker.Stubs;

/// <summary>
/// This class MUST NOT be used in a production code.
/// </summary>
public class QueueControllerStub : IRequestGameDTOAccepter
{
    private readonly IMatchSender _matchSender;

    public QueueControllerStub(IMatchSender matchSender)
    {
        _matchSender = matchSender;
    }

    public void AcceptRequestGameDTO(RequestGameDTO dto)
    {
        _matchSender.SendMatch(new DuelMatch(1, 2));
    }
}