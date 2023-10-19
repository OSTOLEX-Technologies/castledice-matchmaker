using castledice_matchmaker.DataSenders;
using castledice_matchmaker.Matches;
using castledice_riptide_dto_adapters.Extensions;

namespace castledice_matchmaker_tests;

public class MatchSenderTests
{
    [Fact]
    //MatchFoundDTO sent by MatchSender must contain list with same players' ids as in a given match object.
    public void SendMatch_ShouldSendMessageWithAppropriateMatchFoundDTO()
    {
        var firstPlayerId = 1;
        var secondPlayerId = 3;
        var messageSender = new TestMessageSenderById();
        var matchSender = new MatchSender(messageSender);
        var matchToSend = new DuelMatch(firstPlayerId, secondPlayerId);
        
        matchSender.SendMatch(matchToSend);
        var sentMessage = messageSender.SentMessage;
        sentMessage.GetByte();
        sentMessage.GetByte(); //First two bytes in a message contain information about message send mode and message id. Well, I suppose they do.
        var sentDTO = sentMessage.GetMatchFoundDTO();
        
        Assert.True(sentDTO.PlayerIds.Count == 2);
        Assert.Contains(firstPlayerId, sentDTO.PlayerIds);
        Assert.Contains(secondPlayerId, sentDTO.PlayerIds);
    }
}