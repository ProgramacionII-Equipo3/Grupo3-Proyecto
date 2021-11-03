using Library.HighLevel.Accountability;
using Library.HighLevel.Materials;
using Ucu.Poo.Locations.Client;
using System.Collections.Generic;
using NUnit.Framework;
namespace ProgramTests
{
    /// <summary>
    /// Tests if a company can publish an offer for entrepreneurs.
    /// </summary>
    public class PublishOfferTest
    {
        /// <summary>
        /// Sets the LocationApli client.
        /// </summary>
        LocationApiClient provider { get; set; }

        /// <summary>
        /// Material´s category.
        /// </summary>
        MaterialCategory category;

        /// <summary>
        /// 
        /// </summary>
        Unit unit;
        /// <summary>
        /// 
        /// </summary>
        Amount amount;

        /// <summary>
        /// 
        /// </summary>
        Price price;
        /// <summary>
        /// 
        /// </summary>
        Location location;
        /// <summary>
        /// 
        /// </summary>
        List<string> keyword;
        

        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        public async void Setup()
        {  
            provider = new LocationApiClient();
            category = new MaterialCategory("Impermeable");
            unit = new Unit("Centimeters", "cm", 0.1, Measure.Length);
            amount = new Amount(10, unit);
            price = new Price(100, Currency.Peso, unit);
            location = await provider.GetLocationAsync("Luis Alberto de Herrera 776", "Minas", "Lavalleja", "Uruguay");
            keyword = new List<string>();
            keyword.Add("cámara");
        }

        /// <summary>
        /// If the company publish the offer the entrepreneur can see it.
        /// </summary>
        [Test]
        public void PublishOffer()
        { 
            Material material = Material.CreateInstance("Cámara de cubierta", Measure.Length, category);
            MaterialPublication publication = MaterialPublication.CreateInstance(material, amount, price, location, keyword);   
            MaterialPublication.publications.Add(publication);

            var expected = MaterialPublication.publications[0];
            Assert.AreEqual(publication, expected);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public async void NotPublishOffer()
        {

        }

    }

}