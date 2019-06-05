using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Jet_Bot.Modules
{
    public class RainBowRole : ModuleBase<SocketCommandContext>
    {
        [Command("Rainbow")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireBotPermission(GuildPermission.Administrator)]
        public async Task RainbowAsync([Remainder]IRole role)
        {
            DateTime finishTime = System.DateTime.Now.AddSeconds(200);
            byte r = role.Color.R, g = role.Color.G, b = role.Color.B;
            while (System.DateTime.Now.CompareTo(finishTime) < 0)
            {
                if ( r == 255 && g < 255 && b == 0 )
                {
                    g+=3;
                    if (g > 255) g = 255;
                }
                if ( g == 255 && r > 0 && b == 0 )
                {
                    r-=3;
                }
                if ( g == 255 && b < 255 && r == 0 )
                {
                    b+=3;
                    if (b > 255) b = 255;
                }
                if ( b == 255 && g > 0 && r == 0 )
                {
                    g-=3;
                }
                if ( b == 255 && r < 255 && g == 0 )
                {
                    r+=3;
                    if (r > 255) r = 255;
                }
                if ( r == 255 && b > 0 && g == 0 )
                {
                    b-=3;
                }
                await role.ModifyAsync(x => { x.Color = new Color(r, g, b); });
            }
            Console.WriteLine("Finished");
        }
    }
}