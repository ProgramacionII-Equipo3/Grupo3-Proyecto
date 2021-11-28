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
                    invitationCode = AdminStatesTest.IsCreateInvitationResponseRegex(responses[0].Item2); ;
                    Assert.That(invitationCode, Is.Not.Null);
                }
                Assert.AreEqual(1, Singleton<Library.Core.Invitations.InvitationManager>.Instance.InvitationCount);

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
            }

            // Publish a material as Company1
            platform.ReceiveMessages(
                "Company1",
                "/publish",
                "Bujes de cartón",
                "length",
                "Celulósicos",
                "30cm",
                "15 U$/cm",
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
        }
    }
}
