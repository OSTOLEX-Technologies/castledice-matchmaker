using castledice_events_logic.ClientToServer;
using castledice_matchmaker;
using castledice_matchmaker.DataSenders;
using castledice_matchmaker.Queues;
using Moq;

namespace castledice_matchmaker_tests;

public class QueuesControllerTests
{
    [Fact]
    public void AcceptCancelGameDTO_ShouldSendCancelationResultWithFalse_IfNoQueueContainsPlayerId()
    {
        var playerId = 3;
        var idRetrieverMock = new Mock<IIdRetriever>();
        var cancelationResultSenderMock = new Mock<ICancelationResultSender>();
        idRetrieverMock.Setup(retriever => retriever.RetrievePlayerId(It.IsAny<string>())).Returns(playerId);
        var queuesController = new QueuesControllerBuilder
        {
            IdRetriever = idRetrieverMock.Object,
            CancelationResultSender = cancelationResultSenderMock.Object
        }.Build();
        var cancelGameDTO = new CancelGameDTO("somekey");
        
        queuesController.AcceptCancelGameDTO(cancelGameDTO);
        
        cancelationResultSenderMock.Verify(s => s.SendCancelationResult(3, false), Times.Once);
    }

    [Fact]
    public void AcceptCancelGameDTO_ShouldSendCancelationResultWithTrue_IfPlayerWasRemovedFromSomeQueue()
    {
        var playerId = 4;
        var idRetrieverMock = new Mock<IIdRetriever>();
        var cancelationResultSenderMock = new Mock<ICancelationResultSender>();
        idRetrieverMock.Setup(retriever => retriever.RetrievePlayerId(It.IsAny<string>())).Returns(playerId);
        var queue = new Mock<IGameModeQueue>();
        queue.Setup(q => q.RemovePlayer(playerId)).Returns(true);
        var queuesController = new QueuesControllerBuilder
        {
            IdRetriever = idRetrieverMock.Object,
            CancelationResultSender = cancelationResultSenderMock.Object,
            Queues = new List<IGameModeQueue> {queue.Object}
        }.Build();
        
        queuesController.AcceptCancelGameDTO(new CancelGameDTO("somekey"));
        
        cancelationResultSenderMock.Verify(s => s.SendCancelationResult(playerId, true), Times.Once);
    }
     
    public class QueuesControllerBuilder
    {
        public List<IGameModeQueue> Queues { get; set; } = new();
        public IMatchSender MatchSender { get; set; } = new Mock<IMatchSender>().Object;
        public ICancelationResultSender CancelationResultSender { get; set; } = new Mock<ICancelationResultSender>().Object;
        public IIdRetriever IdRetriever { get; set; } = new Mock<IIdRetriever>().Object;
        
        public QueuesController Build()
        {
            return new(Queues, MatchSender, IdRetriever, CancelationResultSender);
        }
    }
}