using System.Collections.Generic;
using Library.HighLevel.Accountability;
using Library.HighLevel.Materials;
using NUnit.Framework;
using Ucu.Poo.Locations.Client;

namespace ProgramTests
{
    /// <summary>
    /// Tests if a company can publish an offer for entrepreneurs.
    /// </summary>
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
            LocationApiClient provider = new LocationApiClient();
            MaterialCategory category = new MaterialCategory("Impermeable");
            Unit unit = new Unit("Centimeters", "cm", 0.1, Measure.Length);
            Amount amount = new Amount(10, unit);
            Price price = new Price(100, Currency.Peso, unit);
            Location location = provider.GetLocationAsync("Luis Alberto de Herrera 776", "Minas", "Lavalleja", "Uruguay").Result;
            List<string> keyword = new List<string> { "Cámara" };
            Material material = Material.CreateInstance("Cámara de cubierta", Measure.Length, category);
            MaterialPublication publication = MaterialPublication.CreateInstance(material, amount, price, location, keyword);
            MaterialPublication.AddPublication(publication);

            MaterialCategory category2 = new MaterialCategory("Plástico");
            Amount amount2 = new Amount(5, unit);
            Price price2 = new Price(600, Currency.Peso, unit);
            Location location2 = provider.GetLocationAsync("Camino Maldonado km 11").Result;
            List<string> keyword2 = new List<string> { "Palet", "Plástico" };
            Material material2 = Material.CreateInstance("Palet de Plástico", Measure.Length, category2);
            MaterialPublication publication2 = MaterialPublication.CreateInstance(material2, amount2, price2, location2, keyword2);
            MaterialPublication.AddPublication(publication2);

            List<MaterialPublication> expected = new List<MaterialPublication> { publication, publication2 };
            Assert.AreEqual(MaterialPublication.Publications, expected);
        }

        /// <summary>
        /// Test when a company not publish other offer.
        /// </summary>
        [Test]
        public void NotPublishOffer()
        {
            LocationApiClient client = new LocationApiClient();
            MaterialCategory category3 = new MaterialCategory("Metálicos");
            Unit unit3 = new Unit("Kilogramos", "kg", 1, Measure.Weight);
            Amount amount3 = new Amount(3, unit3);
            Price price3 = new Price(250, Currency.Peso, unit3);
            Location location3 = client.GetLocationAsync("Av. 8 de Octubre 2738").Result;
            List<string> keywords = new List<string> { "metálicos", "metal", "residuos de contenedores" };
            Material material3 = Material.CreateInstance("Residuos generados de reparaciones de contenedores", Measure.Weight, category3);
            MaterialPublication.CreateInstance(material3, amount3, price3, location3, keywords);
            List<MaterialPublication> expected2 = new List<MaterialPublication>();

            Assert.AreEqual(MaterialPublication.Publications, expected2);
        }
    }
}