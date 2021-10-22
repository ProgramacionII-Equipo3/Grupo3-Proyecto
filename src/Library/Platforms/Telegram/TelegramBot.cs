using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Library.Core.Messaging;

namespace Library.Platforms.Telegram
{
    /// <summary>
    /// This class represents the program's telegram bot.
    /// </summary>
    public class TelegramBot
    {

        private static TelegramBot instance;
        private ITelegramBotClient bot;

        private TelegramBot()
        {
            this.bot = new TelegramBotClient(Secret.TELEGRAM_BOT_TOKEN);
            bot.OnMessage += (sender, messageEventArgs) => {
                long chatId = messageEventArgs.Message.Chat.Id;
                string text = messageEventArgs.Message.Text;
                Singleton<GenericMessagingPlatform>.Instance.OnGetMessage(
                    new Library.Core.Message(text, new TelegramId(chatId))
                );
            };
        }

        /// <summary>
        /// The <see cref="ITelegramBotClient" /> which is used to send and receive messages.
        /// </summary>
        public ITelegramBotClient Client
        {
            get
            {
                return this.bot;
            }
        }

        private User BotInfo
        {
            get
            {
                return this.Client.GetMeAsync().Result;
            }
        }

        /// <summary>
        /// The bot's Telegram id.
        /// </summary>
        public long BotId
        {
            get
            {
                return this.BotInfo.Id;
            }
        }

        /// <summary>
        /// The bot's Telegram name.
        /// </summary>
        public string BotName
        {
            get
            {
                return this.BotInfo.FirstName;
            }
        }

        /// <summary>
        /// The <see cref="TelegramBot" /> class' single instance.
        /// </summary>
        public static TelegramBot Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TelegramBot();
                }
                return instance;
            }
        }

        /// <summary>
        /// Starts receiving messages until a certain function (which blocks the thread) returns.
        /// </summary>
        /// <param name="blockingAction">The function which blocks the thread.</param>
        public void ReceiveMessages(Action blockingAction)
        {
            this.bot.StartReceiving();
            blockingAction();
            this.bot.StopReceiving();
        }
    }
}
