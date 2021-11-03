using NUnit.Framework;
using Library.HighLevel.Entrepreneurs;
using Library.Core;
using Library.Platforms.Telegram;
namespace ProgramTests
{
    /// <summary>
    /// 
    /// </summary>
    public class EntrepreneurRegisterTest
    {
        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        public void Setup()
        {
            
        }

        /// <summary>
        /// This test evaluate if the entrepreneur is register
        /// </summary>
        [Test]
        public void EntrepreneurRegister()
        {
            TelegramId juanId =  new TelegramId(2567104974);
            Message nameMessage = new Message("Juan", juanId);
            Message ageMessage = new Message("23", juanId);
            Message locationMessage = new Message("montecaseros", juanId);
            Message headingMessage = new Message("carpintero", juanId);
            Entrepreneur juan = new Entrepreneur(juanId, nameMessage.Text, ageMessage.Text, locationMessage.Text, headingMessage.Text);
            Entrepreneur.entrepeneurList.Add(juanId);

            /// <summary>
            /// The user must be in the list of entrepreneurs to be register
            /// </summary>
            UserId idExpected = headingMessage.Id;
            int indexUser = Entrepreneur.entrepeneurList.IndexOf(headingMessage.Id);
            Assert.AreEqual(Entrepreneur.entrepeneurList[indexUser], idExpected);

        }
    }

}
