using System;
using System.IO;
using System.Linq;
using System.Text;
using Library;
using Library.Core;
using Library.Core.Invitations;
using Library.Core.Distribution;
using Library.Platforms.Telegram;
using Library.HighLevel.Entrepreneurs;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Grupo3_Proyecto
{
    /// <summary>
    /// The main program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Executes the program.
        /// </summary>
        public static void Main(string[] args)
        {
            // Console.SetIn(new StringReader(
            //     string.Join(null,
            //         new string[]
            //         {
            //             "--STOP"
            //         }.Select(s => $"{s}\n"))));
            Library.Utils.SerializationUtils.DeserializeAllFromJSON("../../Memory");
            if(args.Length > 0 && (args[0] == "--console" || args[0] == "-c"))
            {
                Console.WriteLine("Running via Console");
                new MultipleUsersConsolePlatform("ConsoleID").Run();
            } else
            {
                Console.WriteLine("Running via Telegram");
                TelegramBot.Instance.ReceiveMessages(() =>
                {
                    while(Console.ReadLine() != "--STOP");
                });
            }
            Singleton<Ucu.Poo.Locations.Client.LocationApiClient>.Instance.Dispose();
            Library.Utils.SerializationUtils.SerializeAllIntoJson("../../Memory-end");
        }
    }
}
