using System.IO;

namespace Library.Platforms.Telegram
{
    public static class Secret
    {
        public static readonly string TELEGRAM_BOT_TOKEN = File.ReadAllText("../../secret.txt");
    }
}
