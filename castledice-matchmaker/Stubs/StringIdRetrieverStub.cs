namespace castledice_matchmaker.Stubs;

/// <summary>
/// This class MUST NOT be used in a production code.
/// </summary>
public class StringIdRetrieverStub : IIdRetriever
{
    public Task<int> RetrievePlayerIdAsync(string playerToken)
    {
        return Task.FromResult(int.Parse(playerToken));
    }
}