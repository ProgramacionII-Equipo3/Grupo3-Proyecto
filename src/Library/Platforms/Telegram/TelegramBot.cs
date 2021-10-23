using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Library.Core;
using Library.Core.Messaging;

namespace Library.Platforms.Telegram
{
    /// <summary>
    /// This class represents the program's telegram bot.
    /// </summary>
    public class TelegramBot : IMessageReceiver<long>, IMessageSender<long>
    {
        UserId IMessageReceiver<long>.GetUserId(long id) => new TelegramId(id);

        async void IMessageSender<long>.SendMessage(string msg, long id)
        {
            await TelegramBot.Instance.Client
                .SendTextMessageAsync(chatId: id, text: msg)
                .ConfigureAwait(true);
        }

        private TelegramBot()
        {
            this.Client = new TelegramBotClient(Secret.TELEGRAM_BOT_TOKEN);
            this.Client.OnMessage += (sender, messageEventArgs) =>
                (this as IMessageReceiver<long>).OnGetMessage(
                    messageEventArgs.Message.Text,
                    messageEventArgs.Message.Chat.Id
                );
        }

        /// <summary>
        /// The <see cref="ITelegramBotClient" /> which is used to send and receive messages.
        /// </summary>
        public readonly ITelegramBotClient Client;

        private User BotInfo => this.Client.GetMeAsync().Result;

        /// <summary>
        /// The bot's Telegram id.
        /// </summary>
        public long BotId => this.BotInfo.Id;

        /// <summary>
        /// The bot's Telegram name.
        /// </summary>
        public string BotName => this.BotInfo.FirstName;

        /// <summary>
        /// The <see cref="TelegramBot" /> class' single instance.
        /// </summary>
        public static readonly TelegramBot Instance = new TelegramBot();

        /// <summary>
        /// Starts receiving messages until a certain function (which blocks the thread) returns.
        /// </summary>
        /// <param name="blockingAction">The function which blocks the thread.</param>
        public void ReceiveMessages(Action blockingAction)
        {
            this.Client.StartReceiving();
            blockingAction();
            this.Client.StopReceiving();
        }
    }
}
