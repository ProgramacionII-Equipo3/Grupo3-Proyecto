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
        public void CompanyReport()
        {
            MaterialCategory category = new MaterialCategory("Plásticos");
            Unit unit = new Unit("Centímetros", "cm", 1, Measure.Length);
            Price price = new Price(300, Currency.Peso, unit);
            Amount amount = new Amount(3, unit);
            Material soldMaterial = Material.CreateInstance("Palet Plástico", Measure.Length, category);
            DateTime sold = new DateTime(2021, 10, 3, 15, 30, 16);
            MaterialSalesLine materialSale = new MaterialSalesLine(soldMaterial, amount, price, sold);
            var list = new List<MaterialSalesLine>();
            list.Add(materialSale);
            MaterialSalesLine expected = list[0];

            Assert.AreEqual(materialSale, expected);

        }
    }
}