namespace castledice_matchmaker.Matches;

public class DuelMatch : Match
{
    public int FirstPlayerId { get; private set; }
    public int SecondPlayerId { get; private set; }

    public DuelMatch(int firstPlayerId, int secondPlayerId)
    {
        FirstPlayerId = firstPlayerId;
        SecondPlayerId = secondPlayerId;
    }

    public override T Accept<T>(IMatchVisitor<T> visitor)
    {
        return visitor.VisitDuelMatch(this);
    }
}