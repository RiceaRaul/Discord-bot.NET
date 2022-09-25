using Discord;
using Discord.Interactions;

namespace Discord_bot.Modules
{
    // Must use InteractionModuleBase<SocketInteractionContext> for the InteractionService to auto-register the commands
    public class PingModule : InteractionModuleBase<SocketInteractionContext>
    {
        public InteractionService Commands { get; set; }

        [SlashCommand("ping", "Receive a pong!")]
        public async Task Ping()
        {
            
            // Respond to the user
            await RespondAsync("pong");
        }
    }
}
