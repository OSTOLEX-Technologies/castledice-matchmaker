using castledice_matchmaker.Matches;

namespace castledice_matchmaker.Queues;

public interface IGameModeQueue
{
    void EnqueuePlayer(int playerId);
    List<Match> GetMatches();
    bool RemovePlayer(int playerId);
    GameMode GameMode { get; }
}