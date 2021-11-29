using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Library.HighLevel.Accountability
{
    /// <summary>
    /// This class represents a report of all materials a company sold over a certain period of time.
    /// Created because of SRP, a report is conformed by lines (created by another class), that way
    /// also avoid High Coupling, but have a High Cohesion. At the same time the sent material and
    /// received one are created by different classes because of polymorphism.
    /// </summary>
    public class SentMaterialReport
    {
        /// <summary>
        /// The list of material sales.
        /// </summary>
        public readonly ReadOnlyCollection<MaterialSalesLine> Lines;

        /// <summary>
        /// Initializes a new instance of the <see cref="SentMaterialReport"/> class.
        /// </summary>
        /// <param name="lines">The report's list of material sales.</param>
        public SentMaterialReport(ReadOnlyCollection<MaterialSalesLine> lines)
        {
            this.Lines = lines;
        }

        /// <summary>
        /// This method returns a list of materials that were sold in a certain period of time.
        /// </summary>
        /// <param name="materialSales">The list of materials sold.</param>
        /// <param name="time">The period of time to search.</param>
        /// <returns>A report of materials sended in a period of time.</returns>
        public static IList<MaterialSalesLine> GetSentReport(IList<MaterialSalesLine> materialSales, int time)
        {

            IList<MaterialSalesLine> result = materialSales.Where(
                delegate(MaterialSalesLine materialSale)
                {
                    return materialSale.DateTime.Month > DateTime.Now.Month - time;
                }).ToList();
            return result;
        }

        /// <inheritdoc />
        public override string? ToString() =>
            this.Lines.Count > 0
                ? string.Join('\n', this.Lines.Select(l => $"({l.SaleID}) {l}"))
                : "Reporte vacío.";
    }
}
