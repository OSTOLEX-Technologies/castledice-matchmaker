namespace castledice_matchmaker.DataSenders;

public class CancelationResultSender : ICancelationResultSender
{
    private IMessageSenderById _messageSender;

    public CancelationResultSender(IMessageSenderById messageSender)
    {
        _messageSender = messageSender;
    }

    public void SendCancelationResult(int playerId, bool isCanceled)
    {
        throw new NotImplementedException();
    }
}