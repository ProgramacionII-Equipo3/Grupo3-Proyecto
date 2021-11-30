using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Library;
using Library.HighLevel.Accountability;
using Library.HighLevel.Companies;
using Library.HighLevel.Entrepreneurs;
using Library.HighLevel.Materials;
using Library.Utils;
using NUnit.Framework;
using ProgramTests.Utils;
using Ucu.Poo.Locations.Client;

namespace ProgramTests
{
    /// <summary>
    /// This class holds tests about purchasing materials.
    /// </summary>
    public class PurchaseTest
    {
        /// <summary>
        /// Tests the user story of buying a material publication.
        /// </summary>
        [Test]
        public void BuyMaterialPublicationTest()
        {
            RuntimeTest.BasicRuntimeTest("buy-material-publication", () =>
            {
                List<(string, string)> responses = Singleton<ProgramaticMultipleUserPlatform>.Instance.ReceiveMessages(
                    "Entrepreneur1",
                    "/buy",
                    "Teogal",
                    "Bujes de cartón",
                    "30 cm",
                    "Sí");

                string finalMessage = responses[responses.Count - 1].Item2;
                Regex regex = new Regex(
                    "La compra se ha concretado, para coordinar el envío o el retiro del material, te envío la información de contacto de la empresa:\n"
                  + "Número Telefónico: (?<phonenumber>.+?)\n"
                  + "Correo Electrónico: (?<email>.+?)\n",
                    RegexOptions.Compiled
                );

                string[][] expected = new string[][]
                {
                    new string[]
                    {
                        "098140124",
                        "teogal@gmail.com"
                    }
                }, actual = regex.Matches(finalMessage)
                    .Select(m => new string[]
                    {
                        m.Groups["phonenumber"].Value,
                        m.Groups["email"].Value
                    }).ToArray();

                Assert.AreEqual(expected, actual);
            });
        }

        /// <summary>
        /// Tests the user story of purchasing a material.
        /// </summary>
        [Test]
        public void PurchaseMaterialTest()
        {
            RuntimeTest.BasicRuntimeTest("purchase-material", () =>
            {
                Singleton<ProgramaticMultipleUserPlatform>.Instance.ReceiveMessages(
                    "Entrepreneur1",
                    "/buy",
                    "Teogal",
                    "Bujes de cartón",
                    "2 cm",
                    "Sí");

                {
                    List<MaterialPublication> publications = Singleton<CompanyManager>.Instance.GetByName("Teogal")!.Publications;
                    Assert.AreEqual(1, publications.Count);
                    MaterialPublication publication = publications[0];
                    CheckUtils.CheckMaterialPublicationEquality(
                        MaterialPublication.CreateInstance(
                            Material.CreateInstance(
                                "Bujes de cartón",
                                Measure.Length,
                                MaterialCategory.GetByName("Celulósicos").Unwrap()),
                            new Amount(8, Unit.GetByAbbr("cm").Unwrap()),
                            new Price(15, Currency.Peso, Unit.GetByAbbr("cm").Unwrap()),
                            new Location
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
                            new List<string>() { "Bujes", "Cartón" },
                            new List<string>()).Unwrap(),
                        publication);
                }
                {
                    BoughtMaterialLine[] purchases = Singleton<EntrepreneurManager>.Instance.GetById("Entrepreneur1")!.BoughtMaterials.ToArray();
                    Assert.AreEqual(1, purchases.Length);
                    BoughtMaterialLine purchase = purchases[0];
                    CheckUtils.CheckBoughtMaterialLineEquality(
                        new BoughtMaterialLine(
                            "Teogal",
                            Material.CreateInstance(
                                "Bujes de cartón",
                                Measure.Length,
                                MaterialCategory.GetByName("Celulósicos").Unwrap()),
                            DateTime.Today,
                            new Price(15, Currency.Peso, Unit.GetByAbbr("cm").Unwrap()),
                            new Amount(2, Unit.GetByAbbr("cm").Unwrap()),
                            0),
                        purchase);
                }
                {
                    MaterialSalesLine[] sales = Singleton<CompanyManager>.Instance.GetByName("Teogal")!.MaterialSales.ToArray();
                    Assert.AreEqual(1, sales.Length);
                    MaterialSalesLine sale = sales[0];
                    CheckUtils.CheckMaterialSalesLineEquality(
                        new MaterialSalesLine(
                            Material.CreateInstance(
                                "Bujes de cartón",
                                Measure.Length,
                                MaterialCategory.GetByName("Celulósicos").Unwrap()),
                            new Amount(2, Unit.GetByAbbr("cm").Unwrap()),
                            new Price(15, Currency.Peso, Unit.GetByAbbr("cm").Unwrap()),
                            DateTime.Today,
                            "Santiago"),
                        sale);
                }
            });
        }

