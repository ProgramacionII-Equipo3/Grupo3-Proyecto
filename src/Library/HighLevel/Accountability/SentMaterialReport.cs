using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Library.HighLevel.Accountability
{
    /// <summary>
    /// This class represents a report of all materials a company sold over a certain period of time.
    /// Created because of SRP.
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
        public static List<MaterialSalesLine> GetSentReport(List<MaterialSalesLine> materialSales, int time)
        {

            List<MaterialSalesLine> result = materialSales.FindAll(
                delegate(MaterialSalesLine materialSale)
                {
                    return materialSale.DateTime.Month > DateTime.Now.Month - time;
                });
            return result;
        }
    }
}
