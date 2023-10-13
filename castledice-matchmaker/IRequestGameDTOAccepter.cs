using casltedice_events_logic.ClientToServer;

namespace castledice_matchmaker;

public interface IRequestGameDTOAccepter
{
    void AcceptRequestGameDTO(RequestGameDTO dto);
}