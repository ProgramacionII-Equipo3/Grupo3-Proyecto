using System.Collections.Generic;
using System.Linq;
using Library;
using Library.HighLevel.Accountability;
using Library.HighLevel.Entrepreneurs;
using Library.HighLevel.Materials;
using Library.HighLevel.Companies;
using Library.Core;
using Library.Utils;
using NUnit.Framework;
using Ucu.Poo.Locations.Client;

namespace ProgramTests
{
    /// <summary>
    /// This test proves that a entrepreneur can search material
    /// publication's using a keyword, category or zone.
    /// </summary>
    [TestFixture]
    public class SearchOffersTest
    {
        private MaterialCategory? category1;
        private Material? material1;
        private Unit? unit1;
        private Amount amount1;
        private Price price1;
        private Location? pickupLocation1;
        private AssignedMaterialPublication? publication1;
        private MaterialCategory? category2;
        private Material? material2;
        private Unit? unit2;
        private Amount amount2;
        private Price price2;
        private Location? pickupLocation2;
        private AssignedMaterialPublication? publication2;
        private ContactInfo contact;


        /// <summary>
        /// Test Setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {

            this.category1 = new MaterialCategory("Residuos hospitalarios");
            IList<string> keyword1 = new List<string> { "agujas", "hospital" };
            IList<string> requirements1 = new List<string> { };
            this.material1 = Material.CreateInstance("Agujas Quirúrgicas", Measure.Weight, this.category1);
            this.unit1 = Unit.GetByAbbr("kg") !;
            this.amount1 = new Amount(100, this.unit1);
            this.price1 = new Price(1000, Currency.Peso, this.unit1);
            using LocationApiClient client = new LocationApiClient();
            this.pickupLocation1 = client.GetLocationResilient("Libertad 2500");
            this.contact = new ContactInfo("company1@gmail.com", 099421658);
            
            this.category2 = new MaterialCategory("Residuos hospitalarios");
            this.material2 = Material.CreateInstance("Tapabocas Descartable", Measure.Weight, this.category2);
            this.unit2 = Unit.GetByAbbr("kg") !;
            this.amount2 = new Amount(500, this.unit2);
            this.price2 = new Price(800, Currency.Peso, this.unit2);
            this.pickupLocation2 = client.GetLocationResilient("Dr. Gustavo Gallinal 1720");
            IList<string> keyword2 = new List<string> { "hospital", "cubrebocas" };
            IList<string> requirements2 = new List<string> { };

            Company empresa;
            if (Singleton<CompanyManager>.Instance.GetByName("Company1") is Company c)
            {
                empresa = c;
            }
            else
            {
                empresa = Singleton<CompanyManager>.Instance.CreateCompany("Company1", this.contact, "Tecnología", this.pickupLocation1) !;
                empresa.PublishMaterial(this.material1, this.amount1, this.price1, this.pickupLocation1, MaterialPublicationTypeData.Normal(), keyword1,requirements1);
                empresa.PublishMaterial(this.material2, this.amount2, this.price2, this.pickupLocation2, MaterialPublicationTypeData.Normal(), keyword2, requirements2);
            }

            IList<AssignedMaterialPublication> publications = empresa.AssignedPublications;

            this.publication1 = publications.Where(p => p.Publication.Keywords.Any(k => k == "hospital")).FirstOrDefault();
            this.publication2 = publications.Where(p => p.Publication.Keywords.Any(k => k == "cubrebocas")).FirstOrDefault();

            client.Dispose();
        }

        /// <summary>
        /// This test checks that an entrepreneur is able to
        /// search material publication's by the category.
        /// </summary>
        [Test]
        public void SearchOffersbyCategoryFound()
        {
            MaterialCategory categoryToSearch = new MaterialCategory("Residuos hospitalarios");

            IList<AssignedMaterialPublication> expected1 = new List<AssignedMaterialPublication>();
            expected1.Add(this.publication1.Unwrap());
            expected1.Add(this.publication2.Unwrap());

            Assert.AreEqual(expected1, Singleton<Searcher>.Instance.SearchByCategory(categoryToSearch));
        }

        /// <summary>
        /// This test checks that if an entrepreneur searches
        /// for a category that didn't exist it returns a list
        /// with 0 elements.
        /// </summary>
        [Test]
        public void SearchOffersbyCategoryNotFound()
        {
            MaterialCategory categoryToSearch = new MaterialCategory("Materia Prima");

            IList<AssignedMaterialPublication> expected2 = new List<AssignedMaterialPublication>();

            Assert.AreEqual(expected2, Singleton<Searcher>.Instance.SearchByCategory(categoryToSearch));
        }

        /// <summary>
        /// This test checks that an entrepreneur is able to
        /// search material publication's with a keyword.
        /// </summary>
        [Test]
        public void SearchOffersbyKeywordsFound()
        {
            List<AssignedMaterialPublication> expected3 = new List<AssignedMaterialPublication>();
            System.Console.WriteLine("---" + (this.publication2 is null));
            expected3.Add(this.publication2.Unwrap());

            List<AssignedMaterialPublication> result = Singleton<Searcher>.Instance.SearchByKeyword("cubrebocas");

//            Assert.That(expected3, Is.Not.Null);
            Assert.That(result, Is.Not.Null);
//            System.Console.WriteLine(string.Join(", ", expected3));
//            System.Console.WriteLine(string.Join(", ", result));
            Assert.AreEqual(expected3, result);
        }

        /// <summary>
        /// This test checks that if an entrepreneur searches
        /// for a keyword that isn't included in a publication
        /// it returns a list with 0 elements.
        /// </summary>
        [Test]
        public void SearchOffersbyKeywordsNotFound()
        {
            List<AssignedMaterialPublication> expected4 = new List<AssignedMaterialPublication>();

            Assert.AreEqual(expected4, Singleton<Searcher>.Instance.SearchByKeyword("sanitario"));
        }

        /// <summary>
        /// This test checks that an entrepreneur is able to
        /// search material publication's by the zone.
        /// </summary>
        [Test]
        public void SearchOffersbyZoneFound()
        {
            using LocationApiClient clientTest = new LocationApiClient();
            Location locationSpecified = new Location();
            locationSpecified = clientTest.GetLocationResilient("Av. Gral. San Martín 2909");
            double distanceSpecified = 4;

            IList<AssignedMaterialPublication> expected5 = new List<AssignedMaterialPublication>();
            expected5.Add(this.publication2.Unwrap());

            Assert.AreEqual(expected5, Singleton<Searcher>.Instance.SearchByLocation(locationSpecified, distanceSpecified));
            clientTest.Dispose();
        }

        /// <summary>
        /// This test checks that if an entrepreneur searches
        /// for a zone that isn't included in a publication or is to
        /// far away form the distance specified, it returns a list with 0 elements.
        /// </summary>
        [Test]
        public void SearchOffersbyZoneNotFound()
        {
            using LocationApiClient clientTest = new LocationApiClient();
            Location locationSpecified = new Location();
            locationSpecified = clientTest.GetLocationResilient("12 De Diciembre 811");
            double distanceSpecified = 2;

            IList<AssignedMaterialPublication> expected6 = new List<AssignedMaterialPublication>();

            Assert.AreEqual(expected6, Singleton<Searcher>.Instance.SearchByLocation(locationSpecified, distanceSpecified));
            clientTest.Dispose();
        }
    }
}
