using Library.Core;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Library.Platforms.Telegram
{
    public class TelegramId: UserId
    {
        private long telegramId;
        public long ChatId => this.telegramId;

        public TelegramId(long chatId)
        {
            this.telegramId = chatId;
        }

        public override bool Equals(UserId other)
        {
            return other is TelegramId otherTelegram && otherTelegram.telegramId == this.telegramId;
        }

        public override async void SendMessage(string msg)
        {
            await TelegramBot.Instance.Client.SendTextMessageAsync(
                chatId: this.telegramId,
                text: msg
            );
        }
    }
}
