using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;

namespace Library.HighLevel.Accountability
{
    /// <summary>
    /// This class represents a report of all materials a company sold over a certain period of time.
    /// </summary>
    public class SentMaterialReport
    {
        /// <summary>
        /// The list of material sales.
        /// </summary>
        public readonly ReadOnlyCollection<MaterialSalesLine> Lines;

        /// <summary>
        /// Creates an instance of <see cref="SentMaterialReport"/>.
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
        /// <returns></returns>
        public static List<MaterialSalesLine> GetSentReport(List<MaterialSalesLine> materialSales, int time)
        {

            List<MaterialSalesLine> result = materialSales.FindAll(
                delegate(MaterialSalesLine materialSale)
                {
                    return materialSale.DateTime.Month > DateTime.Now.Month - time;
                }
            );
            return result;
        }
    }
}
