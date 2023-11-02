namespace castledice_matchmaker.Stubs;

/// <summary>
/// This class MUST NOT be used in a production code.
/// </summary>
public class StringIdRetrieverStub : IIdRetriever
{
    public int RetrievePlayerId(string playerToken)
    {
        return int.Parse(playerToken);
    }
}