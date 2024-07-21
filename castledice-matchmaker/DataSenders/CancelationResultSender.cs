using castledice_events_logic.ServerToClient;
using castledice_riptide_dto_adapters.Extensions;
using NLog;
using Riptide;

namespace castledice_matchmaker.DataSenders;

public class CancelationResultSender : ICancelationResultSender
{
    private readonly IMessageSenderById _messageSender;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public CancelationResultSender(IMessageSenderById messageSender)
    {
        _messageSender = messageSender;
    }

    public void SendCancelationResult(int playerId, bool isCanceled)
    {
        _logger.Info($"Sending cancelation result for player with id {playerId}");
        var dto = new CancelGameResultDTO(isCanceled, playerId);
        var message = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientMessageType.CancelGame);
        message.AddCancelGameResultDTO(dto);
        _messageSender.SendToAll(message);
    }
}