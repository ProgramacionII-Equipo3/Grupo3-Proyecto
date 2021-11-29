using System;
using System.Collections.Generic;
using Library;
using Library.HighLevel.Accountability;
using Library.HighLevel.Materials;
using Library.Utils;
using NUnit.Framework;
using ProgramTests.Utils;

namespace ProgramTests
{
    /// <summary>
    /// This test check if the Entrepreneur can get an received material report.
    /// </summary>
    [TestFixture]
    public class EntrepreneurReportTest
    {
        /// <summary>
        /// Test Setup.
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
            MaterialCategory category = MaterialCategory.GetByName("Metales").Unwrap();
            Unit unit = Unit.GetByAbbr("kg")!;
            Amount amount = new Amount(3, unit);
            Price price = new Price(520, Currency.Peso, unit);
            IList<string> keyword = new List<string>();
            keyword.Add("ruleman");
            keyword.Add("metal");
            Material boughtMaterial = Material.CreateInstance("Ruleman Metal", Measure.Weight, category);
            DateTime date = new DateTime(2021, 9, 2, 23, 20, 18);
            BoughtMaterialLine materialbought1 = new BoughtMaterialLine(string.Empty, boughtMaterial, date, price, amount, 0);
            var list = new List<BoughtMaterialLine>();
            list.Add(materialbought1);
            BoughtMaterialLine expected = list[0];
            Assert.AreEqual(expected, materialbought1);

            MaterialCategory category2 = MaterialCategory.GetByName("Plásticos").Unwrap();
            Unit unit2 = Unit.GetByAbbr("g")!;
            Amount amount2 = new Amount(2, unit);
            Price price2 = new Price(2, Currency.Dollar, unit);
            IList<string> keyword2 = new List<string>();
            keyword2.Add("Botella");
            keyword2.Add("plástico");
            Material boughtMaterial2 = Material.CreateInstance("Botella plástico", Measure.Weight, category2);
            DateTime date2 = new DateTime(2021, 7, 8, 20, 17, 19);
            BoughtMaterialLine materialbought2 = new BoughtMaterialLine(string.Empty, boughtMaterial2, date2, price2, amount2, 0);
            list.Add(materialbought2);
            BoughtMaterialLine expected2 = list[1];
            Assert.AreEqual(expected2, materialbought2);
        }

        /// <summary>
        /// Tests the user story of obtain a entrepreneur report
        /// </summary>
        [Test]
        public void CreateEntrepreneurReportTest()
        {
            RuntimeTest.BasicRuntimeTest("report-entrepreneur",() =>
            {
                List<(string, string)> messages = Singleton<ProgramaticMultipleUserPlatform>.Instance.ReceiveMessages(
                    "Entrepreneur1",
                    "/ereport",
                    "23/11/2021"
                );

                Assert.AreEqual(
                    "10.00 cm de Bujes de cartón el día 28/11/2021 a precio de 15 U$/cm (U$ 150)",
                    messages[messages.Count - 1].Item2.Split('\n')[0]);
            });
        }
    }
}