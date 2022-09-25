using Discord;
using Discord.Commands;

namespace Discord_bot.Modules
{
    public class PongModule : ModuleBase<SocketCommandContext>
    {
        [Command("pong")]
        public async Task Pong()
        {
            await Context.Message.ReplyAsync("Pong!");
        }
    }
}
