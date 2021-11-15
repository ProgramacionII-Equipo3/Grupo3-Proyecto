using System.Collections.Generic;
using Library.HighLevel.Accountability;
using Library.HighLevel.Entrepreneurs;
using Library.HighLevel.Materials;
using NUnit.Framework;
using Ucu.Poo.Locations.Client;

namespace ProgramTests
{
    /// <summary>
    /// This test proves that a entrepreneur can search material
    /// publication's using a keyword, category or zone.
    /// </summary>
    public class SearchOffersTest
    {
        private MaterialCategory category1;
        private Material material1;
        private Unit unit1;
        private Amount amount1;
        private Price price1;
        private LocationApiClient client;
        private Location pickupLocation1;
        private MaterialPublication publication1;
        private MaterialCategory category2;
        private Material material2;
        private Unit unit2;
        private Amount amount2;
        private Price price2;
        private Location pickupLocation2;
        private MaterialPublication publication2;

        /// <summary>
        /// Test Setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.category1 = new MaterialCategory("Residuos hospitalarios");
            List<string> keyword1 = new List<string> { "agujas", "hospital" };
            this.material1 = Material.CreateInstance("Agujas Quirúrgicas", Measure.Weight, this.category1);
            this.unit1 = new Unit("kilogram", "kg", 20, Measure.Weight);
            this.amount1 = new Amount(100, this.unit1);
            this.price1 = new Price(1000, Currency.Peso, this.unit1);
            this.client = new LocationApiClient();
            this.pickupLocation1 = this.client.GetLocationAsync("Libertad 2500").Result;
            this.publication1 = MaterialPublication.CreateInstance(this.material1, this.amount1, this.price1, this.pickupLocation1, MaterialPublicationTypeData.Normal(), keyword1);

            this.category2 = new MaterialCategory("Residuos hospitalarios");
            List<string> keyword2 = new List<string> { "hospital", "cubrebocas" };
            this.material2 = Material.CreateInstance("Tapabocas Descartable", Measure.Weight, this.category2);
            this.unit2 = new Unit("kilogram", "kg", 5, Measure.Weight);
            this.amount2 = new Amount(500, this.unit2);
            this.price2 = new Price(800, Currency.Peso, this.unit2);
            this.pickupLocation2 = this.client.GetLocationAsync("Dr. Gustavo Gallinal 1720").Result;
            this.publication2 = MaterialPublication.CreateInstance(this.material2, this.amount2, this.price2, this.pickupLocation2, MaterialPublicationTypeData.Normal(), keyword2);
        }

        /// <summary>
        /// This test checks that an entrepreneur is able to
        /// search material publication's by the category.
        /// </summary>
        [Test]
        public void SearchOffersbyCategoryFound()
        {
            List<MaterialPublication> publicationsToSearchIn = new List<MaterialPublication> { this.publication1, this.publication2 };

            MaterialCategory categoryToSearch = new MaterialCategory("Residuos hospitalarios");
            Searcher.SearchByCategory(publicationsToSearchIn, categoryToSearch);

            List<MaterialPublication> expected1 = new List<MaterialPublication>();
            expected1.Add(this.publication1);
            expected1.Add(this.publication2);

            Assert.AreEqual(Searcher.SearchResult, expected1);
        }

        /// <summary>
        /// This test checks that if an entrepreneur searches
        /// for a category that didn't exist it returns a list
        /// with 0 elements.
        /// </summary>
        [Test]
        public void SearchOffersbyCategoryNotFound()
        {
            List<MaterialPublication> publicationsToSearchIn = new List<MaterialPublication> { this.publication1, this.publication2 };

            MaterialCategory categoryToSearch = new MaterialCategory("Materia Prima");
            Searcher.SearchResult.Clear();
            Searcher.SearchByCategory(publicationsToSearchIn, categoryToSearch);

            List<MaterialPublication> expected2 = new List<MaterialPublication>();

            Assert.AreEqual(Searcher.SearchResult, expected2);
        }

        /// <summary>
        /// This test checks that an entrepreneur is able to
        /// search material publication's with a keyword.
        /// </summary>
        [Test]
        public void SearchOffersbyKeywordsFound()
        {
            List<MaterialPublication> publicationsToSearchIn = new List<MaterialPublication> { this.publication1, this.publication2 };

            Searcher.SearchResult.Clear();
            Searcher.SearchByKeyword(publicationsToSearchIn, "cubrebocas");

            List<MaterialPublication> expected3 = new List<MaterialPublication>();
            expected3.Add(this.publication2);

            Assert.AreEqual(Searcher.SearchResult, expected3);
        }

        /// <summary>
        /// This test checks that if an entrepreneur searches
        /// for a keyword that isn't included in a publication
        /// it returns a list with 0 elements.
        /// </summary>
        [Test]
        public void SearchOffersbyKeywordsNotFound()
        {
            List<MaterialPublication> publicationsToSearchIn = new List<MaterialPublication> { this.publication1, this.publication2 };

            Searcher.SearchResult.Clear();
            Searcher.SearchByKeyword(publicationsToSearchIn, "sanitario");

            List<MaterialPublication> expected4 = new List<MaterialPublication>();

            Assert.AreEqual(Searcher.SearchResult, expected4);
        }

        /// <summary>
        /// This test checks that an entrepreneur is able to
        /// search material publication's by the zone.
        /// </summary>
        [Test]
        public void SearchOffersbyZoneFound()
        {
            List<MaterialPublication> publicationsToSearchIn = new List<MaterialPublication> { this.publication1, this.publication2 };

            LocationApiClient clientTest = new LocationApiClient();
            Location locationSpecified = new Location();
            locationSpecified = clientTest.GetLocationAsync("Av. Gral. San Martín 2909").Result;
            double distanceSpecified = 4;
            Searcher.SearchResult.Clear();
            Searcher.SearchByLocation(publicationsToSearchIn, locationSpecified, distanceSpecified);

            List<MaterialPublication> expected5 = new List<MaterialPublication>();
            expected5.Add(this.publication2);

            Assert.AreEqual(Searcher.SearchResult, expected5);
        }

        /// <summary>
        /// This test checks that if an entrepreneur searches
        /// for a zone that isn't included in a publication or is to
        /// far away form the distance specified, it returns a list with 0 elements.
        /// </summary>
        [Test]
        public void SearchOffersbyZoneNotFound()
        {
            List<MaterialPublication> publicationsToSearchIn = new List<MaterialPublication> { this.publication1, this.publication2 };

            LocationApiClient clientTest = new LocationApiClient();
            Location locationSpecified = new Location();
            locationSpecified = clientTest.GetLocationAsync("12 De Diciembre 811").Result;
            double distanceSpecified = 2;
            Searcher.SearchResult.Clear();
            Searcher.SearchByLocation(publicationsToSearchIn, locationSpecified, distanceSpecified);

            List<MaterialPublication> expected6 = new List<MaterialPublication>();

            Assert.AreEqual(Searcher.SearchResult, expected6);
        }
    }
}