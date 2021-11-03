using System.Collections.Generic;
using Library.HighLevel.Materials;
using System.Collections.ObjectModel;

namespace Library.HighLevel.Entrepreneurs
{
    /// <summary>
    /// This class has the responsibility of searching material
    /// publication´s according to a specified keyword
    /// </summary>
    public class SearchByKeyword
    {

        /// <summary>
        /// This list is created to contain all the publication's that
        /// are from the specified keyword
        /// </summary>
        public static List<MaterialPublication> keywordSearcher = new List<MaterialPublication>();
        
        /// <summary>
        /// This method has the responsibility of searching all the publication's
        /// </summary>
        /// <param name="publications"></param>
        /// /// <param name="keyword"></param>
        public void Search(List<MaterialPublication> publications, string keyword)
        {
           foreach (var item in publications)
           {
               if (item.Material.Keyword.Contains(keyword))
               {
                   keywordSearcher.Add(item);
               }
           }
        }
    }
}