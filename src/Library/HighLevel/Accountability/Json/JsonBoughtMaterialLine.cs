using System;
using System.Linq;
using Library.HighLevel.Companies;
using Library.HighLevel.Materials;
using Library.Utils;

namespace Library.HighLevel.Accountability.Json
{
    ///
    public struct JsonBoughtMaterialLine
    {
        ///
        public string CompanyName { get; }

        ///
        public Material Material { get; }

        ///
        public DateTime DateTime { get; }

        ///
        public Price Price { get; }

        ///
        public Amount Amount { get; }

        ///
        public BoughtMaterialLine ToMaterialLine()
        {
            var material = this.Material;
            new BoughtMaterialLine(
                this.CompanyName,
                Singleton<CompanyManager>.Instance.GetByName(this.CompanyName).Unwrap().Publications.Select(p => p.Publication).Where(p => p.Material.Name == material.Name)).
        }
    }
}