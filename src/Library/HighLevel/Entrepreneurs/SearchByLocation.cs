//using Ucu.Poo.Locations.Client;
using System.Collections.ObjectModel;
using Library.HighLevel.Materials;
using System.Collections.Generic;
using Library.HighLevel.Accountability;

namespace Library.HighLevel.Entrepreneurs
{
    /// <summary>
    /// This class has the responsibility of searching material 
    /// publication's near a specified location
    /// </summary>
    public class SearchByLocation
    {
        //Location LocationSpecified;

        /// <summary>
        /// This list is created to contain all the publication's that.
        /// are near a specified location.
        /// </summary>

        public static List<MaterialPublication> locationSearcher = new List<MaterialPublication>();

        /// <summary>
        /// This method has the responsibility of searching all the publication's.
        /// </summary>
        /// <param name="materialList"></param>
        /// <returns></returns>
        /*public async void Search(ReadOnlyCollection<MaterialPublication> materialList)
        {
            LocationApiClient client = new LocationApiClient();
           foreach (var item in materialList)
           {
               float distance;
               distance = await client.GetDistanceAsync(LocationSpecified, item.PickupLocation);
               if(distance <= 5)
               {
                   locationSearcher.Add(item);
               }
           }
        }*/
    }
}