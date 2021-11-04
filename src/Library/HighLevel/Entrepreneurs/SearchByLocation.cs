using Ucu.Poo.Locations.Client;
using Library.HighLevel.Materials;
using System.Collections.Generic;
using Library.HighLevel.Accountability;

namespace Library.HighLevel.Entrepreneurs
{
    /// <summary>
    /// This class has the responsibility of searching material 
    /// publication's near a specified location.
    /// It was created because of polymorphism.
    /// </summary>
    public class SearchByLocation
    {
        /// <summary>
        /// This list is created to contain all the publication's that.
        /// are near a specified location.
        /// </summary>

        public static List<MaterialPublication> locationSearcher = new List<MaterialPublication>();

        /// <summary>
        /// This method has the responsibility of searching all the publication's.
        /// </summary>
        /// <param name="publications"></param>
        /// <param name="locationSpecified"></param>
        /// <param name="distanceSpecified"></param>
        /// <returns></returns>
        public async void Search(List<MaterialPublication> publications, Location locationSpecified, double distanceSpecified)
        {
           LocationApiClient client = new LocationApiClient();
           foreach (var item in publications)
           {
               Distance distance;
               distance = await client.GetDistanceAsync(locationSpecified, item.PickupLocation);
               if(distance.TravelDistance <= distanceSpecified)
               {
                   locationSearcher.Add(item);
               }
           }
        }
    }
}