using System;
using Library;
using Library.Core;
using Library.Core.Invitations;
using Library.HighLevel.Companies;
using Library.Utils;
using NUnit.Framework;
using Ucu.Poo.Locations.Client;

namespace ProgramTests
{
    /// <summary>
    /// This test check if the class Company and CompanyManager and all methods of both classes works.
    /// </summary>
    [TestFixture]
    public class CompanyTest
    {
        /// <summary>
        /// Test Setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Test if a company can be created and added in list of all companies.
        /// </summary>
        [Test]
        public void CreateCompanyTest()
        {
            ContactInfo contactInfo = new ContactInfo("company@gmail.com", 094567142);
            using LocationApiClient client = new LocationApiClient();
            Location location = client.GetLocation("Luis Alberto de Herrera 776", "Minas", "Lavalleja", "Uruguay");
            Company company = Singleton<CompanyManager>.Instance.CreateCompany("Company", contactInfo, "heading", location)!;
            bool expected = Singleton<CompanyManager>.Instance.Companies.Contains(company);
            Assert.That(expected, Is.True);
            Singleton<CompanyManager>.Instance.RemoveCompany("Company");
        }

        /// <summary>
        /// This test check if a company can have representants.
        /// </summary>
        [Test]
        public void HasUserTest()
        {
            string id = "Telegram_2066298868";
            ContactInfo contactInfo = new ContactInfo("company@gmail.com", 094567142);
            using LocationApiClient client = new LocationApiClient();
            Location location = client.GetLocation("Luis Alberto de Herrera 777", "Minas", "Lavalleja", "Uruguay");
            Company company = new Company("Company", contactInfo, "heading", location);
            company.AddUser(id);
            bool expected = company.HasUser(id);

            Assert.That(expected, Is.True);
            client.Dispose();
        }

        /// <summary>
        /// This test check GetCompanyOf method of CompanyManager class.
        /// </summary>
        [Test]
        public void GetCompanyOfTest()
        {
            string id = "Telegram_2022597748";
            ContactInfo contactInfo = new ContactInfo("company@gmail.com", 094567142);
            using LocationApiClient client = new LocationApiClient();
            Location location = client.GetLocation("Luis Alberto de Herrera 776", "Minas", "Lavalleja", "Uruguay");
            Company company = Singleton<CompanyManager>.Instance.CreateCompany("Blue Patna", contactInfo, "Arroz", location)!;
            company.AddUser(id);
            Company expected = Singleton<CompanyManager>.Instance.GetCompanyOf(id)!;
            Assert.AreEqual(expected, company);
            Singleton<CompanyManager>.Instance.RemoveCompany("Blue Patna");
        }

        /// <summary>
        /// This test check GetByName method of CompanyManager class.
        /// </summary>
        [Test]
        public void GetByNameTest()
        {
#pragma warning disable 0219
            string id = "Telegram_2015598868";
#pragma warning restore 0219
            ContactInfo contactInfo = new ContactInfo("Woodcompany@gmail.com", 098567417);
            using LocationApiClient client = new LocationApiClient();
            Location location = client.GetLocation("Luis Alberto de Herrera 774", "Minas", "Lavalleja", "Uruguay");
            Company company = Singleton<CompanyManager>.Instance.CreateCompany("WoodCompany", contactInfo, "Madera", location)!;
            Company expected = Singleton<CompanyManager>.Instance.GetByName("WoodCompany")!;
            Assert.AreEqual(expected, company);
            Singleton<CompanyManager>.Instance.RemoveCompany("WoodCompany");
        }

        /// <summary>
        /// Tests the course of registering a company from user input.
        /// </summary>
        [Test]
        public void CompanyRegisterFromUserInput()
        {
            Singleton<InvitationManager>.Instance.CreateInvitation<CompanyInvitation>("4jsk", code => new CompanyInvitation(code));
            UnitTests.ProgramaticPlatform platform = new UnitTests.ProgramaticPlatform(
                "___",
                "/start 4jsk",
                "Teogal",
                "Maderas",
                "Av. 8 de Octubre, Montevideo, Montevideo, Uruguay",
                "098471724",
                "teogal@gmail.com",
                "/publish",
                "A",
                "length",
                "metales",
                "50cm",
                "30 U$/cm",
                "Av. 8 de Octubre, Montevideo, Montevideo, Uruguay",
                "/finish");
            platform.Run();
            Console.WriteLine();
            Console.WriteLine(String.Join("\n\t--------\n", platform.ReceivedMessages));
            Singleton<CompanyManager>.Instance.RemoveCompany("Teogal");
        }
    }
}