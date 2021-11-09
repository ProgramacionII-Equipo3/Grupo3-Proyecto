using NUnit.Framework;
using Library.HighLevel.Entrepreneurs;
using System.Collections.Generic;
using Library.HighLevel.Accountability;
using Library.HighLevel.Materials;
using Ucu.Poo.Locations.Client;

namespace ProgramTests
{
    /// <summary>
    /// Test if an entrepreneur can see the material that is constantly generated.
    /// </summary>
    /* public class GetMaterialConstantlyGenerated
    {
        /// <summary>
        /// Test SetUp.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
        }

        /// <summary>
        /// Get the report of the constantly generated materials.
        /// </summary>
        [Test]
        public void GetReport()
        {
            LocationApiClient client = new LocationApiClient();
            MaterialCategory category = new MaterialCategory("Cartón");
            MaterialCategory category2 = new MaterialCategory("Productos Químicos");
            Material material = Material.CreateInstance("Bujes de Cartón", Measure.Length, category);
            Material material2 = Material.CreateInstance("Productos químicos usados para descartar", Measure.Weight, category2);
            Material material3 = Material.CreateInstance("Bujes de Cartón", Measure.Length, category);
            Material material4 = Material.CreateInstance("Bujes de Cartón", Measure.Length, category);
            Material material5 = Material.CreateInstance("Bujes de Cartón", Measure.Length, category);
            Location location = client.GetLocationAsync("Camino Maldonado km 15").Result;
            Unit unit = new Unit("Centímetros", "cm", 0.1, Measure.Length);
            Amount amount = new Amount(10, unit);
            Price price = new Price(50, Currency.Peso, unit);
            List<string> keywords = new List<string> { "keyword1", "keyword2" };

            MaterialPublication publication = MaterialPublication.CreateInstance(material, amount, price, location, keywords);
            MaterialPublication publication2 = MaterialPublication.CreateInstance(material2, amount, price, location, keywords);
            MaterialPublication publication3 = MaterialPublication.CreateInstance(material3, amount, price, location, keywords);
            MaterialPublication publication4 = MaterialPublication.CreateInstance(material4, amount, price, location, keywords);
            MaterialPublication publication5 = MaterialPublication.CreateInstance(material5, amount, price, location, keywords);
            MaterialPublication.AddPublication(publication);
            MaterialPublication.AddPublication(publication2);
            MaterialPublication.AddPublication(publication3);
            MaterialPublication.AddPublication(publication4);
            MaterialPublication.AddPublication(publication5);

            GetMaterialConstantlyGenerated.GetReport();
        }
    } */
}