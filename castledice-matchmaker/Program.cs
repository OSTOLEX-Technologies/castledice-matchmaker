using System.Diagnostics;
using castledice_matchmaker;
using castledice_matchmaker.Configuration;
using castledice_matchmaker.DataSenders;
using castledice_matchmaker.HttpUtilities;
using castledice_matchmaker.MessageHandlers;
using castledice_matchmaker.Queues;
using Microsoft.Extensions.Configuration;
using Riptide;
using Riptide.Transports.Tcp;
using Riptide.Utils;

internal class Program
{
    private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
    
    public static void Main(string[] args)
    {
        RiptideLogger.Initialize(Logger.Debug, Logger.Info, Logger.Warn, Logger.Error, false);
        
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
        var matchMakerStartOptions = config.GetRequiredSection("MatchMakerStartOptions").Get<MatchMakerStartOptions>();
        var authBackendConnectionOptions = config.GetRequiredSection("AuthBackendConnectionOptions").Get<AuthBackendConnectionOptions>();
        Debug.Assert(matchMakerStartOptions != null, nameof(matchMakerStartOptions) + " != null");
        Debug.Assert(authBackendConnectionOptions != null, nameof(authBackendConnectionOptions) + " != null");
       
        var httpClientWrapper = new HttpClientWrapper(new HttpClient());
        var matchMakerServer = new Server(new TcpServer());
        matchMakerServer.Start(matchMakerStartOptions.Port, matchMakerStartOptions.MaxClientCount);
        var serverWrapper = new ServerWrapper(matchMakerServer);
        
        
        var controller = new QueuesController(
            new List<IGameModeQueue>
            {
                new DuelModeQueue()
            }, 
            new MatchSender(serverWrapper), 
            new HttpIdRetriever(authBackendConnectionOptions.Url, httpClientWrapper), //TODO: This must be replaced with an actual id retriever
            new CancelationResultSender(serverWrapper)
            );
        
        RequestGameMessageHandler.SetAccepter(controller);
        CancelGameMessageHandler.SetAccepter(controller);

        while (true)
        {
            try
            {
                matchMakerServer.Update();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}