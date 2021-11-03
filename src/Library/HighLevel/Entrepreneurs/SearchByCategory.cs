using System.Collections.Generic;
using Library.HighLevel.Materials;
using System.Collections.ObjectModel;

namespace Library.HighLevel.Entrepreneurs
{
    /// <summary>
    /// This class has the responsibility of searching material
    /// publication´s according to a specified category
    /// </summary>
    public class SearchByCategory
    {

        /// <summary>
        /// This list is created to contain all the publication's that
        /// are from the specified category
        /// </summary>
        public static List<MaterialPublication> categorySearcher = new List<MaterialPublication>();

        /// <summary>
        /// This method has the responsibility of searching all the publication's
        /// </summary>
        /// <param name="publications"></param>
        /// <param name="category"></param>
        public void Search(List<MaterialPublication> publications, MaterialCategory category)
        {
           foreach (var item in publications)
           {
               if (item.Material.Category.Name == category.Name)
               {
                   categorySearcher.Add(item);
               }
           }
        }
    }
}