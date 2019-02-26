using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Rest;

namespace Jet_Bot.Modules
{
    public class Troll : ModuleBase<SocketCommandContext>
    {
        [Command("Trolling")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireBotPermission(GuildPermission.Administrator)]
        public async Task TrollingAsync(IGuildUser user, SocketVoiceChannel channel, int timeTroll = 30)
        {
            DateTime finishTime = System.DateTime.Now.AddSeconds(timeTroll);

            while (System.DateTime.Now.CompareTo(finishTime) < 0)
            {
                await Context.Channel.TriggerTypingAsync();

                if (user != null && channel != null)
                {
                    await user?.ModifyAsync(x =>
                    {
                        x.Channel = channel;
                    });
                } else 
                {
                    Console.WriteLine("Error");
                    return;
                }

            }
        }
    }
}