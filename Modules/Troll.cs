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

            SocketGuild guild = Context.Guild;

            SocketVoiceChannel voiceTroll1 = guild.GetVoiceChannel(552915482080968725);
            SocketVoiceChannel voiceTroll2 = guild.GetVoiceChannel(552915483142389763);

            while (System.DateTime.Now.CompareTo(finishTime) < 0)
            {
                await Context.Channel.TriggerTypingAsync();

                if (user != null && channel != null)
                {
                    await user?.ModifyAsync(x =>
                    {
                        x.Channel = voiceTroll1;
                    });
                    await Task.Delay(1000);
                    await user?.ModifyAsync(x =>
                    {
                        x.Channel = voiceTroll2;
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