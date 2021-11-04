using NUnit.Framework;
using System.Collections.Generic;
using Library.HighLevel.Materials;
using Library.HighLevel.Accountability;
using Library.HighLevel.Entrepreneurs;
using Ucu.Poo.Locations.Client;

namespace ProgramTests
{
    /// <summary>
    /// This test proves that a entrepreneur can search material 
    /// publication's using a keyword, category or zone.
    /// </summary>
    public class SearchOffersTest
    {        
        
        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        public void Setup()
        {

        }

        /// <summary>
        /// This test checks that an entrepreneur is able to
        /// search material publication's by the category.
        /// </summary>
        [Test]
        public void SearchOffersbyCategoryFound()
        {
            MaterialCategory category1 = new MaterialCategory("Residuos hospitalarios"); 
            List<string> keyword1 = new List<string>();
            keyword1.Add("agujas");
            keyword1.Add("hospital");
            Material material1 = Material.CreateInstance("Agujas Quirúrgicas", Measure.Weight, category1);
            Unit unit1 = new Unit("kilogram", "kg", 20, Measure.Weight);
            Amount amount1 = new Amount(100, unit1);
            Price price1 = new Price(1000, Currency.Peso, unit1);
            LocationApiClient client = new LocationApiClient();
            Location pickupLocation1 = new Location();
            pickupLocation1 = client.GetLocationAsync("Libertad 2500").Result;
            MaterialPublication publication1 = MaterialPublication.CreateInstance(material1, amount1, price1, pickupLocation1, keyword1);
            MaterialPublication.AddPublication(publication1);

            MaterialCategory category2 = new MaterialCategory("Residuos hospitalarios"); 
            List<string> keyword2 = new List<string>();
            keyword2.Add("hospital");
            keyword2.Add("cubrebocas");
            Material material2 = Material.CreateInstance("Tapabocas Descartable", Measure.Weight, category2);
            Unit unit2 = new Unit("kilogram", "kg", 5, Measure.Weight);
            Amount amount2 = new Amount(500, unit2);
            Price price2 = new Price(800, Currency.Peso, unit2);
            Location pickupLocation2 = new Location();
            pickupLocation2 = client.GetLocationAsync("Dr. Gustavo Gallinal 1720").Result;
            MaterialPublication publication2 = MaterialPublication.CreateInstance(material2, amount2, price2, pickupLocation2, keyword2);
            MaterialPublication.AddPublication(publication2);

            MaterialCategory category = new MaterialCategory("Residuos hospitalarios");
            SearchByCategory searchByCategory = new SearchByCategory();
            searchByCategory.Search(MaterialPublication.publications, category);

            List<MaterialPublication> expected1 = new List<MaterialPublication>();
            expected1.Add(publication1);
            expected1.Add(publication2);

            Assert.AreEqual(SearchByCategory.categorySearcher, expected1);
        }

