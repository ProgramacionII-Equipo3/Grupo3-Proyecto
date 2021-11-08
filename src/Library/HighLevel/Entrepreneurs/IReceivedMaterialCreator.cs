using Library.HighLevel.Accountability;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Library.HighLevel.Companies
{
    /// <summary>
    /// This interface was created because of SRP and OCP.
    /// </summary>
    public interface IReceivedMaterialCreator
    {
        /// <summary>
        /// It is the collection of bought materials.
        /// </summary>
        protected List<BoughtMaterialLine> boughtMaterials { get; }

        /// <summary>
        /// It creates a reports of the purchase made by the entrepreneur.
        /// </summary>
        /// <param name="dateTime">It is the time when the entrepreneur bought the material</param>
        /// <returns>A <see cref="ReceivedMaterialReport" />.</returns>
        public ReceivedMaterialReport GetMaterialReport(DateTime dateTime) =>
            new ReceivedMaterialReport(this.boughtMaterials.ToList().AsReadOnly());
        
    }
}