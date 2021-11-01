using System.Collections.Generic;
using Library.HighLevel.Materials;

namespace Library.HighLevel.Entrepreneurs
{
    public class SearchByCategory
    {
        string Category;
        public static List<MaterialPublication> categorySearcher = new List<MaterialPublication>();

        public void Search(List<MaterialPublication> materialList)
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