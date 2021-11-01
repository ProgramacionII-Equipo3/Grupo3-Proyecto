using System.Collections.Generic;
using Library.HighLevel.Materials;

namespace Library.HighLevel.Entrepreneurs
{
    public class SearchByKeyword
    {
        string Keyword;
        public static List<MaterialPublication> keywordSearcher = new List<MaterialPublication>();

        public void Search(List<MaterialPublication> materialList)
        {
           foreach (var item in materialList)
           {
               if (item.Material.Keyword.Contains(Keyword))
               {
                   keywordSearcher.Add(item);
               }
           }
        }
    }
}