        /// <summary>
        /// Tests the user story of purchasing a material which doesn't exist.
        /// </summary>
        [Test]
        public void PurchaseNonExistentMaterialTest()
        {
            RuntimeTest.BasicRuntimeTest("purchase-non-existent-material", () =>
            {
                Singleton<ProgramaticMultipleUserPlatform>.Instance.ReceiveMessages(
                    "Entrepreneur1",
                    "/buy",
                    "Teog",
                    "Teo",
                    "Teogal",
                    "A1",
                    "C2",
                    "\\");

                Assert.AreEqual(0, Singleton<EntrepreneurManager>.Instance.GetById("Entrepreneur1")!.BoughtMaterials.Count);
                Assert.AreEqual(0, Singleton<CompanyManager>.Instance.GetByName("Teogal")!.MaterialSales.Count);
            });
        }

        /// <summary>
        /// Tests the user story of purchasing a material with lower stock than asked.
        /// </summary>
        [Test]
        public void PurchaseUnderstockedMaterialTest()
        {
            RuntimeTest.BasicRuntimeTest("purchase-understocked-material", () =>
            {
                Singleton<ProgramaticMultipleUserPlatform>.Instance.ReceiveMessages(
                    "Entrepreneur1",
                    "/buy",
                    "Teogal",
                    "Bujes de cartón",
                    "12 cm",
                    "Sí",
                    "Sí");

                {
                    List<MaterialPublication> publications = Singleton<CompanyManager>.Instance.GetByName("Teogal")!.Publications;
                    Assert.AreEqual(1, publications.Count);
                    MaterialPublication publication = publications[0];
                    CheckUtils.CheckMaterialPublicationEquality(
                        MaterialPublication.CreateInstance(
                            Material.CreateInstance(
                                "Bujes de cartón",
                                Measure.Length,
                                MaterialCategory.GetByName("Celulósicos").Unwrap()),
                            new Amount(0, Unit.GetByAbbr("cm").Unwrap()),
                            new Price(15, Currency.Peso, Unit.GetByAbbr("cm").Unwrap()),
                            new Location
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
                            new List<string>() { "Bujes", "Cartón" },
                            new List<string>()).Unwrap(),
                        publication);
                        Assert.That(publication.Sold, Is.True);
                }
                {
                    BoughtMaterialLine[] purchases = Singleton<EntrepreneurManager>.Instance.GetById("Entrepreneur1")!.BoughtMaterials.ToArray();
                    Assert.AreEqual(1, purchases.Length);
                    BoughtMaterialLine purchase = purchases[0];
                    CheckUtils.CheckBoughtMaterialLineEquality(
                        new BoughtMaterialLine(
                            "Teogal",
                            Material.CreateInstance(
                                "Bujes de cartón",
                                Measure.Length,
                                MaterialCategory.GetByName("Celulósicos").Unwrap()),
                            DateTime.Today,
                            new Price(15, Currency.Peso, Unit.GetByAbbr("cm").Unwrap()),
                            new Amount(10, Unit.GetByAbbr("cm").Unwrap()),
                            0),
                        purchase);
                }
                {
                    MaterialSalesLine[] sales = Singleton<CompanyManager>.Instance.GetByName("Teogal")!.MaterialSales.ToArray();
                    Assert.AreEqual(1, sales.Length);
                    MaterialSalesLine sale = sales[0];
                    CheckUtils.CheckMaterialSalesLineEquality(
                        new MaterialSalesLine(
                            Material.CreateInstance(
                                "Bujes de cartón",
                                Measure.Length,
                                MaterialCategory.GetByName("Celulósicos").Unwrap()),
                            new Amount(10, Unit.GetByAbbr("cm").Unwrap()),
                            new Price(15, Currency.Peso, Unit.GetByAbbr("cm").Unwrap()),
                            DateTime.Today,
                            "Santiago"),
                        sale);
                }
            });
        }

        /// <summary>
        /// Tests the user story of removing a sale from the platform.
        /// </summary>
        [Test]
        public void RemoveSaleTest()
        {
            RuntimeTest.BasicRuntimeTest("remove-sale", () =>
            {
                Company teogal = Singleton<CompanyManager>.Instance.GetByName("Teogal").Unwrap();
                MaterialPublication publication = teogal.Publications.Where(p => p.Material.Name == "Picado para relleno").FirstOrDefault().Unwrap();
                MaterialSalesLine line = teogal.MaterialSales.Where(sale => sale.SaleID == 1).FirstOrDefault().Unwrap();
                Assert.AreEqual(50, publication.Amount.Quantity);
                Assert.AreEqual(100, line.Amount.Quantity);

                Singleton<ProgramaticMultipleUserPlatform>.Instance.ReceiveMessages(
                    "Telegram_2066298868",
                    "/removesale",
                    "1",
                    "Bianca");

                Assert.AreEqual(0, teogal.MaterialSales.Count);
                Assert.AreEqual(150, publication.Amount.Quantity);
            });
        }
    }
}