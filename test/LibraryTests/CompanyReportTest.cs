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
           

            

    
           
            

        }
    }
}