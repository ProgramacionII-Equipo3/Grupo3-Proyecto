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
            /* TelegramBot telegramBot = TelegramBot.Instance;
            Console.WriteLine($"Hola soy el Bot de P2, mi nombre es {telegramBot.BotName} y tengo el Identificador {telegramBot.BotId}");
            telegramBot.ReceiveMessages(
                () =>
                {
                    Console.WriteLine("Escribe una línea para terminar");
                    Console.ReadLine();
                }
            ); */
            if(args.Length != 1) throw new Exception("One command-line argument expected.");
            switch(args[0])
            {
                case "--admin":
                    Singleton<SessionManager>.Instance.NewUser("ConsoleID", new UserData("console user", true, UserData.Type.ADMIN, null, null), new Library.States.Admins.AdminInitialMenuState());
                    break;
                case "--entrepreneur":
                    break;
                case "--company":
                    Singleton<InvitationManager>.Instance.CreateInvitation<Library.HighLevel.Companies.CompanyInvitation>("4jsk", code => new Library.HighLevel.Companies.CompanyInvitation(code));
                    break;
                default:
                    throw new Exception("Command-line argument expected to be \"--admin\", \"--entrepreneur\" or \"--company\"");
            }
            Console.WriteLine("----------");
            new ConsolePlatform().Run();
        }
    }
}
