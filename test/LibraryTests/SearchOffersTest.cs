using NUnit.Framework;
using Library.HighLevel.Materials;
using Library.HighLevel.Accountability;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Library.HighLevel.Entrepreneurs;

namespace ProgramTests
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchOffersTest
    {        
        MaterialCategory category1;
        Material material1;
        Unit unit1;
        Amount amount1;
        Price price1;
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
            List<string> keyword1 = new List<string>();
            keyword1.Add("agujas");
            keyword1.Add("hospital");
            material1 = Material.CreateInstance("Agujas Quirúrgicas", Measure.Weight, category1, keyword1);
            unit1 = new Unit("kilogram", "kg", 20, Measure.Weight);
            amount1 = new Amount(100, unit1);
            price1 = new Price(1000, Currency.Peso, unit1);
            pickupLocation1 = new Location();
            publication1 = MaterialPublication.CreateInstance(material1, amount1, price1, pickupLocation1);
            MaterialPublication.AddPublication(publication1);

            category2 = new MaterialCategory("Residuos hospitalarios"); 
            List<string> keyword2 = new List<string>();
            keyword2.Add("hospital");
            keyword2.Add("cubrebocas");
            material2 = Material.CreateInstance("Tapabocas Descartable", Measure.Weight, category2, keyword2);
            unit2 = new Unit("kilogram", "kg", 5, Measure.Weight);
            amount2 = new Amount(500, unit2);
            price2 = new Price(800, Currency.Peso, unit2);
            pickupLocation2 = new Location();
            publication2 = MaterialPublication.CreateInstance(material2, amount2, price2, pickupLocation2);
            MaterialPublication.AddPublication(publication2);

        }

        /// <summary>
        /// This test proves that an entrepreneur is able to
        /// search material publication's by the category.
        /// </summary>
        [Test]
        public void SearchOffersbyCategoryFound()
        {
            MaterialCategory Category = new MaterialCategory("Residuos hospitalarios");
            SearchByCategory searchByCategory = new SearchByCategory();
            searchByCategory.Search(MaterialPublication.publications, Category);

            List<MaterialPublication> expected = new List<MaterialPublication>();
            expected.Add(publication1);
            expected.Add(publication2);

            Assert.AreEqual(SearchByCategory.categorySearcher, expected);
        }

        /// <summary>
        /// This test proves that if an entrepreneur searches
        /// for a category that didn't exist it returns a list
        /// with 0 elements.
        /// </summary>
        [Test]
        public void SearchOffersbyCategoryNotFound()
        {
            MaterialCategory Category = new MaterialCategory("Materia Prima");
            SearchByCategory searchByCategory = new SearchByCategory();
            searchByCategory.Search(MaterialPublication.publications, Category);

            List<MaterialPublication> expected = new List<MaterialPublication>();

            Assert.AreEqual(SearchByCategory.categorySearcher, expected);
        }

        /// <summary>
        /// This test proves that an entrepreneur is able to
        /// search material publication's with a keyword.
        /// </summary>
        [Test]
        public void SearchOffersbyKeywordsFound()
        {
            SearchByKeyword searchByKeyword = new SearchByKeyword();
            searchByKeyword.Search(MaterialPublication.publications, "cubrebocas");

            List<MaterialPublication> expected = new List<MaterialPublication>();
            expected.Add(publication2);

            Assert.AreEqual(SearchByKeyword.keywordSearcher, expected);
        }

        /// <summary>
        /// This test proves that if an entrepreneur searches
        /// for a keyword that isn't included in a publication 
        /// it returns a list with 0 elements.
        /// </summary>
        [Test]
        public void SearchOffersbyKeywordsNotFound()
        {
            SearchByKeyword searchByKeyword = new SearchByKeyword();
            searchByKeyword.Search(MaterialPublication.publications, "sanitario");

            List<MaterialPublication> expected = new List<MaterialPublication>();

            Assert.AreEqual(SearchByKeyword.keywordSearcher, expected);
        }

        /// <summary>
        /// This test proves that an entrepreneur is able to
        /// search material publication's by the zone.
        /// </summary>
        [Test]
        public void SearchOffersbyZone()
        {

        }

    }
}