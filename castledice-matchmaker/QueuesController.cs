using casltedice_events_logic.ClientToServer;
using castledice_matchmaker.Queues;

namespace castledice_matchmaker;

public class QueuesController : IRequestGameDTOAccepter
{
    private readonly List<IGameModeQueue> _queues;
    private readonly IMatchSender _matchSender;
    private readonly IIdRetriever _idRetriever;

    public QueuesController(List<IGameModeQueue> queues, IMatchSender matchSender, IIdRetriever idRetriever)
    {
        _queues = queues;
        _matchSender = matchSender;
        _idRetriever = idRetriever;
    }

    public void AcceptRequestGameDTO(RequestGameDTO dto)
    {
        var playerId = _idRetriever.RetrievePlayerId(dto);
        var duelQueue = _queues[0]; //TODO: At the moment the only possible game mode is duel, however in the future there will be more and this logic will have to be rewritten.
        duelQueue.EnqueuePlayer(playerId);
        var availableMatches = duelQueue.GetMatches();
        foreach (var match in availableMatches)
        {
            _matchSender.SendMatch(match);
        }
    }
}