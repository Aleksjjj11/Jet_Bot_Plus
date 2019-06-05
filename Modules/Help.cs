using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Jet_Bot.Modules
{
    public class Help : ModuleBase<SocketCommandContext>
    {
        [Command("Help")]
        public async Task HelpAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("All commands for bot")
                .WithDescription("```css\n[Translate] <language> <message>\n{.: 'данная команда переводит введённое сообщение на выбранный язык.'}\n" +         
                "[T_en] <message>\n{.: 'данная команда переводит введённый текст с любого языка на русский.'}\n" +
                "[T_ru] <message>\n{.: 'данная команда переводит введённый текст с любого языка на английский.'}\n" +
                "[File]\n{.: 'данная команда переводит архив из 5000 слов (в разработке)'}\n"+
                "[S] <count_result> <request>\n{.: 'данная команда находит видео, каналы и плейлисты по заданному запросу(макс. знач. 50)'}\n" +
                "[infoping] <werbiste_url>\n{.:'узнать информацию о доступе к сайту.'}\n" +
                "```")
                .WithColor(7949007U)
                .WithImageUrl("https://images.ctfassets.net/awpxl2koull4/1pB4INeVpe2EOsoyOIak68/2ac6be9ffbba97e18e7da8b322568849/2652bc95-59e2-4283-b2b8-c832d656eca4.jpg?w=1200")
                .WithTimestamp(DateTimeOffset.Now);
            
            await ReplyAsync("", false, builder.Build());
            SocketGuildUser user = Context.User as SocketGuildUser;
            if (user.GuildPermissions.Administrator)
            {
                var dmChannel = await user.GetOrCreateDMChannelAsync();
                await dmChannel.SendMessageAsync("У вас есть права администратора, вы можете использовать команду 'comlist', чтобы увидеть команды для администраторов.");
            }
            //DataStorage.AddToStorage(Context.User.Username, Context.User.Id.ToString());
        }

        [Command("ComList")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task CommandListAdmin()
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("Command for admin:")
                .WithDescription("```css\n" +
                                 "[ban] <user_name> <reason> <count_day>\n{.: Выдать пользователю бан на n дней}\n" +
                                 "[kick] <user_name>\n{.: выгнать пользователя с сервера}\n" +
                                 "[warn] <user_name>\n{.: выдать предупреждение пользователю}\n" +
                                 "[delWarn] <user_name> <count>\n{.: снять предупреждение с пользователя}\n" +
                                 "[mute] <user_name>\n{.: выдать мут пользователю.}\n" +
                                 "[unmute] <user_name>\n{.:снять мут с пользователя}\n" +
                                 "[unban] <user_name>\n{.:снять бан с пользователя}\n" +
                                 "[rainbow] <role_name>\n{.:включить радужное сияние для роли(последующие использование бота не будет возможно)}\n" +
                                 "```")
                .WithImageUrl("https://steamuserimages-a.akamaihd.net/ugc/918042532939565723/7B92D92D4D645AC5409079A434DB9D518024F9B8/")
                .WithColor(new Color(152, 56, 209));
            await ReplyAsync("", false, builder.Build());
        }
    }
}