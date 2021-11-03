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
        Unit unit;
        Price price;
        Amount amount;
        Material soldMaterial;
        DateTime sold;
        SentMaterialReport report;


        /// <summary>
        /// 
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
        }

        /// <summary>
        /// Tests if the company can see the report of the sealed materials.
        /// </summary>
        [Test]
        public void CompanyReport()
        {
            
            MaterialSalesLine materialSale = new MaterialSalesLine(soldMaterial, amount, price, sold);
            var list = new List<MaterialSalesLine>();
            List<MaterialSalesLine> result = new List<MaterialSalesLine>();
            list.Add(materialSale);

            // The period of time to show the report.
            int time = 3;
            foreach (var item in list)
            {
                if (item.DateTime.Month < time)
                {
                    result.Add(item);
                }

            }
            report = new SentMaterialReport(result.AsReadOnly());
            
            MaterialSalesLine expected = report.Lines[0];

            Assert.AreEqual(materialSale, expected);

        }
    }
}