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
            Console.WriteLine("----------");
            new MultipleUsersConsolePlatform("ConsoleID").Run();
            (Singleton<Searcher>.Instance as IDisposable).Dispose();
            Library.Utils.SerializationUtils.SerializeAllIntoJson("../../Memory-end");
//            Library.Utils.SerializationUtils.SerializeAllIntoJSON("../../Memory");
        }
    }
}
