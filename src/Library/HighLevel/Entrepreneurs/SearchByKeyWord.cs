using System.Collections.Generic;
using Library.HighLevel.Materials;
using System.Collections.ObjectModel;

namespace Library.HighLevel.Entrepreneurs
{
    /// <summary>
    /// This class has the responsibility of searching material
    /// publications according to a specified keyword.
    /// </summary>
    public class SearchByKeyword
    {
        string Keyword;

        /// <summary>
        /// This list is created to contain all the publication's that
        /// are from the specified keyword.
        /// </summary>
        public static List<MaterialPublication> keywordSearcher = new List<MaterialPublication>();
        
        /// <summary>
        /// This method has the responsibility of searching all the publications.
        /// </summary>
        /// <param name="materialList"></param>
        public void Search(ReadOnlyCollection<MaterialPublication> materialList)
        {
           foreach (var item in materialList)
           {
               if (item.Keywords.Contains(Keyword))
               {
                   keywordSearcher.Add(item);
               }
           }
        }
    }
}