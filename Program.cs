using Discord; 
using Microsoft.Extensions.DependencyInjection; 
using System;
using System.IO;
using System.Reflection; 
using System.Threading.Tasks;

using Discord.Commands;
using Discord.WebSocket;
using Jet_Bot.Core.Accounts;
using Jet_Bot.Modules;

namespace Jet_Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            string credentail_path = @"/home/arekusei/Документы/Jet_Bot/Jet-Bot-3cec51a5b6b1.json";
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentail_path);
            string debug = System.Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
            Program program = new Program();
            //Console.WriteLine(debug);
            program.RunBotAsync().GetAwaiter().GetResult();
        } 

        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services; 
        
        public static string OutputFolder = $"{Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar}videos";

        private async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            string BotToken = "NTA5NTgxNzA0NzgwODQwOTYx.DsRM9A.8zInNX-DXzEkTlmnvPjrJgHDhUY";

            //Event subscription
            _client.Log += Log;
            _client.UserJoined += AnnounceUserJoined;
            
            await RegisterCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, BotToken); 

            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task AnnounceUserJoined(SocketGuildUser user)
        {
            SocketGuild guild = user.Guild;
            SocketTextChannel channel = guild.DefaultChannel;
            await channel.SendMessageAsync($"Welcome in channel, {user.Mention}");
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        private async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            SocketUserMessage message = arg as SocketUserMessage;

            if (message is null || message.Author.IsBot) return;

            int argPos = 0;
            
            SocketCommandContext context = new SocketCommandContext(_client, message);
            
            //Mute check
            UserAccount userAccount = UserAccounts.GetAccount(context.User);
            if (userAccount.IsMuted)
            {
                await context.Message.DeleteAsync();
                IDMChannel dmChannelFromUserMute = await context.User.GetOrCreateDMChannelAsync();
                await dmChannelFromUserMute.SendMessageAsync("You have mute.");
                return;
            }

            if (ChatCorrecter.CheckMessageAsync(message))
            {
                await context.Message.DeleteAsync();
                IDMChannel dmChannelFromUserMute = await context.User.GetOrCreateDMChannelAsync();
                await dmChannelFromUserMute.SendMessageAsync("Your message had obscene language and was deleted.");
                return;
            }

            if (message.HasStringPrefix("!", ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _services);

                if (!result.IsSuccess)
                    Console.WriteLine(result.ErrorReason);
            }
            
            UserAccounts.GetAccount(context.User); //Don't working исправить UsersAccounts либо полность перейти на Users
        } 
    }
} 