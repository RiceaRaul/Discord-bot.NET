using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Discord_bot.Client;
using Discord_bot.Client.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();

var configDiscord = new DiscordSocketConfig
{
    GatewayIntents = GatewayIntents.AllUnprivileged,
    LogGatewayIntentWarnings = false,
    AlwaysDownloadUsers = true,
    LogLevel = LogSeverity.Debug
};

IHost host = Host.CreateDefaultBuilder()
    .ConfigureServices((_, services) =>
    services
    .AddSingleton(config)

    .AddSingleton(x => new DiscordSocketClient(configDiscord))

    .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))

    .AddSingleton<InteractionHandler>()

    .AddSingleton(x => new CommandService(new CommandServiceConfig
    {
        LogLevel = LogSeverity.Debug,
        DefaultRunMode = Discord.Commands.RunMode.Async
    }))

    .AddSingleton<PrefixHandler>())
    .Build();

await host.RunDiscord();
 