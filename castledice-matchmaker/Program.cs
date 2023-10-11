using System.Diagnostics;
using castledice_matchmaker;
using castledice_matchmaker.Configuration;
using castledice_matchmaker.Queues;
using castledice_matchmaker.Stubs;
using Microsoft.Extensions.Configuration;
using Riptide;
using Riptide.Utils;

internal class Program
{
    private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
    
    public static void Main(string[] args)
    {
        RiptideLogger.Initialize(Logger.Debug, Logger.Info, Logger.Warn, Logger.Error, false);
        
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
        var gameServerConnectionOptions =
            config.GetRequiredSection("GameServerConnectionOptions").Get<GameServerConnectionOptions>();
        var matchMakerStartOptions = config.GetRequiredSection("MatchMakerStartOptions").Get<MatchMakerStartOptions>();

        var matchMakerServer = new Server();
        Debug.Assert(matchMakerStartOptions != null, nameof(matchMakerStartOptions) + " != null");
        matchMakerServer.Start(matchMakerStartOptions.Port, matchMakerStartOptions.MaxClientCount);

        var gameServerClient = new Client();
        var gameServerClientWrapper = new ClientWrapper(gameServerClient);
        Debug.Assert(gameServerConnectionOptions != null, nameof(gameServerConnectionOptions) + " != null");
        gameServerClient.Connect($"{gameServerConnectionOptions.Ip}:{gameServerConnectionOptions.Port}");

        var controller = new QueuesController(
            new List<IGameModeQueue>
            {
                new DuelModeQueue()
            }, 
            new MatchSender(gameServerClientWrapper), 
            new IdRetrieverStub() //TODO: This must be replaced with an actual id retriever
            );
        
        RequestGameMessageHandler.SetAccepter(controller);

        while (true)
        {
            matchMakerServer.Update();
            gameServerClient.Update();
        }
    }
}