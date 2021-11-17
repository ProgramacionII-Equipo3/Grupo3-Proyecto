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
        /// It creates a client to be able to use the LocationAPI.
        /// </summary>

        /// <returns></returns>
        public LocationApiClient client = new LocationApiClient();


        /// <summary>
        /// This method has the responsibility of searching all the publication's by a category.
        /// </summary>

        /// <param name="publications"></param>
        /// <param name="category"></param>
        public List<MaterialPublication> SearchByCategory(List<MaterialPublication> publications, MaterialCategory category)
        {
           List<MaterialPublication> searchResultA = new List<MaterialPublication>();
           foreach (var item in publications)
           {
               if (item.Material.Category.Name == category.Name)
               {
                   searchResultA.Add(item);
               }
           }
           return searchResultA;
        }

        /// <summary>
        /// This method has the responsibility of searching all the publication's by a keyword.
        /// </summary>

        /// <param name="publications"></param>
        /// <param name="keyword"></param>
        public List<MaterialPublication> SearchByKeyword(List<MaterialPublication> publications, string keyword)
        {
           List<MaterialPublication> searchResultB = new List<MaterialPublication>();
           foreach (var item in publications)
           {
               if (item.Keywords.Contains(keyword))
               {
                   searchResultB.Add(item);
               }
           }
           return searchResultB;
        }

        /// <summary>
        /// This method has the responsibility of searching all the publication's by a location.
        /// </summary>

        /// <param name="publications"></param>
        /// <param name="locationSpecified"></param>
        /// <param name="distanceSpecified"></param>
        public List<MaterialPublication> SearchByLocation(List<MaterialPublication> publications, Location locationSpecified, double distanceSpecified)
        {
           List<MaterialPublication> searchResultC = new List<MaterialPublication>();
           foreach (var item in publications)
           {
               Distance distance;
               distance = client.GetDistanceAsync(locationSpecified, item.PickupLocation).Result;
               if (distance.TravelDistance <= distanceSpecified)
               {
                   searchResultC.Add(item);
               }
           }
           return searchResultC;
        }        
    }
}