using NUnit.Framework;
using System.Collections.Generic;
using Library.HighLevel.Entrepreneurs;
using Library.Core;
using Library.HighLevel.Materials;
using Library.Platforms.Telegram;
using Ucu.Poo.Locations.Client;

namespace ProgramTests
{
    /// <summary>
    /// Test if an Entrepreneur can register into the platform.
    /// </summary>
    public class EntrepreneurRegisterTest
    {
        TelegramId juanId;
        Message nameMessage;
        Message ageMessage;
        LocationApiClient provider;
        Location location;
        Message headingMessage;
        Message habilitationsMessage;
        Message specializationsMessage;

        /// <summary>
        /// It´s create a message with the information correspondent.
        /// </summary>       
        [SetUp]
        public void Setup()
        {
            juanId =  new TelegramId(2567104974);
            nameMessage = new Message("Juan", juanId);
            ageMessage = new Message("23", juanId);
            headingMessage = new Message("carpintero", juanId);
            habilitationsMessage = new Message("/command link1 link2",juanId);
            specializationsMessage = new Message("/command specialization1, specialization2", juanId);
            provider = new LocationApiClient();
            location = provider.GetLocationAsync("Av. 8 de Octubre 2738").Result;


        }

        /// <summary>
        /// This test evaluate if the entrepreneur is registered with their correct information.
        /// </summary>
        [Test]
        public void EntrepreneurRegister()
        {
            string[] habilitationsMessageSplitted = habilitationsMessage.Text.Trim().Split();
            List<Habilitation> habilitations = new List<Habilitation>();
            
            for (int i = 1; i < habilitationsMessageSplitted.Length; i++)
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

            Entrepreneur juan = new Entrepreneur(juanId, nameMessage.Text, ageMessage.Text, location, headingMessage.Text, habilitations, specializations );
            Entrepreneur.entrepeneurList.Add(juanId);
            
           
            // The user must be in the list of entrepreneurs to be registered.
           
            
            UserId idExpected = nameMessage.Id;
            int indexnameUser = Entrepreneur.entrepeneurList.IndexOf(nameMessage.Id);
            Assert.AreEqual(Entrepreneur.entrepeneurList[indexnameUser], idExpected);
            
            
            // Evaluate if the habilitations, specializations and name are registered correctly.
            
            
            string nameExpected = nameMessage.Text;
            Assert.AreEqual(habilitations, juan.Habilitation);
            Assert.AreEqual(specializations,juan.Specialization);
            Assert.AreEqual(nameExpected,juan.Name);
        }
    }

}
