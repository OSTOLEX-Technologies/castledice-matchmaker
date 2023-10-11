using casltedice_events_logic.ClientToServer;

namespace castledice_matchmaker.Stubs;

/// <summary>
/// This class MUST NOT be used in a production code.
/// </summary>
public class IdRetrieverStub : IIdRetriever
{
    public int RetrievePlayerId(RequestGameDTO dto)
    {
        return 1;
    }
}