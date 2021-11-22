using System.Collections.Generic;
using Library;
using Library.Core;
using Library.Utils;
using Library.HighLevel.Administers;
using Library.HighLevel.Companies;
using Library.HighLevel.Entrepreneurs;
using Library.HighLevel.Materials;
using NUnit.Framework;
using Ucu.Poo.Locations.Client;

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
            Administer.CreateCompanyInvitation();
            string id = "Telegram_2066298868";

            // Message with the code.
            Message message = new Message("1234567", id);
            LocationApiClient provider = new LocationApiClient();

            // If the message with the code is equal with te code sended in an invitation,
            // the user can register the company.
            ContactInfo contactInfo = new ContactInfo();
            contactInfo.Email = "companysa@gmail.com";
            contactInfo.PhoneNumber = 098765432;
            Location location = provider.GetLocation("Av. 8 de Octubre 2738", "Montevideo", "Montevideo", "Uruguay");
            Company company = Singleton<CompanyManager>.Instance.CreateCompany("Company.SA", contactInfo, "Arroz", location)!;
            company.AddUser(message.Id);

            bool expected = company.HasUser(message.Id);
            Company expectedCompany = Singleton<CompanyManager>.Instance.GetByName("Company.SA")!;

            // If the message with the code is equal with an invitation sended, the user has to
            // be added in the representants list of the company.
            // The company is registered.
            Assert.That(expected, Is.True);
            Assert.AreEqual(expectedCompany, company);
        }

        /// <summary>
        /// If the user don't have a code, its user is an Entrepreneur.
        /// </summary>
        [Test]
        public void NotAcceptInvitation()
        {
            string id = "Telegram_2066298868";
            Message message = new Message(string.Empty, id);
            Habilitation habilitation = new Habilitation("Link1", "description1");
            Habilitation habilitation2 = new Habilitation("Link2", "description2");
            IList<Habilitation> habilitations = new List<Habilitation> { habilitation, habilitation2 };
            string specialization = "specialization1";
            string specialization2 = "specialization2";
            IList<string> specializations = new List<string> { specialization, specialization2 };
            LocationApiClient provider = new LocationApiClient();
            Location location = provider.GetLocation("Av. 8 de Octubre 2738", "Montevideo", "Montevideo", "Uruguay");
            Entrepreneur entrepreneur = new Entrepreneur(id, "Juan", 22, location, "Carpintero", habilitations, specializations);
            Singleton<EntrepreneurManager>.Instance.NewEntrepreneur(entrepreneur);
            bool expected = Singleton<EntrepreneurManager>.Instance.Entrepreneurs.Contains(entrepreneur);
            Assert.That(expected, Is.True);
        }
    }
}