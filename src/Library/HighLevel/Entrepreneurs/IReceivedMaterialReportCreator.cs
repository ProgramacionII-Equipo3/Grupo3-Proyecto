using System;
using System.Collections.Generic;
using System.Linq;
using Library.HighLevel.Accountability;

namespace Library.HighLevel.Companies
{
    /// <summary>
    /// This interface represents a class which can create <see cref="ReceivedMaterialReport" />s.
    /// It was created because of DIP, that way the classes depend of an abstraction.
    /// </summary>
    public interface IReceivedMaterialReportCreator
    {
        /// <summary>
        /// Gets the collection of bought materials.
        /// </summary>
        protected IList<BoughtMaterialLine> BoughtMaterials { get; }

        /// <summary>
        /// Gets a reports of the purchase made by the entrepreneur.
        /// </summary>
        /// <param name="dateTime">It is the time when the entrepreneur bought the material.</param>
        /// <returns>A <see cref="ReceivedMaterialReport" />.</returns>
        public ReceivedMaterialReport GetMaterialReport(DateTime dateTime) =>
            new ReceivedMaterialReport(this.BoughtMaterials.ToList().AsReadOnly());
    }
}
