using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Library.Platforms.Telegram;
using System.IO;
using System.Text;

namespace Program
{
    class Program
    {
        public static void Main()
        {
            TelegramBot telegramBot = TelegramBot.Instance;
            Console.WriteLine($"Hola soy el Bot de P2, mi nombre es {telegramBot.BotName} y tengo el Identificador {telegramBot.BotId}");
            telegramBot.ReceiveMessages(() => {
                Console.WriteLine("Escribe una línea para terminar");
                Console.ReadLine();
            });
        }
    }
}
