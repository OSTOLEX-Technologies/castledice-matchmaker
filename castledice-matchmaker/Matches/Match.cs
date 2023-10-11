namespace castledice_matchmaker.Matches;

public abstract class Match
{
    public abstract T Accept<T>(IMatchVisitor<T> visitor);
}