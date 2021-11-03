using NUnit.Framework;
using System.Collections.Generic;
using Library.HighLevel.Materials;
using Library.HighLevel.Accountability;
using Ucu.Poo.Locations.Client;

namespace ProgramTests
{
    /// <summary>
    /// 
    /// </summary>
    public class PublishOfferTest
    {
        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        public void Setup()
        {
           
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public async void PublishOffer()
        {
            LocationApiClient provider = new LocationApiClient();
            MaterialCategory category = new MaterialCategory("Impermeable");
            Unit unit = new Unit("Centimeters", "cm", 0.1, Measure.Length);
            Amount amount = new Amount(10, unit);
            Price price = new Price(100, Currency.Peso, unit);

            Location location = await provider.GetLocationAsync("Luis Alberto de Herrera 776", "Minas", "Lavalleja", "Uruguay");
            List<string> keyword = new List<string>();
            keyword.Add("cámara");
            Material material = Material.CreateInstance("Cámara de cubierta", Measure.Length, category);

            MaterialPublication publication = MaterialPublication.CreateInstance(material, amount, price, location, keyword);   
            MaterialPublication.publications.Add(publication);

            var expected = MaterialPublication.publications[0];
            Assert.AreEqual(publication, expected);
        }
    }
}