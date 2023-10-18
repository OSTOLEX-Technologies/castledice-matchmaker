using castledice_matchmaker.Matches;

namespace castledice_matchmaker.DataSenders;

public interface IMatchSender
{
    void SendMatch(Match match);
}