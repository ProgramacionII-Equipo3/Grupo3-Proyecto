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
        MaterialCategory category1;
        Material material1;
        Unit unit1;
        Amount amount1;
        Price price1; 
        LocationApiClient client;
        Location pickupLocation1;
        MaterialPublication publication1;

        MaterialCategory category2;
        Material material2;
        Unit unit2;
        Amount amount2;
        Price price2; 
        Location pickupLocation2;
        MaterialPublication publication2;
        
        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        public void Setup()
        {
            category1 = new MaterialCategory("Residuos hospitalarios"); 
            List<string> keyword1 = new List<string>{ "agujas", "hospital" };
            material1 = Material.CreateInstance("Agujas Quirúrgicas", Measure.Weight, category1);
            unit1 = new Unit("kilogram", "kg", 20, Measure.Weight);
            amount1 = new Amount(100, unit1);
            price1 = new Price(1000, Currency.Peso, unit1);
            client = new LocationApiClient();
            pickupLocation1 = client.GetLocationAsync("Libertad 2500").Result;
            publication1 = MaterialPublication.CreateInstance(material1, amount1, price1, pickupLocation1, keyword1);

            category2 = new MaterialCategory("Residuos hospitalarios"); 
            List<string> keyword2 = new List<string>{ "hospital", "cubrebocas" };
            material2 = Material.CreateInstance("Tapabocas Descartable", Measure.Weight, category2);
            unit2 = new Unit("kilogram", "kg", 5, Measure.Weight);
            amount2 = new Amount(500, unit2);
            price2 = new Price(800, Currency.Peso, unit2);
            pickupLocation2 = client.GetLocationAsync("Dr. Gustavo Gallinal 1720").Result;
            publication2 = MaterialPublication.CreateInstance(material2, amount2, price2, pickupLocation2, keyword2);

        }

        /// <summary>
        /// This test checks that an entrepreneur is able to
        /// search material publication's by the category.
        /// </summary>
        [Test]
        public void SearchOffersbyCategoryFound()
        {
            List<MaterialPublication> publicationsToSearchIn = new List<MaterialPublication>{ publication1, publication2 };
           
            MaterialCategory categoryToSearch = new MaterialCategory("Residuos hospitalarios");
            SearchByCategory searchByCategory = new SearchByCategory();
            searchByCategory.Search(publicationsToSearchIn, categoryToSearch);

            List<MaterialPublication> expected1 = new List<MaterialPublication>();
            expected1.Add(publication1);
            expected1.Add(publication2);

            Assert.AreEqual(SearchByCategory.CategorySearcher, expected1);
        }

        /// <summary>
        /// This test checks that if an entrepreneur searches
        /// for a category that didn't exist it returns a list
        /// with 0 elements.
        /// </summary>
        [Test]
        public void SearchOffersbyCategoryNotFound()
        {
            List<MaterialPublication> publicationsToSearchIn = new List<MaterialPublication>{ publication1, publication2 };

            MaterialCategory categoryToSearch = new MaterialCategory("Materia Prima");
            SearchByCategory searchByCategory = new SearchByCategory();
            SearchByCategory.CategorySearcher.Clear();
            searchByCategory.Search(publicationsToSearchIn, categoryToSearch);

            List<MaterialPublication> expected2 = new List<MaterialPublication>();

            Assert.AreEqual(SearchByCategory.CategorySearcher, expected2);
        }

        /// <summary>
        /// This test checks that an entrepreneur is able to
        /// search material publication's with a keyword.
        /// </summary>
        [Test]
        public void SearchOffersbyKeywordsFound()
        {
            List<MaterialPublication> publicationsToSearchIn = new List<MaterialPublication>{ publication1, publication2 };

            SearchByKeyword searchByKeyword = new SearchByKeyword();
            searchByKeyword.Search(publicationsToSearchIn, "cubrebocas");

            List<MaterialPublication> expected3 = new List<MaterialPublication>();
            expected3.Add(publication2);

            Assert.AreEqual(SearchByKeyword.KeywordSearcher, expected3);
        }

        /// <summary>
        /// This test checks that if an entrepreneur searches
        /// for a keyword that isn't included in a publication 
        /// it returns a list with 0 elements.
        /// </summary>
        [Test]
        public void SearchOffersbyKeywordsNotFound()
        {
            List<MaterialPublication> publicationsToSearchIn = new List<MaterialPublication>{ publication1, publication2 };

            SearchByKeyword searchByKeyword = new SearchByKeyword();
            SearchByKeyword.KeywordSearcher.Clear();
            searchByKeyword.Search(publicationsToSearchIn, "sanitario");

            List<MaterialPublication> expected4 = new List<MaterialPublication>();

            Assert.AreEqual(SearchByKeyword.KeywordSearcher, expected4);
        }

        /// <summary>
        /// This test checks that an entrepreneur is able to
        /// search material publication's by the zone.
        /// </summary>
        [Test]
        public void SearchOffersbyZoneFound()
        {
            List<MaterialPublication> publicationsToSearchIn = new List<MaterialPublication>{ publication1, publication2 };

            SearchByLocation searchByLocation = new SearchByLocation();
            LocationApiClient clientTest = new LocationApiClient();
            Location locationSpecified = new Location();
            locationSpecified = clientTest.GetLocationAsync("Av. Gral. San Martín 2909").Result;
            double distanceSpecified = 4;
            searchByLocation.Search(publicationsToSearchIn, locationSpecified, distanceSpecified);

            List<MaterialPublication> expected5 = new List<MaterialPublication>();
            expected5.Add(publication2);

            Assert.AreEqual(SearchByLocation.LocationSearcher, expected5);
        }
        /// <summary>
        /// This test checks that if an entrepreneur searches
        /// for a zone that isn't included in a publication or is to 
        /// far away form the distance specified, it returns a list with 0 elements.
        /// </summary>
        [Test]
        public void SearchOffersbyZoneNotFound()
        {
            List<MaterialPublication> publicationsToSearchIn = new List<MaterialPublication>{ publication1, publication2 };

            SearchByLocation searchByLocation = new SearchByLocation();
            LocationApiClient clientTest = new LocationApiClient();
            Location locationSpecified = new Location();
            locationSpecified = clientTest.GetLocationAsync("12 De Diciembre 811").Result;
            double distanceSpecified = 2;
            SearchByLocation.LocationSearcher.Clear();
            searchByLocation.Search(publicationsToSearchIn, locationSpecified, distanceSpecified);

            List<MaterialPublication> expected6 = new List<MaterialPublication>();

            Assert.AreEqual(SearchByLocation.LocationSearcher, expected6);
        }
    }
}