using System;
using System.Collections.Generic;
using Library.HighLevel.Accountability;
using Library.HighLevel.Materials;
using Library.Utils;
using NUnit.Framework;

namespace ProgramTests
{
    /// <summary>
    /// Test if a company can get a report of all sent materials.
    /// </summary>
    public class CompanyReportTest
    {
        private MaterialCategory? category;
        private MaterialCategory? category2;
        private Unit? unit;
        private Unit? unit2;
        private Price price;
        private Price price2;
        private Amount amount;
        private Amount amount2;
        private Material? soldMaterial;
        private Material? soldMaterial2;
        private DateTime sold;
        private DateTime sold2;

        /// <summary>
        /// Necessary configuration.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.category = new MaterialCategory("Plásticos");
            this.unit = Unit.GetByAbbr("cm")!;
            this.price = new Price(300, Currency.Peso, this.unit);
            this.amount = new Amount(3, this.unit);
            this.soldMaterial = Material.CreateInstance("Palet Plástico", Measure.Length, this.category);
            this.sold = new DateTime(2021, 10, 3, 15, 30, 16);
            this.category2 = new MaterialCategory("Cartón");
            this.unit2 = Unit.GetByAbbr("cm")!;
            this.price2 = new Price(10, Currency.Dollar, this.unit2);
            this.amount2 = new Amount(40, this.unit2);
            this.soldMaterial2 = Material.CreateInstance("Bujes de cartón", Measure.Length, this.category2);
            this.sold2 = new DateTime(2021, 11, 1, 16, 21, 15);
        }

        /// <summary>
        /// Tests if the company can see the report of the sealed materials.
        /// </summary>
        [Test]
        public void CompanyReport()
        {
            MaterialSalesLine materialSale = new MaterialSalesLine(this.soldMaterial!, this.amount, this.price, this.sold);
            MaterialSalesLine materialSale2 = new MaterialSalesLine(this.soldMaterial2!, this.amount2, this.price2, this.sold2);
            IList<MaterialSalesLine> sales = new List<MaterialSalesLine> { materialSale, materialSale2 };
            IList<MaterialSalesLine> expected = SentMaterialReport.GetSentReport(sales, 3);
            Assert.AreEqual(expected, sales);
        }

        /// <summary>
        /// Test if the materials sold are out of time.
        /// </summary>
        [Test]
        public void CompanyReportOutOfTime()
        {
            MaterialCategory category3 = new MaterialCategory("Plásticos");
            Unit unit3 = Unit.GetByAbbr("cm")!;
            Price price3 = new Price(300, Currency.Peso, unit3);
            Amount amount3 = new Amount(3, unit3);
            Material soldMaterial3 = Material.CreateInstance("Palet Plástico", Measure.Length, category3);
            DateTime sold3 = new DateTime(2021, 3, 10, 13, 45, 12);
            MaterialCategory category4 = new MaterialCategory("Cartón");
            Unit unit4 = Unit.GetByAbbr("g")!;
            Price price4 = new Price(10, Currency.Dollar, unit4);
            Amount amount4 = new Amount(40, unit4);
            Material soldMaterial4 = Material.CreateInstance("Bujes de cartón", Measure.Length, category4);
            DateTime sold4 = new DateTime(2021, 2, 15, 17, 45, 02);

            MaterialSalesLine materialSale3 = new MaterialSalesLine(soldMaterial3, amount3, price3, sold3);
            MaterialSalesLine materialSale4 = new MaterialSalesLine(soldMaterial4, amount4, price4, sold4);
            IList<MaterialSalesLine> sales2 = new List<MaterialSalesLine> { materialSale3, materialSale4 };
            IList<MaterialSalesLine> report = SentMaterialReport.GetSentReport(sales2, 3);
            IList<MaterialSalesLine> expected = new List<MaterialSalesLine>();

            Assert.AreEqual(expected, report);
        }
    }
}