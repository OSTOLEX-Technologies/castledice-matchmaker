using castledice_matchmaker.Matches;

namespace castledice_matchmaker;

public interface IMatchSender
{
    void SendMatch(Match match);
}