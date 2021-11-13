using System.Collections.ObjectModel;

namespace Library.HighLevel.Accountability
{
    /// <summary>
    /// This class represents a reports of all material purchased by the entrepreneur.
    /// We used the pattern Creator and the SRP principle, that is why this class 
    /// creates a report and not another class to avoid High Coupling.
    /// </summary>
    public class ReceivedMaterialReport
    {
        /// <summary>
        /// The list of purchased materials.
        /// </summary>
        public readonly ReadOnlyCollection<BoughtMaterialLine> Materials;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceivedMaterialReport"/> class.
        /// </summary>
        /// <param name="materials">The collection of purchased materials.</param>
        public ReceivedMaterialReport(ReadOnlyCollection<BoughtMaterialLine> materials)
        {
            this.Materials = materials;
        }
    }
}
