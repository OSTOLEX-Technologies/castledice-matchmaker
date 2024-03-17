using castledice_events_logic.ClientToServer;

namespace castledice_matchmaker;

public interface IRequestGameDTOAccepter
{
    Task AcceptRequestGameDTOAsync(RequestGameDTO dto);
}