using System.Collections.ObjectModel;

namespace Library.HighLevel.Accountability
{
    /// <summary>
    /// This class represents a reports of all material purchased by the entrepreneur.
    /// </summary>
    public class ReceivedMaterialReport
    {
        /// <summary>
        /// The list of purchased materials.
        /// </summary>
        public readonly ReadOnlyCollection<BoughtMaterialLine> Materials;
        
        /// <summary>
        /// Creates a <see cref="ReceivedMaterialReport"/>.
        /// </summary>
        /// <param name="materials">The collection of purchased materials.</param>
        public ReceivedMaterialReport(ReadOnlyCollection<BoughtMaterialLine> materials)
        {
            this.Materials = materials;
        }
    }
}