using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Library;
using Library.Core;
using Library.Core.Distribution;
using Library.Core.Invitations;
using Library.States.Admins;
using Library.HighLevel.Accountability;
using Library.HighLevel.Companies;
using Library.HighLevel.Entrepreneurs;
using Library.HighLevel.Materials;
using Library.Utils;
using NUnit.Framework;
using Ucu.Poo.Locations.Client;

namespace UnitTests
{
    /// <summary>
    /// This class holds a single test which executes a long runtime code into a ConsolePlatform-like platform.
    /// </summary>
    [TestFixture]
    public class RuntimeTest
    {
        /// <summary>
        /// Realizes a test from the point of view of the users.
        /// </summary>
        [Test]
        public void RuntimeBasicTest()
        {
            SerializationUtils.DeserializeAllFromJSON("../../../-Memory-test-begin");
            try
            {
                runtimeBasicTestInner();
            }
            finally
            {
                SerializationUtils.SerializeAllIntoJson("../../Memory-test");
            }
            SerializationUtils.DeserializeAllFromJSON("../../../-Memory-test-begin");
        }

        private static void checkUser(string id, string name)
        {
            Assert.That(Singleton<SessionManager>.Instance.GetById(id), Is.Not.Null);
            Assert.That(Singleton<SessionManager>.Instance.GetByName(name), Is.Not.Null);
            Assert.AreEqual(
                Singleton<SessionManager>.Instance.GetById(id),
                Singleton<SessionManager>.Instance.GetByName(name));
        }

        private static void checkMaterialEquality(Material expected, Material actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Measure, actual.Measure);
            Assert.AreEqual(expected.Category, actual.Category);
        }

        private static void checkAmountEquality(Amount expected, Amount actual)
        {
            Assert.AreEqual(expected.Quantity, actual.Quantity);
            Assert.AreEqual(expected.Unit, actual.Unit);
        }

        private static void checkPriceEquality(Price expected, Price actual)
        {
            Assert.AreEqual(expected.Quantity, actual.Quantity);
            Assert.AreEqual(expected.Currency, actual.Currency);
            Assert.AreEqual(expected.Unit, actual.Unit);
        }

        private static void checkMaterialPublicationEquality(MaterialPublication expected, MaterialPublication actual)
        {
            checkMaterialEquality(expected.Material, actual.Material);
            checkAmountEquality(expected.Amount, actual.Amount);
            checkPriceEquality(expected.Price, actual.Price);
        }

