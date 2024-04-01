using castledice_events_logic.ClientToServer;

namespace castledice_matchmaker;

public interface ICancelGameDTOAccepter
{
    Task AcceptCancelGameDTOAsync(CancelGameDTO dto);
}