using System;
using System.Collections.Generic;
using Library;
using Library.Core;
using Library.HighLevel.Accountability;
using Library.HighLevel.Companies;
using Library.HighLevel.Materials;
using Library.Utils;
using NUnit.Framework;
using Ucu.Poo.Locations.Client;
using ProgramTests.Utils;

namespace ProgramTests
{
    /// <summary>
    /// Tests if a company can publish an offer for entrepreneurs.
    /// </summary>
    [TestFixture]
    public class PublishOfferTest
    {
        /// <summary>
        /// Setup config test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Test if the company publish the offer, the entrepreneur can see it.
        /// </summary>
        [Test]
        public void PublishOffer()
        {
            using LocationApiClient provider = new LocationApiClient();
            MaterialCategory category = MaterialCategory.GetByName("Otros").Unwrap();
            Unit unit = Unit.GetByAbbr("cm") !;
            Amount amount = new Amount(10, unit);
            Price price = new Price(100, Currency.Peso, unit);
            Location location = provider.GetLocationResilient("Luis Alberto de Herrera 776", "Minas", "Lavalleja", "Uruguay");
            IList<string> keyword = new List<string> { "Cámara" };
            IList<string> requirements = new List<string> { };
            Material material = Material.CreateInstance("Cámara de cubierta", Measure.Length, category);

            ContactInfo contact = new ContactInfo("evertec@gmail.com", 095456258);
            Company empresa = Singleton<CompanyManager>.Instance.CreateCompany("Evertec", contact, "Tecnología", location) !;
            empresa.PublishMaterial(material, amount, price, location, MaterialPublicationTypeData.Normal(), keyword, requirements);

            MaterialCategory category2 = MaterialCategory.GetByName("Plásticos").Unwrap();
            Amount amount2 = new Amount(5, unit);
            Price price2 = new Price(600, Currency.Peso, unit);
            Location location2 = provider.GetLocationResilient("Camino Maldonado km 11");
            IList<string> keyword2 = new List<string> { "Palet", "Plástico" };
            Material material2 = Material.CreateInstance("Palet de Plástico", Measure.Length, category2);

            Singleton<CompanyManager>.Instance.RemoveCompany("Evertec");
        }

        /// <summary>
        /// Test when a company not publish other offer.
        /// </summary>
        [Test]
        public void NotPublishOffer()
        {
            using LocationApiClient client = new LocationApiClient();
            MaterialCategory category3 = MaterialCategory.GetByName("Metales").Unwrap();

            Unit unit3 = Unit.GetByAbbr("kg") !;

            Amount amount3 = new Amount(3, unit3);
            Price price3 = new Price(250, Currency.Peso, unit3);
            Location location3 = client.GetLocationResilient("Av. 8 de Octubre 2738");
            IList<string> keywords = new List<string> { "metálicos", "metal", "residuos de contenedores" };
            IList<string> requirements = new List<string> { };
            Material material3 = Material.CreateInstance("Residuos generados de reparaciones de contenedores", Measure.Weight, category3);
            MaterialPublication.CreateInstance(material3, amount3, price3, location3, MaterialPublicationTypeData.Normal(), keywords, requirements);
            List<MaterialPublication> expected2 = new List<MaterialPublication>();
        }

        /// <summary>
        /// Tests the user story of publishing a normal material.
        /// </summary>
        [Test]
        public void PublishNormalMaterialTest()
        {
            RuntimeTest.BasicRuntimeTest("publish-normal-material", () =>
            {
                // Publish a material as Company1
                Singleton<ProgramaticMultipleUserPlatform>.Instance.ReceiveMessages(
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
                    CheckUtils.CheckMaterialPublicationEquality(
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
            });
        }

        /// <summary>
        /// Tests the user story of publishing a continuous material.
        /// </summary>
        [Test]
        public void PublishContinuousMaterialTest()
        {
            RuntimeTest.BasicRuntimeTest("publish-continuous-material", () =>
            {
                Singleton<ProgramaticMultipleUserPlatform>.Instance.ReceiveMessages(
                    "Company3",
                    "/publish",
                    "Envase de vidrio",
                    "weight",
                    "Vidrio",
                    "500g",
                    "10 U$/g",
                    "Av. 8 de Octubre, Montevideo, Montevideo, Uruguay",
                    "/continuous",
                    "/add",
                    "Envase",
                    "/add",
                    "Vidrio",
                    "/finish",
                    "/finish");

                IList<MaterialPublication> publications = Singleton<CompanyManager>.Instance.GetByName("La Metalería")!.Publications;
                Assert.AreEqual(1, publications.Count);
                MaterialPublication publication = publications[0];
                CheckUtils.CheckMaterialPublicationEquality(
                    MaterialPublication.CreateInstance(
                        Material.CreateInstance(
                            "Envase de vidrio",
                            Measure.Weight,
                            MaterialCategory.GetByName("Vidrio").Unwrap()),
                        new Amount(500, Unit.GetByAbbr("g").Unwrap()),
                        new Price(10, Currency.Peso, Unit.GetByAbbr("g").Unwrap()),
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
                    publication);
            });

        }

        /// <summary>
        /// Tests the user story of publishing a scheduled material.
        /// </summary>
        [Test]
        public void PublishScheduledMaterialTest()
        {
            RuntimeTest.BasicRuntimeTest("publish-scheduled-material", () =>
            {
                Singleton<ProgramaticMultipleUserPlatform>.Instance.ReceiveMessages(
                    "Company2",
                    "/publish",
                    "Garrafas",
                    "weight",
                    "Metales",
                    "5kg",
                    "15 U$/kg",
                    "Av. 8 de Octubre, Montevideo, Montevideo, Uruguay",
                    "/scheduled",
                    "12/11/2021",
                    "/add",
                    "Metales",
                    "/add",
                    "Metálicos",
                    "/finish",
                    "/finish");

                IList<MaterialPublication> publications2 = Singleton<CompanyManager>.Instance.GetByName("Compañía de vidrios")!.Publications;
                Assert.AreEqual(1, publications2.Count);
                MaterialPublication publication2 = publications2[0];
                CheckUtils.CheckMaterialPublicationEquality(
                    MaterialPublication.CreateInstance(
                        Material.CreateInstance(
                            "Garrafas",
                            Measure.Weight,
                            MaterialCategory.GetByName("Metales").Unwrap()),
                        new Amount(5, Unit.GetByAbbr("kg").Unwrap()),
                        new Price(15, Currency.Peso, Unit.GetByAbbr("kg").Unwrap()),
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
                        MaterialPublicationTypeData.Scheduled(new DateTime(2021, 11, 30)),
                        new List<string>()
                        {
                            "Metales", "Metálicos"
                        },
                        new List<string>()).Unwrap(),
                    publication2);
            });
        }
    }
}
