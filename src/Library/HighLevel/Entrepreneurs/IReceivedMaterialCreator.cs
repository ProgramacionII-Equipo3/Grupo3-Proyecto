using Library.HighLevel.Accountability;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Library.HighLevel.Companies
{
    public interface IReceivedMaterialCreator
    {
        /// <summary>
        /// It is the collection of bought materials 
        /// </summary>
        /// <value></value>
        protected List<MaterialBoughtLine> materialBought { get; }


        /// <summary>
        /// It creates a reports of the purchase made by the entrepreneur
        /// </summary>
        /// <param name="dateTime">It is the time when the entrepreneur bought the material</param>
        /// <returns></returns>
        ReceivedMaterialReport MaterialReport(DateTime dateTime)
        {
            new ReceivedMaterialReport(this.materialBought.ToList().AsReadOnly());
        }

    }
}