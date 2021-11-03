using NUnit.Framework;
using Library.Core;
using Library.HighLevel.Accountability;
using Library.HighLevel.Companies;
using Library.Core.Invitations;
using Library.Platforms.Telegram;

namespace ProgramTests
{
    /// <summary>
    /// This Test is for verificates if a Company can accept an invitation to the platform.
    /// </summary>
    public class AcceptInvitationTest
    {
        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        public void Setup()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void AcceptInvitation()
        {
            InvitationManager.CreateInvitation();
            TelegramId id = new TelegramId(2066298868);
            Message message = new Message(InvitationManager.invitations[0].Code, id);
            if (message.Text.Equals(InvitationManager.invitations[0].Code))
            {
                // If the message with the code is equal with te code sended in an invitation, 
                // the user can register the company
                ContactInfo contactInfo;
                contactInfo.Email = "companysa@gmail.com";
                contactInfo.PhoneNumber = 098765432;
                Location location = new Location("Av. 8 de Octubre 2738", "Montevideo", "Montevideo", "Uruguay");
                Company company = new Company("Company.SA", contactInfo, "Arroz", location);
                company.AddUser(message.Id);

                bool expected = company.HasUser(message.Id);
                // If the message with the code is equal with an invitation sended, the user has to 
                // be added in the representants list of the company.
                Assert.AreEqual(true, expected);
            }
            
            
            
        }
    }
}