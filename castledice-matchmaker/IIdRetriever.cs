using casltedice_events_logic.ClientToServer;

namespace castledice_matchmaker;

public interface IIdRetriever
{
    int RetrievePlayerId(string playerToken);
}