using System;
using System.IO;
using System.Text;
using Library;
using Library.Core;
using Library.Core.Invitations;
using Library.Core.Distribution;
using Library.Platforms.Telegram;
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
            Library.Utils.SerializationUtils.DeserializeAllFromJSON("../../Memory");
            Console.WriteLine("----------");
            new MultipleUsersConsolePlatform("ConsoleID").Run();
//            Library.Utils.SerializationUtils.SerializeAllIntoJSON("../../Memory");
        }
    }
}
