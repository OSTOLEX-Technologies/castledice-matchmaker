namespace castledice_matchmaker.Matches;

public interface IMatchVisitor<out T>
{
    T VisitDuelMatch(DuelMatch match);
}