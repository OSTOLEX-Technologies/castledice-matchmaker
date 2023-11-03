using casltedice_events_logic.ClientToServer;
using castledice_riptide_dto_adapters.Extensions;
using Riptide;

namespace castledice_matchmaker.MessageHandlers;

public static class CancelGameMessageHandler
{
    private static ICancelGameDTOAccepter _dtoAccepter;
    
    public static void SetAccepter(ICancelGameDTOAccepter accepter)
    {
        _dtoAccepter = accepter;
    }
    
    [MessageHandler((ushort)ClientToServerMessageType.CancelGame)]
    private static void HandleCancelGameMessage(ushort fromClientId, Message message)
    {
        _dtoAccepter.AcceptCancelGameDTO(message.GetCancelGameDTO());
    }
}