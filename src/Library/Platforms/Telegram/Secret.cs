using System.IO;

namespace Library.Platforms.Telegram
{
    /// <summary>
    /// This class stores secret data whose access has to be carefully managed.
    /// </summary>
    public static class Secret
    {
        /// <summary>
        /// The bot's secret token.
        /// </summary>
        public static readonly string TELEGRAM_BOT_TOKEN = File.ReadAllText("../../secret.txt");
    }
}
