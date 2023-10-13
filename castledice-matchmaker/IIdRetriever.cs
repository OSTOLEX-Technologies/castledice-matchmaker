using casltedice_events_logic.ClientToServer;

namespace castledice_matchmaker;

public interface IIdRetriever
{
    int RetrievePlayerId(RequestGameDTO dto);
}