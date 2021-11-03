using System.Collections.Generic;
using Library.HighLevel.Materials;
using System.Collections.ObjectModel;

namespace Library.HighLevel.Entrepreneurs
{
    /// <summary>
    /// This class has the responsibility of searching material.
    /// publication´s according to a specified category.
    /// </summary>
    public class SearchByCategory
    {
        string Category;

        /// <summary>
        /// This list is created to contain all the publication's that.
        /// are from the specified category.
        /// </summary>
        public static List<MaterialPublication> categorySearcher = new List<MaterialPublication>();

        /// <summary>
        /// This method has the responsibility of searching all the publication's.
        /// </summary>
        /// <param name="materialList"></param>
        public void Search(ReadOnlyCollection<MaterialPublication> materialList)
        {
           foreach (var item in materialList)
           {
               if (item.Material.Category.Name == Category)
               {
                   categorySearcher.Add(item);
               }
           }
        }
    }
}