using Library.Core;
using Library.Core.Messaging;
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
        /// <summary>
        /// The id of the user.
        /// </summary>
        public long ChatId { get; }

        /// <summary>
        /// Creates an instance of <see cref="TelegramId" />.
        /// </summary>
        /// <param name="chatId">The Telegram id.</param>
        public TelegramId(long chatId)
        {
            this.ChatId = chatId;
        }

        /// <summary>
        /// Checks the equality two <see cref="UserId" />s.
        /// </summary>
        /// <param name="other">The other id.</param>
        /// <returns>Whether the two ids are equal or not.</returns>
        public override bool Equals(UserId other) =>
            other is TelegramId otherTelegram && otherTelegram.ChatId == this.ChatId;

        /// <summary>
        /// Sends a message to a concrete Telegram user.
        /// </summary>
        /// <param name="msg">The message.</param>
        public override void SendMessage(string msg)
        {
            (TelegramBot.Instance as IMessageSender<long>).SendMessage(msg, this.ChatId);
        }
    }
}
