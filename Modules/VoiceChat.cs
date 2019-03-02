using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using YoutubeExtractor;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Audio;

namespace Jet_Bot.Modules
{
    public class VoiceChat : ModuleBase<SocketCommandContext>
    {
        private VideoDownloader downloader;
        private TaskCompletionSource<bool> DownloadFinishedEvent = new TaskCompletionSource<bool>();
        private VideoInfo Videoinfo;
        public string FilePath;
        public SocketVoiceChannel voiceChannel;
        
        
        [Command("JoinVoice")]
        public async Task JoinVoiceAsync(IVoiceChannel channel)
        { 
            voiceChannel = Context.Guild.GetVoiceChannel(channel.Id);
            await ReplyAsync(voiceChannel.Name + " ID: " + voiceChannel.Id.ToString());
            await voiceChannel.ConnectAsync();
        }

        [Command("InMe")]
        //Connect to voice channel with user
        public async Task JoinWithUser()
        { 
            IVoiceChannel voiceChannel = null;
            voiceChannel = (Context.User as IGuildUser).VoiceChannel;
            
            if (voiceChannel != null) 
            {
                DiscordSocketClient client = Context.Client;
                await ReplyAsync((Context.User as IGuildUser).Nickname + " Voice = " + voiceChannel.Name);
                //var audioClient = await voiceChannel.ConnectAsync();
                //var ffmpeg;
            } else await ReplyAsync("You are not in voice channel. Connect to voice channel, please.");
        }
        
        private static string GetMD5(string inputString) //Creates a MD5 hash from a input string
        {
            byte[] encodedPassword = new UTF8Encoding().GetBytes(inputString);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            string encoded = BitConverter.ToString(hash).Replace("-", string.Empty).ToUpper();
            return encoded;
        }

        [Command("Dictionary")]
        public async Task Leave_vAsync()
        {
            await Context.Channel.SendFileAsync("dictionary.txt");
        } 
        
        private async Task DownloadVideo() //Downloads a Youtube Video
        {
            downloader = new VideoDownloader(Videoinfo, FilePath);
            downloader.DownloadProgressChanged += (sender, args) => Console.WriteLine(args.ProgressPercentage);
            try
            {
                downloader.Execute();
                DownloadFinishedEvent.SetResult(true);
            }
            catch (Exception DownloadException)
            {
                Console.WriteLine(DownloadException);
            }
            await DownloadFinishedEvent.Task;
        }
    }
}