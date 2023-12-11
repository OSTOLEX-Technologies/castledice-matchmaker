using castledice_events_logic.ClientToServer;
using castledice_riptide_dto_adapters.Extensions;
using Riptide;

namespace castledice_matchmaker.MessageHandlers;

public static class RequestGameMessageHandler
{
    private static IRequestGameDTOAccepter _dtoAccepter;

    public static void SetAccepter(IRequestGameDTOAccepter accepter)
    {
        _dtoAccepter = accepter;
    }

    [MessageHandler((ushort)ClientToServerMessageType.RequestGame)]
    private static void HandleRequestGameMessage(ushort fromClientId, Message message)
    {
        _dtoAccepter.AcceptRequestGameDTO(message.GetRequestGameDTO());
    }
}