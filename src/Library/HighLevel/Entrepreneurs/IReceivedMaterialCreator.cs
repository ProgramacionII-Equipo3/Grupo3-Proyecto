using System;
using System.Linq;
using System.Collections.Generic;
using Library.HighLevel.Accountability;

namespace Library.HighLevel.Companies
{
    /// <summary>
    /// This interface was created because of DIP, that way the classes depend of an abstraction.
    /// </summary>
    public interface IReceivedMaterialCreator
    {
        /// <summary>
        /// Gets the collection of bought materials.
        /// </summary>
        protected List<BoughtMaterialLine> BoughtMaterials { get; }

        /// <summary>
        /// Gets a reports of the purchase made by the entrepreneur.
        /// </summary>
        /// <param name="dateTime">It is the time when the entrepreneur bought the material.</param>
        /// <returns>A <see cref="ReceivedMaterialReport" />.</returns>
        public ReceivedMaterialReport GetMaterialReport(DateTime dateTime) =>
            new ReceivedMaterialReport(this.BoughtMaterials.ToList().AsReadOnly());
    }
}