        /// <summary>
        /// This test checks that if an entrepreneur searches
        /// for a category that didn't exist it returns a list
        /// with 0 elements.
        /// </summary>
        [Test]
        public void SearchOffersbyCategoryNotFound()
        {
            MaterialCategory category1 = new MaterialCategory("Residuos hospitalarios"); 
            List<string> keyword1 = new List<string>();
            keyword1.Add("agujas");
            keyword1.Add("hospital");
            Material material1 = Material.CreateInstance("Agujas Quirúrgicas", Measure.Weight, category1);
            Unit unit1 = new Unit("kilogram", "kg", 20, Measure.Weight);
            Amount amount1 = new Amount(100, unit1);
            Price price1 = new Price(1000, Currency.Peso, unit1);
            LocationApiClient client = new LocationApiClient();
            Location pickupLocation1 = new Location();
            pickupLocation1 = client.GetLocationAsync("Libertad 2500").Result;
            MaterialPublication publication1 = MaterialPublication.CreateInstance(material1, amount1, price1, pickupLocation1, keyword1);
            MaterialPublication.AddPublication(publication1);

            MaterialCategory category2 = new MaterialCategory("Residuos hospitalarios"); 
            List<string> keyword2 = new List<string>();
            keyword2.Add("hospital");
            keyword2.Add("cubrebocas");
            Material material2 = Material.CreateInstance("Tapabocas Descartable", Measure.Weight, category2);
            Unit unit2 = new Unit("kilogram", "kg", 5, Measure.Weight);
            Amount amount2 = new Amount(500, unit2);
            Price price2 = new Price(800, Currency.Peso, unit2);
            Location pickupLocation2 = new Location();
            pickupLocation2 = client.GetLocationAsync("Dr. Gustavo Gallinal 1720").Result;
            MaterialPublication publication2 = MaterialPublication.CreateInstance(material2, amount2, price2, pickupLocation2, keyword2);
            MaterialPublication.AddPublication(publication2);

            MaterialCategory Category = new MaterialCategory("Materia Prima");
            SearchByCategory searchByCategoryTest = new SearchByCategory();
            SearchByCategory.categorySearcher.Clear();
            searchByCategoryTest.Search(MaterialPublication.publications, Category);

            List<MaterialPublication> expected2 = new List<MaterialPublication>();

            Assert.AreEqual(SearchByCategory.categorySearcher, expected2);
        }/*

        /// <summary>
        /// This test checks that an entrepreneur is able to
        /// search material publication's with a keyword.
        /// </summary>
        [Test]
        public void SearchOffersbyKeywordsFound()
        {
                        MaterialCategory category1 = new MaterialCategory("Residuos hospitalarios"); 
            List<string> keyword1 = new List<string>();
            keyword1.Add("agujas");
            keyword1.Add("hospital");
            Material material1 = Material.CreateInstance("Agujas Quirúrgicas", Measure.Weight, category1);
            Unit unit1 = new Unit("kilogram", "kg", 20, Measure.Weight);
            Amount amount1 = new Amount(100, unit1);
            Price price1 = new Price(1000, Currency.Peso, unit1);
            LocationApiClient client = new LocationApiClient();
            Location pickupLocation1 = new Location();
            pickupLocation1 = client.GetLocationAsync("Libertad 2500").Result;
            MaterialPublication publication1 = MaterialPublication.CreateInstance(material1, amount1, price1, pickupLocation1, keyword1);
            MaterialPublication.AddPublication(publication1);

            MaterialCategory category2 = new MaterialCategory("Residuos hospitalarios"); 
            List<string> keyword2 = new List<string>();
            keyword2.Add("hospital");
            keyword2.Add("cubrebocas");
            Material material2 = Material.CreateInstance("Tapabocas Descartable", Measure.Weight, category2);
            Unit unit2 = new Unit("kilogram", "kg", 5, Measure.Weight);
            Amount amount2 = new Amount(500, unit2);
            Price price2 = new Price(800, Currency.Peso, unit2);
            Location pickupLocation2 = new Location();
            pickupLocation2 = client.GetLocationAsync("Dr. Gustavo Gallinal 1720").Result;
            MaterialPublication publication2 = MaterialPublication.CreateInstance(material2, amount2, price2, pickupLocation2, keyword2);
            MaterialPublication.AddPublication(publication2);

            SearchByKeyword searchByKeywordTest = new SearchByKeyword();
            SearchByKeyword.keywordSearcher.Clear();
            searchByKeywordTest.Search(MaterialPublication.publications, "cubrebocas");

            List<MaterialPublication> expected3 = new List<MaterialPublication>();
            expected3.Add(publication2);

            Assert.AreEqual(SearchByKeyword.keywordSearcher, expected3);
        }

        /// <summary>
        /// This test checks that if an entrepreneur searches
        /// for a keyword that isn't included in a publication 
        /// it returns a list with 0 elements.
        /// </summary>
        [Test]
        public void SearchOffersbyKeywordsNotFound()
        {
            SearchByKeyword searchByKeyword = new SearchByKeyword();
            searchByKeyword.Search(MaterialPublication.publications, "sanitario");

            List<MaterialPublication> expected4 = new List<MaterialPublication>();

            Assert.AreEqual(SearchByKeyword.keywordSearcher, expected4);
        }

        /// <summary>
        /// This test checks that an entrepreneur is able to
        /// search material publication's by the zone.
        /// </summary>
        [Test]
        public void SearchOffersbyZoneFound()
        {
            SearchByLocation searchByLocation = new SearchByLocation();
            LocationApiClient clientTest = new LocationApiClient();
            Location locationSpecified = new Location();
            locationSpecified = clientTest.GetLocationAsync("Av. Gral. San Martín 2909").Result;
            double distanceSpecified = 100;
            searchByLocation.Search(MaterialPublication.publications, locationSpecified, distanceSpecified);

            List<MaterialPublication> expected5 = new List<MaterialPublication>();
            expected5.Add(publication2);

            Assert.AreEqual(SearchByLocation.locationSearcher, expected5);
        }*/
    }
}