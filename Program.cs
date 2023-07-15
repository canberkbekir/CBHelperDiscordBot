using CBHelperDiscordBot.Models.BaseModels;
using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace DiscordBot
{
    public class Program
    {
        public static Task Main(string[] args) => new Program().MainAsync();

        private DiscordSocketClient? _client;
        private readonly string? _path = $@"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}\";

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;

            // Some alternative options would be to keep your token in an Environment Variable or a standalone file.
            // var token = Environment.GetEnvironmentVariable("NameOfYourEnvironmentVariable");
            // var token = File.ReadAllText("token.txt");
            var token = JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText(_path + "appSettings.json"))?.ApiKey;

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();


            // Block this task until the program is closed.
            await Task.Delay(-1);
        }


        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }


    }
}