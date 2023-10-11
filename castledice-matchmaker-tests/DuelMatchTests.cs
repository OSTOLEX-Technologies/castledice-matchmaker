using castledice_matchmaker.Matches;
using Moq;

namespace castledice_matchmaker_tests;

public class DuelMatchTests
{
    [Theory]
    [InlineData(1, 2)]
    [InlineData(3, 4)]
    public void IdProperties_ShouldReturnIds_GivenInConstructor(int firstPlayerId, int secondPlayerId)
    {
        var match = new DuelMatch(firstPlayerId, secondPlayerId);
        
        Assert.Equal(firstPlayerId, match.FirstPlayerId);
        Assert.Equal(secondPlayerId, match.SecondPlayerId);
    }

    [Fact]
    public void Accept_ShouldCallVisitDuelMatch_OnGivenVisitor()
    {
        var visitorMock = new Mock<IMatchVisitor<int>>();
        var match = new DuelMatch(1, 2);

        match.Accept(visitorMock.Object);
        
        visitorMock.Verify(v => v.VisitDuelMatch(match), Times.Once);
    }

    [Fact]
    public void Accept_ShouldReturnValue_ReturnedByVisitor()
    {
        var match = new DuelMatch(1, 2);
        var visitorMock = new Mock<IMatchVisitor<int>>();
        var expectedValue = 3;
        visitorMock.Setup(v => v.VisitDuelMatch(match)).Returns(expectedValue);

        var actualValue = match.Accept(visitorMock.Object);
        
        Assert.Equal(expectedValue, actualValue);
    }
}