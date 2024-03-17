using castledice_events_logic.ClientToServer;

namespace castledice_matchmaker;

public interface IIdRetriever
{
    public Task<int> RetrievePlayerIdAsync(string playerToken);
}