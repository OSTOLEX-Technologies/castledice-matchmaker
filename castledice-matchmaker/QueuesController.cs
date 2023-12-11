﻿using castledice_events_logic.ClientToServer;
using castledice_matchmaker.DataSenders;
using castledice_matchmaker.Queues;

namespace castledice_matchmaker;

public class QueuesController : IRequestGameDTOAccepter, ICancelGameDTOAccepter
{
    private readonly List<IGameModeQueue> _queues;
    private readonly ICancelationResultSender _cancelationResultSender;
    private readonly IMatchSender _matchSender;
    private readonly IIdRetriever _idRetriever;

    public QueuesController(List<IGameModeQueue> queues, IMatchSender matchSender, IIdRetriever idRetriever, ICancelationResultSender cancelationResultSender)
    {
        _queues = queues;
        _matchSender = matchSender;
        _idRetriever = idRetriever;
        _cancelationResultSender = cancelationResultSender;
    }

    public void AcceptRequestGameDTO(RequestGameDTO dto)
    {
        var playerId = _idRetriever.RetrievePlayerId(dto.VerificationKey);
        var duelQueue = _queues[0]; //TODO: At the moment the only possible game mode is duel, however in the future there will be more and this logic will have to be rewritten.
        duelQueue.EnqueuePlayer(playerId);
        var availableMatches = duelQueue.GetMatches();
        foreach (var match in availableMatches)
        {
            _matchSender.SendMatch(match);
        }
    }

    public void AcceptCancelGameDTO(CancelGameDTO dto)
    {
        var playerId = _idRetriever.RetrievePlayerId(dto.VerificationKey);
        var isPlayerRemoved = _queues.Any(queue => queue.RemovePlayer(playerId));
        _cancelationResultSender.SendCancelationResult(playerId, isPlayerRemoved);
    }
}