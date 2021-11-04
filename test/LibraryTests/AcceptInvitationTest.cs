using NUnit.Framework;
using Library.Core;
using System.Collections.Generic;
using Ucu.Poo.Locations.Client;
using Library.HighLevel.Companies;
using Library.Core.Invitations;
using Library.Platforms.Telegram;
using Library.HighLevel.Entrepreneurs;
using Library.HighLevel.Materials;

namespace ProgramTests
{
    /// <summary>
    /// This Test is for verificates if a Company can accept an invitation to the platform.
    /// </summary>
    public class AcceptInvitationTest
    {
        /// <summary>
        /// Test setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {           
        }

        /// <summary>
        /// Test if a company can accept an invitation and register.
        /// </summary>
        [Test]
        public void AcceptInvitation()
        {
            InvitationManager.CreateInvitation();
            TelegramId id = new TelegramId(2066298868);
            // Message with the code.
            Message message = new Message("1234567", id);
            LocationApiClient provider = new LocationApiClient();
            // If the message with the code is equal with te code sended in an invitation, 
            // the user can register the company
            ContactInfo contactInfo;
            contactInfo.Email = "companysa@gmail.com";
            contactInfo.PhoneNumber = 098765432;
            Location location = provider.GetLocationAsync("Av. 8 de Octubre 2738", "Montevideo", "Montevideo", "Uruguay").Result;
            Company company = new Company("Company.SA", contactInfo, "Arroz", location);
            Company.AddCompany(company);
            company.AddUser(message.Id);

            bool expected = company.HasUser(message.Id);
            bool expected2 = Company.companiesCreated.Contains(company);
            // If the message with the code is equal with an invitation sended, the user has to 
            // be added in the representants list of the company. 
            // The company is registered.
            Assert.AreEqual(true, expected);
            Assert.AreEqual(true, expected2);
        }

        /// <summary>
        /// If the user don´t have a code, it´s user is an Entrepreneur.
        /// </summary>
        public void NotAcceptInvitation()
        {
            TelegramId id = new TelegramId(2066298868);
            Message message = new Message("", id);
            Habilitation habilitation = new Habilitation("Link1");
            Habilitation habilitation2 = new Habilitation("Link2");
            List<Habilitation> habilitations = new List<Habilitation> { habilitation, habilitation2 };
            Specialization specialization = new Specialization("Specialization1");
            Specialization specialization2 = new Specialization("Specialization2");
            List<Specialization> specializations = new List<Specialization> { specialization, specialization2 };

            LocationApiClient provider = new LocationApiClient();
            Location location = provider.GetLocationAsync("Av. 8 de Octubre 2738", "Montevideo", "Montevideo", "Uruguay").Result;
            Entrepreneur entrepreneur = new Entrepreneur(id, "Juan", "22", location, "Carpintero", habilitations, specializations);
            Entrepreneur.entrepeneurList.Add(message.Id);
            bool expected = Entrepreneur.entrepeneurList.Contains(message.Id);
            Assert.AreEqual(true, expected);

        }
    }
}