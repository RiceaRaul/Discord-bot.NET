using Common;
using Discord.Interactions;
using Discord.WebSocket;
using Discord_bot.Client.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Discord_bot.Client
{
    public static class Run
    {
        public static async Task RunDiscord(this IHost host)
        {
            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            var commands = provider.GetRequiredService<InteractionService>();
            var client = provider.GetRequiredService<DiscordSocketClient>();
            var configuration = provider.GetRequiredService<IConfigurationRoot>();
            var config = configuration.GetSection("AppSettings").Get<AppSettings>();

            await provider.GetRequiredService<InteractionHandler>().InitializeAsync();

            var prefixCommands = provider.GetRequiredService<PrefixHandler>();

            await prefixCommands.InitializeAsync();

            client.Log += async (msg) =>
            {
                await Task.CompletedTask;
                Console.WriteLine(msg);
            };

            client.Ready += async () =>
            {
                if (IsDebug())
                    await commands.RegisterCommandsToGuildAsync(config.Guild, true);
                else
                    await commands.RegisterCommandsGloballyAsync(true);
            };


            await client.LoginAsync(Discord.TokenType.Bot, config.Token);
            await client.StartAsync();

            await Task.Delay(-1);
        }

        static bool IsDebug()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }
}
