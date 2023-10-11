using castledice_matchmaker.Matches;

namespace castledice_matchmaker.Queues;

public class DuelModeQueue : IGameModeQueue
{
    private readonly Queue<int> _waitingPlayersIds = new();
    
    public void EnqueuePlayer(int playerId)
    {
        _waitingPlayersIds.Enqueue(playerId);
    }

    public List<Match> GetMatches()
    {
        var matches = new List<Match>();
        while (_waitingPlayersIds.Count >= 2)
        {
            var firstPlayerId = _waitingPlayersIds.Dequeue();
            var secondPlayerId = _waitingPlayersIds.Dequeue();
            var match = new DuelMatch(firstPlayerId, secondPlayerId);
            matches.Add(match);
        }
        return matches;
    }

    public GameMode GameMode => GameMode.Duel;
}