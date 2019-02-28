using System;
using System.Collections.Immutable;
using System.Numerics;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Jet_Bot.Modules
{
    public class InfoPing : ModuleBase<SocketCommandContext>
    {
        [Command("InfoPing")]
        public async Task PingAsync(string server = "176.59.207.127")
        {
            Ping ping = new System.Net.NetworkInformation.Ping();
            PingReply pingReply = pingReply = ping.Send(server);

            string info = server;
            if (pingReply.Status != IPStatus.TimedOut)
            {
                info += "\nAddress: " + pingReply.Address.ToString();
                info += "\nStatus: " + pingReply.Status.ToString();
                info += "\nRoundtrip Time: " + pingReply.RoundtripTime.ToString() + "ms";
                info += "\nSize buff: " + pingReply.Buffer.Length.ToString();
            } else 
            {
                info += "\nStatus: " + pingReply.Status.ToString();
            }
            EmbedBuilder builder = new EmbedBuilder();
            Random rand = new Random();

            builder.WithTitle("About of server")
                .WithDescription(info)
                .WithColor(new Color(rand.Next(255), rand.Next(255), rand.Next(255)))
                .WithTimestamp(DateTimeOffset.Now);
            
            await ReplyAsync("", false, builder.Build());
            
        }
        
        [Command("Trigger")]
        public async Task TrigerAsync()
        { 
            await Context.Channel.SendMessageAsync(Context.IsPrivate.ToString());
            //var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
            //await dmChannel.SendMessageAsync("Hi, how are you?");
        }
    }
}