namespace castledice_matchmaker.DataSenders;

public interface ICancelationResultSender
{
    void SendCancelationResult(int playerId, bool isCanceled);
}