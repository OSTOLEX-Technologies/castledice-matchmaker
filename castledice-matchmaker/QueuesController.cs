using castledice_events_logic.ClientToServer;
using castledice_matchmaker.DataSenders;
using castledice_matchmaker.Matches;
using castledice_matchmaker.Queues;
using NLog;

namespace castledice_matchmaker;

public class QueuesController : IRequestGameDTOAccepter, ICancelGameDTOAccepter
{
    private readonly List<IGameModeQueue> _queues;
    private readonly ICancelationResultSender _cancelationResultSender;
    private readonly IMatchSender _matchSender;
    private readonly IIdRetriever _idRetriever;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public QueuesController(List<IGameModeQueue> queues, IMatchSender matchSender, IIdRetriever idRetriever, ICancelationResultSender cancelationResultSender)
    {
        _queues = queues;
        _matchSender = matchSender;
        _idRetriever = idRetriever;
        _cancelationResultSender = cancelationResultSender;
    }

    public async Task AcceptRequestGameDTOAsync(RequestGameDTO dto)
    {
        _logger.Info($"Received request game DTO with verification key: {dto.VerificationKey}");
        var playerId = await _idRetriever.RetrievePlayerIdAsync(dto.VerificationKey);
        var duelQueue = _queues[0]; //TODO: At the moment the only possible game mode is duel, however in the future there will be more and this logic will have to be rewritten.
        duelQueue.EnqueuePlayer(playerId);
        var availableMatches = duelQueue.GetMatches();
        foreach (var match in availableMatches)
        {
            if (match is DuelMatch duelMatch)
            {
                _logger.Info($"Sending duel match to players: {duelMatch.FirstPlayerId} and {duelMatch.SecondPlayerId}");
            }
            _matchSender.SendMatch(match);
        }
    }

    public async Task AcceptCancelGameDTOAsync(CancelGameDTO dto)
    {
        _logger.Info($"Received cancel game DTO with verification key: {dto.VerificationKey}");
        var playerId = await _idRetriever.RetrievePlayerIdAsync(dto.VerificationKey);
        var isPlayerRemoved = _queues.Any(queue => queue.RemovePlayer(playerId));
        if (isPlayerRemoved)
        {
            _logger.Info($"Player with id {playerId} has been removed from all queues");
        }
        else
        {
            _logger.Info($"Player with id {playerId} has not been found in any queue");
        }
        _cancelationResultSender.SendCancelationResult(playerId, isPlayerRemoved);
    }
}