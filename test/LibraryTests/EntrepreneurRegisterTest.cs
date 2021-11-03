using NUnit.Framework;
using System.Collections.Generic;
using Library.HighLevel.Entrepreneurs;
using Library.Core;
using Library.HighLevel.Materials;
using Library.Platforms.Telegram;
namespace ProgramTests
{
    /// <summary>
    /// 
    /// </summary>
    public class EntrepreneurRegisterTest
    {
        TelegramId juanId;
        Message nameMessage;
        Message ageMessage;
        Message locationMessage;
        Message headingMessage;
        Message habilitationsMessage;
        Message specializationsMessage;

       
        [SetUp]
        public void Setup()
        {
            juanId =  new TelegramId(2567104974);
            nameMessage = new Message("Juan", juanId);
            ageMessage = new Message("23", juanId);
            locationMessage = new Message("montecaseros", juanId);
            headingMessage = new Message("carpintero", juanId);
            habilitationsMessage = new Message("/command link1 link2",juanId);
            specializationsMessage = new Message("/command specialization1, specialization2", juanId);


        }

        /// <summary>
        /// This test evaluate if the entrepreneur is registered with their correct information.
        /// </summary>
        [Test]
        public void EntrepreneurRegister()
        {
            string[] habilitationsMessageSplitted = habilitationsMessage.Text.Trim().Split();
            List<Habilitation> habilitations = new List<Habilitation>();
            
            for (int i=1;i<habilitationsMessageSplitted.Length -1 ;i++)
            {
                Habilitation habilitation =  new Habilitation(habilitationsMessageSplitted[i]);
                habilitations.Add(habilitation);
            }

            string[] specializationMessageSplitted = habilitationsMessage.Text.Trim().Split();
            List<Specialization> specializations = new List<Specialization>();

            for (int i = 1; i < specializationMessageSplitted.Length; i++)
            {
                Specialization specialization = new Specialization(specializationMessageSplitted[i]);
                specializations.Add(specialization);
            }

            Entrepreneur juan = new Entrepreneur(juanId, nameMessage.Text, ageMessage.Text, locationMessage.Text, headingMessage.Text, habilitations, specializations );
            Entrepreneur.entrepeneurList.Add(juanId);

            /// <summary>
            /// The user must be in the list of entrepreneurs to be registered.
            /// </summary>
            
            UserId idExpected = nameMessage.Id;
            int indexnameUser = Entrepreneur.entrepeneurList.IndexOf(nameMessage.Id);
            Assert.AreEqual(Entrepreneur.entrepeneurList[indexnameUser], idExpected);
            
            /// <summary>
            /// Evaluate if the habilitations, specializations and name are registered correctly.
            /// </summary>
            
            string nameExpected = nameMessage.Text;
            Assert.AreEqual(habilitations, juan.Habilitation);
            Assert.AreEqual(specializations,juan.Specialization);
            Assert.AreEqual(nameExpected,juan.Name);



        }
    }

}
