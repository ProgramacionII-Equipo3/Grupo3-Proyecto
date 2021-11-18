using Library;
using Library.Core;
using Library.HighLevel.Companies;
using Library.Platforms.Telegram;
using NUnit.Framework;
using Ucu.Poo.Locations.Client;

namespace ProgramTests
{
    /// <summary>
    /// This test check if the class Company and CompanyManager and all methods of both classes works.
    /// </summary>
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
            ContactInfo contactInfo;
            contactInfo.Email = "company@gmail.com";
            contactInfo.PhoneNumber = 094567142;
            LocationApiClient client = new LocationApiClient();
            Location location = client.GetLocationAsync("Luis Alberto de Herrera 776", "Minas", "Lavalleja", "Uruguay").Result;
            Company company = Singleton<CompanyManager>.Instance.CreateCompany("Company", contactInfo, "heading", location);
            bool expected = Singleton<CompanyManager>.Instance.Companies.Contains(company);
            Assert.That(expected, Is.True);
        }

        /// <summary>
        /// This test check if a company can have representants.
        /// </summary>
        [Test]
        public void HasUserTest()
        {
            string id = "Telegram_2066298868";
            ContactInfo contactInfo;
            contactInfo.Email = "company@gmail.com";
            contactInfo.PhoneNumber = 094567142;
            LocationApiClient client = new LocationApiClient();
            Location location = client.GetLocationAsync("Luis Alberto de Herrera 777", "Minas", "Lavalleja", "Uruguay").Result;
            Company company = new Company("Company", contactInfo, "heading", location);
            company.AddUser(id);
            bool expected = company.HasUser(id);

            Assert.That(expected, Is.True);
        }

        /// <summary>
        /// This test check GetCompanyOf method of CompanyManager class.
        /// </summary>
        [Test]
        public void GetCompanyOfTest()
        {
            string id = "Telegram_2022597748";
            ContactInfo contactInfo;
            contactInfo.Email = "company@gmail.com";
            contactInfo.PhoneNumber = 094567142;
            LocationApiClient client = new LocationApiClient();
            Location location = client.GetLocationAsync("Luis Alberto de Herrera 776", "Minas", "Lavalleja", "Uruguay").Result;
            Company company = Singleton<CompanyManager>.Instance.CreateCompany("Blue Patna", contactInfo, "Arroz", location);
            company.AddUser(id);
            Company expected = Singleton<CompanyManager>.Instance.GetCompanyOf(id);
            Assert.AreEqual(expected, company);
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
            ContactInfo contactInfo;
            contactInfo.Email = "Woodcompany@gmail.com";
            contactInfo.PhoneNumber = 094567417;
            LocationApiClient client = new LocationApiClient();
            Location location = client.GetLocationAsync("Luis Alberto de Herrera 774", "Minas", "Lavalleja", "Uruguay").Result;
            Company company = Singleton<CompanyManager>.Instance.CreateCompany("WoodCompany", contactInfo, "Madera", location);
            Company expected = Singleton<CompanyManager>.Instance.GetByName("WoodCompany");
            Assert.AreEqual(expected, company);
        }
    }
}