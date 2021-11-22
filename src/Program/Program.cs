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
            if(args.Length != 1) throw new Exception("One command-line argument expected.");
            switch(args[0].ToLower())
            {
                case "--admin":
                    Singleton<SessionManager>.Instance.NewUser("ConsoleID", new UserData("console user", true, UserData.Type.ADMIN, null, null), new Library.States.Admins.AdminInitialMenuState());
                    break;
                case "--entrepreneur":
                    break;
                case "--company":
                    Singleton<InvitationManager>.Instance.CreateInvitation<Library.HighLevel.Companies.CompanyInvitation>("4jsk", code => new Library.HighLevel.Companies.CompanyInvitation(code));
                    break;
                case "--json":
                    Library.Utils.SerializationUtils.DeserializeAllFromJSON("../../Memory");
                    break;
                default:
                    throw new Exception("Command-line argument expected to be \"--admin\", \"--entrepreneur\", \"--company\", or \"--JSON\"");
            }
            Console.WriteLine("----------");
            new ConsolePlatform().Run();
        }
    }
}