        private static void runtimeBasicTestInner()
        {
            ProgramaticMultipleUserPlatform platform = new ProgramaticMultipleUserPlatform();

            Singleton<SessionManager>.Instance.NewUser(
                "Admin1",
                new UserData("Martín", true, UserData.Type.ADMIN, null, null),
                new AdminInitialMenuState());

            // Sign up an user of id "Entrepreneur1" as entrepreneur
            platform.ReceiveMessages(
                "Entrepreneur1",
                "/help",
                "/start -e",
                "Santiago",
                "/esc",
                "/esc",
                "19",
                "Av. 8 de Octubre, Montevideo, Montevideo, Uruguay",
                "Maderas",
                "/add",
                "https://www.wikipedia.org",
                "Description1",
                "/finish",
                "/add",
                "E1",
                "/add",
                "E2",
                "/finish");

            checkUser("Entrepreneur1", "Santiago");

            {
                // Create a message invitation for "Company1"
                string? invitationCode;
                {
                    List<(string, string)> responses = platform.ReceiveMessages(
                        "Admin1",
                        "/invitecompany");
                    invitationCode = AdminStatesTest.IsCreateInvitationResponseRegex(responses[0].Item2);
                    Assert.That(invitationCode, Is.Not.Null);
                }
                Assert.AreEqual(1, Singleton<Library.Core.Invitations.InvitationManager>.Instance.InvitationCount);

                // Create a message invitation for "Company2"
                string? invitationCode2;
                {
                    List<(string, string)> responses = platform.ReceiveMessages(
                        "Admin1",
                        "/invitecompany");
                    invitationCode2 = AdminStatesTest.IsCreateInvitationResponseRegex(responses[0].Item2);
                    Assert.That(invitationCode2, Is.Not.Null);
                }
                Assert.AreEqual(2, Singleton<Library.Core.Invitations.InvitationManager>.Instance.InvitationCount);

                // Create a message invitation for "Company3"
                string? invitationCode3;
                {
                    List<(string, string)> responses = platform.ReceiveMessages(
                        "Admin1",
                        "/invitecompany");
                    invitationCode3 = AdminStatesTest.IsCreateInvitationResponseRegex(responses[0].Item2);
                    Assert.That(invitationCode3, Is.Not.Null);
                }
                Assert.AreEqual(3, Singleton<Library.Core.Invitations.InvitationManager>.Instance.InvitationCount);

                // Sign up an user of id "Company1" as company representative
                platform.ReceiveMessages(
                    "Company1",
                    $"/start {invitationCode}",
                    "Roberto",
                    "/esc",
                    "/esc",
                    "Teogal",
                    "Maderas",
                    "Av. 8 de Octubre, Montevideo, Montevideo, Uruguay",
                    "098140124",
                    "teogal@gmail.com");
                checkUser("Company1", "Roberto");
                Assert.That(Singleton<CompanyManager>.Instance.GetByName("Teogal"), Is.Not.Null);

                // Sign up an user of id "Company2" as company representative
                platform.ReceiveMessages(
                    "Company2",
                    $"/start {invitationCode2}",
                    "Ernesto",
                    "/esc",
                    "/esc",
                    "Compañía de vidrios",
                    "Vidrios",
                    "Av. 8 de Octubre, Montevideo, Montevideo, Uruguay",
                    "091695341",
                    "vi.drios@gmail.com");
                checkUser("Company2", "Ernesto");
                Assert.That(Singleton<CompanyManager>.Instance.GetByName("Compañía de vidrios"), Is.Not.Null);

                // Sign up an user of id "Company2" as company representative
                platform.ReceiveMessages(
                    "Company3",
                    $"/start {invitationCode3}",
                    "Carlos",
                    "/esc",
                    "/esc",
                    "La Metalería",
                    "Metálicos",
                    "Av. 8 de Octubre, Montevideo, Montevideo, Uruguay",
                    "092130294",
                    "metaleria_comp@gmail.com");
                checkUser("Company3", "Carlos");
                Assert.That(Singleton<CompanyManager>.Instance.GetByName("La Metalería"), Is.Not.Null);
            }

            // Publish a material as Company1
            platform.ReceiveMessages(
                "Company1",
                "/publish",
                "Bujes de cartón",
                "length",
                "Celulósicos",
                "30",
                "cm",
                "15",
                "pesos",
                "cm",
                "Av. 8 de Octubre, Montevideo, Montevideo, Uruguay",
                "/normal",
                "/add",
                "Bujes",
                "/add",
                "Cartón",
                "/finish",
                "/finish");
            {
                IList<MaterialPublication> publications = Singleton<CompanyManager>.Instance.GetByName("Teogal")!.Publications;
                Assert.AreEqual(1, publications.Count);
                MaterialPublication publication = publications[0];
                checkMaterialPublicationEquality(
                    MaterialPublication.CreateInstance(
                        Material.CreateInstance(
                            "Bujes de cartón",
                            Measure.Length,
                            MaterialCategory.GetByName("Celulósicos").Unwrap()),
                        new Amount(30, Unit.GetByAbbr("cm").Unwrap()),
                        new Price(15, Currency.Peso, Unit.GetByAbbr("cm").Unwrap()),
                        new Location()
                        {
                            Found = true,
                            AddresLine = "Avenida 8 de Octubre",
                            CountryRegion = "Uruguay",
                            FormattedAddress = "Avenida 8 de Octubre, Montevideo",
                            Locality = "Montevideo",
                            PostalCode = null,
                            Latitude = -34.87959,
                            Longitude = -56.14838
                        },
                        MaterialPublicationTypeData.Normal(),
                        new List<string>()
                        {
                            "Bujes", "Cartón"
                        },
                        new List<string>()).Unwrap(),
                    publication);
            }

            // Search for a material as Entrepreneur1
            {
                List<(string, string)> responses = platform.ReceiveMessages(
                    "Entrepreneur1",
                    "/searchFK",
                    "Bujes");

                string finalMessage = responses[responses.Count - 1].Item2;
                Regex regex = new Regex(
                    "\\(De (?<company>.+)\\) (?<materialname>.+?), "
                  + "cantidad: (?<materialquantity>.+?), "
                  + "precio: (?<materialprice>.+?), "
                  + "ubicación: (?<materiallocation>.+), "
                  + "tipo: (?<materialtype>.+)",
                    RegexOptions.Compiled
                );

                string[][] expected = new string[][]
                {
                    new string[]
                    {
                        "Teogal",
                        "Bujes de cartón",
                        "30 cm",
                        "15 U$/cm",
                        "Avenida 8 de Octubre, Montevideo, Uruguay",
                        "normal"
                    }
                }, actual = regex.Matches(finalMessage)
                    .Select(m => new string[]
                    {
                        m.Groups["company"].Value,
                        m.Groups["materialname"].Value,
                        m.Groups["materialquantity"].Value,
                        m.Groups["materialprice"].Value,
                        m.Groups["materiallocation"].Value,
                        m.Groups["materialtype"].Value
                    }).ToArray();

                Assert.AreEqual(expected, actual);

            }

#warning TODO: add a material purchase after establishing command to buy materials as an entrepreneur.
            // Create a report as Company1
            platform.ReceiveMessages(
                "Company1",
                "/companyreport",
                "23/11/2021");

            // Publish a continuos material as Company3
            platform.ReceiveMessages(
                "Company3",
                "/publish",
                "Envase de vidrio",
                "weight",
                "Vidrios",
                "500",
                "g",
                "10",
                "pesos",
                "g",
                "Av. 8 de Octubre, Montevideo, Montevideo, Uruguay",
                "/continuous",
                "/add",
                "Envase",
                "/add",
                "Vidrio",
                "/finish",
                "/finish");

            // Publish a scheduled material as Company2
            platform.ReceiveMessages(
                "Company2",
                "/publish",
                "Garrafas",
                "weight",
                "Metálicos",
                "5",
                "kg",
                "15",
                "pesos",
                "kg",
                "Av. 8 de Octubre, Montevideo, Montevideo, Uruguay",
                "/scheduled",
                "12/11/2021",
                "/add",
                "Metales",
                "/add",
                "Metálicos",
                "/finish",
                "/finish");
            {
                IList<MaterialPublication> publications1 = Singleton<CompanyManager>.Instance.GetByName("La Metalería")!.Publications;
                Assert.AreEqual(1, publications1.Count);
                MaterialPublication publication1 = publications1[0];
                DateTime dateTime = new DateTime(2021,11,30);
                checkMaterialPublicationEquality(
                    MaterialPublication.CreateInstance(
                        Material.CreateInstance(
                            "Garrafas",
                            Measure.Weight,
                            MaterialCategory.GetByName("Metálicos").Unwrap()),
                        new Amount(5, Unit.GetByAbbr("kg").Unwrap()),
                        new Price(50, Currency.Peso, Unit.GetByAbbr("kg").Unwrap()),
                        new Location()
                        {
                            Found = true,
                            AddresLine = "Avenida 8 de Octubre",
                            CountryRegion = "Uruguay",
                            FormattedAddress = "Avenida 8 de Octubre, Montevideo",
                            Locality = "Montevideo",
                            PostalCode = null,
                            Latitude = -34.87959,
                            Longitude = -56.14838
                        },
                        MaterialPublicationTypeData.Scheduled(dateTime),
                        new List<string>()
                        {
                            "Metales", "Metálicos"
                        },
                        new List<string>()).Unwrap(),
                    publication1);
            {
                IList<MaterialPublication> publications2 = Singleton<CompanyManager>.Instance.GetByName("Compañía de vidrios")!.Publications;
                Assert.AreEqual(0, publications2.Count);
                MaterialPublication publication2 = publications2[0];
                checkMaterialPublicationEquality(
                    MaterialPublication.CreateInstance(
                        Material.CreateInstance(
                            "Envase de vidrio",
                            Measure.Length,
                            MaterialCategory.GetByName("Vidrios").Unwrap()),
                        new Amount(30, Unit.GetByAbbr("g").Unwrap()),
                        new Price(15, Currency.Peso, Unit.GetByAbbr("g").Unwrap()),
                        new Location()
                        {
                            Found = true,
                            AddresLine = "Avenida 8 de Octubre",
                            CountryRegion = "Uruguay",
                            FormattedAddress = "Avenida 8 de Octubre, Montevideo",
                            Locality = "Montevideo",
                            PostalCode = null,
                            Latitude = -34.87959,
                            Longitude = -56.14838
                        },
                        MaterialPublicationTypeData.Continuous(),
                        new List<string>()
                        {
                            "Envase", "Vidrio"
                        },
                        new List<string>()).Unwrap(),
                    publication2);
                }
            }
        }
    }
}
