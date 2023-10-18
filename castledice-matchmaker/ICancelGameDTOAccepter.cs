using casltedice_events_logic.ClientToServer;

namespace castledice_matchmaker;

public interface ICancelGameDTOAccepter
{
    void AcceptCancelGameDTO(CancelGameDTO dto);
}