using System;
using System.Collections.Generic;
using Library;
using Library.Core;
using Library.Core.Invitations;
using Library.HighLevel.Companies;
using Library.Utils;
using NUnit.Framework;
using ProgramTests.Utils;
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
            Location location = client.GetLocationResilient("Luis Alberto de Herrera 776", "Minas", "Lavalleja", "Uruguay");
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
            Location location = client.GetLocationResilient("Luis Alberto de Herrera 777", "Minas", "Lavalleja", "Uruguay");
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
            Location location = client.GetLocationResilient("Luis Alberto de Herrera 776", "Minas", "Lavalleja", "Uruguay");
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
            Location location = client.GetLocationResilient("Luis Alberto de Herrera 774", "Minas", "Lavalleja", "Uruguay");
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
            ProgramTests.ProgramaticPlatform platform = new ProgramTests.ProgramaticPlatform(
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

        /// <summary>
        /// Tests the user story of creating a company from an invitation.
        /// </summary>
        [Test]
        public void CreateCompanyStoryTest()
        {
            RuntimeTest.BasicRuntimeTest("create-company", () =>
            {
                {
                    // Sign up an user of id "Company1" as company representative
                    Singleton<ProgramaticMultipleUserPlatform>.Instance.ReceiveMessages(
                        "Company1",
                        $"/start n1AIPqHy",
                        "Roberto",
                        "/esc",
                        "/esc",
                        "Teogal",
                        "Maderas",
                        "Av. 8 de Octubre, Montevideo, Montevideo, Uruguay",
                        "098140124",
                        "teogal@gmail.com");
                    CheckUtils.CheckUserAndEquality(new UserData("Roberto", true, UserData.Type.COMPANY, null, null), "Company1");
                    Company? company = Singleton<CompanyManager>.Instance.GetByName("Teogal");
                    Assert.That(company, Is.Not.Null);
                    Assert.Contains("Company1", company!.Representants);
                }

                {
                    // Sign up an user of id "Company2" as company representative
                    Singleton<ProgramaticMultipleUserPlatform>.Instance.ReceiveMessages(
                        "Company2",
                        $"/start pYzsMjCB",
                        "Ernesto",
                        "/esc",
                        "/esc",
                        "Compañía de vidrios",
                        "Vidrios",
                        "Av. 8 de Octubre, Montevideo, Montevideo, Uruguay",
                        "091695341",
                        "vi.drios@gmail.com");
                    CheckUtils.CheckUserAndEquality(new UserData("Ernesto", true, UserData.Type.COMPANY, null, null), "Company2");
                    Company? company = Singleton<CompanyManager>.Instance.GetByName("Compañía de vidrios");
                    Assert.That(company, Is.Not.Null);
                    Assert.Contains("Company2", company!.Representants);
                }

                {
                    // Sign up an user of id "Company2" as company representative
                    Singleton<ProgramaticMultipleUserPlatform>.Instance.ReceiveMessages(
                        "Company3",
                        $"/start hVvI3DGe",
                        "Carlos",
                        "/esc",
                        "/esc",
                        "La Metalería",
                        "Metálicos",
                        "Av. 8 de Octubre, Montevideo, Montevideo, Uruguay",
                        "092130294",
                        "metaleria_comp@gmail.com");
                    CheckUtils.CheckUserAndEquality(new UserData("Carlos", true, UserData.Type.COMPANY, null, null), "Company3");
                    Company? company = Singleton<CompanyManager>.Instance.GetByName("La Metalería");
                    Assert.That(company, Is.Not.Null);
                    Assert.Contains("Company3", company!.Representants);
                }
            });
        }

        /// <summary>
        /// Tests the user story of adding a company representative to an already existing company.
        /// </summary>
        [Test]
        public void AddCompanyRepresentativeTest()
        {
            RuntimeTest.BasicRuntimeTest("add-representative-to-company", () =>
            {
                // Sign up an user of id "Company1" as company representative
                Singleton<ProgramaticMultipleUserPlatform>.Instance.ReceiveMessages(
                    "Company2",
                    $"/start 01010101",
                    "José",
                    "/esc",
                    "jose@gmail.com",
                    "Teogal",
                    "Sí");
                CheckUtils.CheckUserAndEquality(new UserData("José", true, UserData.Type.COMPANY, "jose@gmail.com", null), "Company2");
                Company? company = Singleton<CompanyManager>.Instance.GetByName("Teogal");
                Assert.That(company, Is.Not.Null);
                Assert.AreEqual(new List<string>()
                {
                    "Company1", "Company2"
                }, company!.Representants);
            });
        }

        /// <summary>
        /// Tests the user story of remove a company by an admin.
        /// </summary>
        [Test]
        public void RemoveCopanyTest()
        {
            RuntimeTest.BasicRuntimeTest("remove-company", () =>
            {
                Singleton<ProgramaticMultipleUserPlatform>.Instance.ReceiveMessages(
                    "Admin1",
                    "/removecompany",
                    "Company2"
                );
                bool actual2= Singleton<CompanyManager>.Instance.RemoveCompany("Company2");
                Assert.That(actual2, Is.False);
            });
        }
    }
}