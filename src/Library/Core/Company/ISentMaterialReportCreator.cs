using System.Collections.Generic;

namespace Library.Core.Company
{
    /// <summary>
    /// This interface represents the responsibility 
    /// of creating a material report
    /// </summary>
    public interface ISentMaterialReportCreator
    {
        /// <summary>
        /// Represents the list of material sales
        /// </summary>
        public List<SentMaterialLine> MaterialSales;

        /// <summary>
        /// This method returns a specific material line 
        /// </summary>
        /// <returns></returns>
        public static SentMaterialReport GetMaterialReport(DateTime dateTime);
    }
}