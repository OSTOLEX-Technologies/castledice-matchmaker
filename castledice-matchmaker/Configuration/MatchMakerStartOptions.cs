namespace castledice_matchmaker.Configuration;

public sealed class MatchMakerStartOptions
{
    public ushort Port { get; set; }
    public ushort MaxClientCount { get; set; }
}