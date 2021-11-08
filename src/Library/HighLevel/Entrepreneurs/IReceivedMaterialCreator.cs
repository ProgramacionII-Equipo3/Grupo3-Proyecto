using Library.HighLevel.Accountability;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Library.HighLevel.Companies
{
    ///
    public interface IReceivedMaterialCreator
    {
        /// <summary>
        /// It is the collection of bought materials.
        /// </summary>
        protected List<BoughtMaterialLine> materialBought { get; }


        /// <summary>
        /// It creates a report of the purchase made by the entrepreneur.
        /// </summary>
        /// <param name="dateTime">The time when the entrepreneur bought the materials.</param>
        public ReceivedMaterialReport GetMaterialReport(DateTime dateTime) =>
        
            new ReceivedMaterialReport(this.materialBought.ToList().AsReadOnly());
        
    }
}