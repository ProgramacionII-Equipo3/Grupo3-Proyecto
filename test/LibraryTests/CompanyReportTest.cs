using NUnit.Framework;
using System;
using System.Collections.Generic;
using Library.HighLevel.Materials;
using Library.HighLevel.Accountability;

namespace ProgramTests
{
    /// <summary>
    /// 
    /// </summary>
    public class CompanyReportTest
    {
        MaterialCategory category;
        MaterialCategory category2;
        Unit unit;
        Unit unit2;
        Price price;
        Price price2;
        Amount amount;
        Amount amount2;
        Material soldMaterial;
        Material soldMaterial2;
        DateTime sold;
        DateTime sold2;
        SentMaterialReport report;
        SentMaterialReport report2;


        /// <summary>
        /// Necessary configuration.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            category = new MaterialCategory("Plásticos");
            unit = new Unit("Centímetros", "cm", 1, Measure.Length);
            price = new Price(300, Currency.Peso, unit);
            amount = new Amount(3, unit);
            soldMaterial = Material.CreateInstance("Palet Plástico", Measure.Length, category);
            sold = new DateTime(2021, 10, 3, 15, 30, 16);

            category2 = new MaterialCategory("Cartón");
            unit2 = new Unit("Centímetros", "cm", 1, Measure.Length);
            price2 = new Price(10, Currency.Dollar, unit);
            amount2 = new Amount(40, unit);
            soldMaterial2 = Material.CreateInstance("Bujes de cartón", Measure.Length, category2);
            sold2 = new DateTime(2021, 11, 1, 16, 21, 15);
        }

        /// <summary>
        /// Tests if the company can see the report of the sealed materials.
        /// </summary>
        [Test]
        public void CompanyReport()
        {
            MaterialSalesLine materialSale = new MaterialSalesLine(soldMaterial, amount, price, sold);
            MaterialSalesLine materialSale2 = new MaterialSalesLine(soldMaterial2, amount2, price2, sold2);
            List<MaterialSalesLine> sales = new List<MaterialSalesLine> { materialSale, materialSale2 };
            List<MaterialSalesLine> expected = SentMaterialReport.GetSentReport(sales, 3);

            Assert.AreEqual(expected, sales);
        }

        /// <summary>
        /// Test if the materials sold are out of time.
        /// </summary>
        [Test]
        public void CompanyReportOutOfTime()
        {
            MaterialCategory category3 = new MaterialCategory("Plásticos");
            Unit unit3 = new Unit("Centímetros", "cm", 1, Measure.Length);
            Price price3 = new Price(300, Currency.Peso, unit);
            Amount amount3 = new Amount(3, unit);
            Material soldMaterial3 = Material.CreateInstance("Palet Plástico", Measure.Length, category3);
            DateTime sold3 = new DateTime(2021, 3, 10, 13, 45, 12);

            MaterialCategory category4 = new MaterialCategory("Cartón");
            Unit unit4 = new Unit("Centímetros", "cm", 1, Measure.Length);
            Price price4 = new Price(10, Currency.Dollar, unit);
            Amount amount4 = new Amount(40, unit);
            Material soldMaterial4 = Material.CreateInstance("Bujes de cartón", Measure.Length, category4);
            DateTime sold4 = new DateTime(2021, 2, 15, 17, 45, 02);

            MaterialSalesLine materialSale3 = new MaterialSalesLine(soldMaterial3, amount3, price3, sold3);
            MaterialSalesLine materialSale4 = new MaterialSalesLine(soldMaterial4, amount4, price4, sold4);
            List<MaterialSalesLine> sales2 = new List<MaterialSalesLine> { materialSale3, materialSale4 };
            List<MaterialSalesLine> report = SentMaterialReport.GetSentReport(sales2, 3);
            List<MaterialSalesLine> expected = new List<MaterialSalesLine>();

            Assert.AreEqual(report, expected);
        }
    }
}