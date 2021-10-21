using Library.Core;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Library.Platforms.Telegram
{
    /// <summary>
    /// This class represents <see cref="UserId" />s of Telegram users.
    /// </summary>
    public class TelegramId: UserId
    {
        private long telegramId;

        /// <summary>
        /// The id of the user.
        /// </summary>
        public long ChatId => this.telegramId;

        /// <summary>
        /// Creates a <see cref="TelegramId" />.
        /// </summary>
        /// <param name="chatId">The Telegram id.</param>
        public TelegramId(long chatId)
        {
            this.telegramId = chatId;
        }

        /// <summary>
        /// Compares the equality of two <see cref="UserId" />s.
        /// </summary>
        /// <param name="other">The other id.</param>
        /// <returns>Whether the two ids are equal or not.</returns>
        public override bool Equals(UserId other)
        {
            return other is TelegramId otherTelegram && otherTelegram.telegramId == this.telegramId;
        }

        /// <summary>
        /// Sends a message to a concrete Telegram user.
        /// </summary>
        /// <param name="msg">The message.</param>
        public override async void SendMessage(string msg)
        {
            await TelegramBot.Instance.Client.SendTextMessageAsync(
                chatId: this.telegramId,
                text: msg
            );
        }
    }
}
