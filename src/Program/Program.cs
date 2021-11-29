using System;
using Library;
using Library.Platforms.Telegram;

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
        /// <param name="args">The program arguments.</param>
        public static void Main(string[] args)
        {
            Library.Utils.SerializationUtils.DeserializeAllFromJSON("../../Memory");
            try
            {
                if (args.Length > 0 && (args[0] == "--console" || args[0] == "-c"))
                {
                    Console.WriteLine("Running via Console");
                    new MultipleUsersConsolePlatform("ConsoleID").Run();
                }
                else
                {
                    Console.WriteLine("Running via Telegram");
                    TelegramBot.Instance.ReceiveMessages(() =>
                    {
                        while (Console.ReadLine() != "--STOP") ;
                    });
                }
            } finally
            {
                Singleton<Ucu.Poo.Locations.Client.LocationApiClient>.Instance.Dispose();
                Library.Utils.SerializationUtils.SerializeAllIntoJson("../../Memory");
            }
        }
    }
}
