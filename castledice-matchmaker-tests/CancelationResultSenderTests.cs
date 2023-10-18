using castledice_matchmaker.DataSenders;
using castledice_riptide_dto_adapters.Extensions;

namespace castledice_matchmaker_tests;

public class CancelationResultSenderTests
{
    [Theory]
    [InlineData(1, true)]
    [InlineData(2, false)]
    public void SendCancelationResult_ShouldSendMessageWithAppropriateCancelGameResultDTO(int playerId, bool isCanceled)
    {
        var messageSender = new TestMessageSenderById();
        var cancelationResultSender = new CancelationResultSender(messageSender);
        
        cancelationResultSender.SendCancelationResult(playerId, isCanceled);
        var sentMessage = messageSender.SentMessage;
        var sentDTO = sentMessage.GetCancelGameResultDTO();
        
        Assert.Equal(playerId, sentDTO.PlayerId);
        Assert.Equal(isCanceled, sentDTO.IsCanceled);
    }
}