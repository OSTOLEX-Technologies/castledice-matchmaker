using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using castledice_matchmaker;
using castledice_matchmaker.Matches;
using castledice_matchmaker.Queues;

namespace castledice_matchmaker_tests;

public class DuelModeQueueTests
{
    [Fact]
    public void GameModeProperty_ShouldReturnDuel()
    {
        var queue = new DuelModeQueue();

        Assert.Equal(GameMode.Duel, queue.GameMode);
    }

    [Fact]
    public void GetMatches_ShouldReturnEmptyList_IfNoPlayersEnqueued()
    {
        var queue = new DuelModeQueue();

        Assert.Empty(queue.GetMatches());
    }

    [Theory]
    [InlineData(2, 1)]
    [InlineData(1, 0)]
    [InlineData(3, 1)]
    [InlineData(10, 5)]
    [InlineData(11, 5)]
    //If amount of enqueued players is an odd number, than amount of matches should be floor rounded.
    public void GetMatches_ShouldReturnListWithCount_EqualToHalfOfPlayersAmount(int playersCount, int expectedMatchCount)
    {
        var queue = new DuelModeQueue();
        for (int i = 0; i < playersCount; i++)
        {
            queue.EnqueuePlayer(i);
        }

        var matches = queue.GetMatches();
        
        Assert.True(matches.Count == expectedMatchCount);
    }

    [Fact]
    public void MatchesReturnedByGetMatches_ShouldContainIdsOfEnqueuedPlayers()
    {
        var firstPlayerId = 3;
        var secondPlayerId = 4;
        var queue = new DuelModeQueue();
        queue.EnqueuePlayer(firstPlayerId);
        queue.EnqueuePlayer(secondPlayerId);

        var match = queue.GetMatches().First() as DuelMatch;
        
        Assert.Equal(firstPlayerId, match.FirstPlayerId);
        Assert.Equal(secondPlayerId, match.SecondPlayerId);
    }

    [Fact]
    public void RemovePlayer_ShouldReturnTrue_IfPlayerWasRemoved()
    {
        var playerId = 3;
        var queue = new DuelModeQueue();
        queue.EnqueuePlayer(playerId);
        
        var result = queue.RemovePlayer(playerId);
        
        Assert.True(result);
    }

    [Fact]
    public void RemovePlayer_ShouldReturnFalse_IfPlayerWasNotRemoved()
    {
        var queue = new DuelModeQueue();
        
        var result = queue.RemovePlayer(3);
        
        Assert.False(result);
    }
}