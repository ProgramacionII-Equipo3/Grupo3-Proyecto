using NUnit.Framework;
using Library.HighLevel.Materials;
using Library.HighLevel.Accountability;
using System;
using System.Collections.Generic;
namespace ProgramTests
{
    /// <summary>
    /// 
    /// </summary>
    public class EntrepreneurReportTest
    {
        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// This test evaluate if the material was added in the report of purchased materials.
        /// </summary>
        [Test]
        public void EntrepreneurReport()
        {
            MaterialCategory category = new MaterialCategory("Metales");
            Unit unit = new Unit("Kilos", "kg", 0.5, Measure.Weight);
            Amount amount = new Amount(3, unit);
            Price price = new Price(520, Currency.Peso, unit);
            List<string> keyword = new List<string>();
            keyword.Add("ruleman");
            keyword.Add("metal");
            Material boughtMaterial = Material.CreateInstance("Ruleman Metal", Measure.Weight, category);
            DateTime date = new DateTime(2021, 9, 2, 23, 20, 18);
            BoughtMaterialLine materialbought1 = new BoughtMaterialLine(boughtMaterial, date, price, amount);
            var list = new List<BoughtMaterialLine>();
            list.Add(materialbought1);
            BoughtMaterialLine expected = list[0];
            Assert.AreEqual(materialbought1, expected);

            MaterialCategory category2 = new MaterialCategory("Plásticos");
            Unit unit2 = new Unit("Gramos","g",500,Measure.Weight);
            Amount amount2 = new Amount(2,unit);
            Price price2 = new Price(2,Currency.Dollar,unit);
            List<string> keyword2 = new List<string>();
            keyword2.Add("Botella");
            keyword2.Add("plástico");
            Material boughtMaterial2 = Material.CreateInstance("Botella plástico",Measure.Weight,category2);
            DateTime date2 = new DateTime(2021,7,8,20,17,19);
            BoughtMaterialLine materialbought2 = new BoughtMaterialLine(boughtMaterial2,date2,price2,amount2);
            list.Add(materialbought2);
            BoughtMaterialLine expected2 = list[1];
            Assert.AreEqual(materialbought2, expected2);
        }
    }
}