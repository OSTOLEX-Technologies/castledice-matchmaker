using castledice_matchmaker.Matches;

namespace castledice_matchmaker.Queues;

public interface IGameModeQueue
{
    void EnqueuePlayer(int playerId);
    List<Match> GetMatches();
    GameMode GameMode { get; }
}