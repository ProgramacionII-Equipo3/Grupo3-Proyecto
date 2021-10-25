using System.Collections.ObjectModel;

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
    }
}
