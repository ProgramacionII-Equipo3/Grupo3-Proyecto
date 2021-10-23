using System.Collections.Generic;

namespace Library.Core.Company
{
    /// <summary>
    /// This class represents a sent material report, it stores
    /// all the reports and search a specific one
    /// </summary>
    public class SentMaterialReport : ISentMaterialReportCreator
    {
        /// <summary>
        /// List of lines of the sent material report
        /// </summary>
        public ReadOnlyList<SentMaterialLine> Lines;

        /// <summary>
        /// The SentMaterialReport Constructor
        /// </summary>
        /// <param name="lines">SentMaterialReport's lines</param>
        public SentMaterialReport(ReadOnlyList<SentMaterialLine> lines)
        {
            this.Lines = lines;
        }

        /// <summary>
        /// This method returns a specific material line 
        /// </summary>
        /// <returns></returns>
        public static SentMaterialReport GetMaterialReport(DateTime dateTime)
        {
            SentMaterialReport result = null;
            foreach (SentMaterialLine line in this.lines)
            {
                if (SentMaterialLine.timestamp == dateTime)
                {
                    result = SentMaterialLine;
                }
            }
            return result;
        }
    }
}
