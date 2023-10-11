using castledice_matchmaker.Matches;

namespace castledice_matchmaker.Queues;

public class DuelModeQueue : IGameModeQueue
{
    public void EnqueuePlayer(int playerId)
    {
        throw new NotImplementedException();
    }

    public List<Match> GetMatches()
    {
        throw new NotImplementedException();
    }

    public GameMode GameMode { get; }
}