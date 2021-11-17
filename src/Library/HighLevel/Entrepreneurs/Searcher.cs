using System.Collections.Generic;
using Library.HighLevel.Materials;
using Ucu.Poo.Locations.Client;

namespace Library.HighLevel.Entrepreneurs
{
    /// <summary>
    /// This class has the responsibility of searching material publication´s 
    /// according to a specified category, keyword or location.
    /// We created this class using expert, this class itself does all the possible 
    /// filter searches). It also has a High Cohesion because of the same reason.
    /// </summary>
    public class Searcher
    {
        /// <summary>
        /// This list is created to contain all the publication's that
        /// are from the specified search.
        /// </summary>
        public IList<MaterialPublication> SearchResult = new List<MaterialPublication>();

        /// <summary>
        /// It creates a client to be able to use the LocationAPI.
        /// </summary>
        /// <returns></returns>
        public LocationApiClient client = new LocationApiClient();

        /// <summary>
        /// This method has the responsibility of searching all the publication's by a category.
        /// </summary>
        /// <param name="publications"></param>
        /// <param name="category"></param>
        public void SearchByCategory(IList<MaterialPublication> publications, MaterialCategory category)
        {
           foreach (var item in publications)
           {
               if (item.Material.Category.Name == category.Name)
               {
                   SearchResult.Add(item);
               }
           }
        }

        /// <summary>
        /// This method has the responsibility of searching all the publication's by a keyword.
        /// </summary>
        /// <param name="publications"></param>
        /// <param name="keyword"></param>
        public void SearchByKeyword(IList<MaterialPublication> publications, string keyword)
        {
           foreach (var item in publications)
           {
               if (item.Keywords.Contains(keyword))
               {
                   SearchResult.Add(item);
               }
           }
        }

        /// <summary>
        /// This method has the responsibility of searching all the publication's by a location.
        /// </summary>
        /// <param name="publications"></param>
        /// <param name="locationSpecified"></param>
        /// <param name="distanceSpecified"></param>
        public void SearchByLocation(IList<MaterialPublication> publications, Location locationSpecified, double distanceSpecified)
        {
           foreach (var item in publications)
           {
               Distance distance;
               distance = client.GetDistanceAsync(locationSpecified, item.PickupLocation).Result;
               if(distance.TravelDistance <= distanceSpecified)
               {
                   SearchResult.Add(item);
               }
           }
        }        
    }
}
