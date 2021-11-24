using System;
using System.Threading.Tasks;
using Ucu.Poo.Locations.Client;

namespace Library.Utils
{
    /// <summary>
    /// This class holds functions related to the 
    /// </summary>
    public static class LocationUtils
    {
        /// <summary>
        /// Converts a location into a string equivalent.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns>The string equivalent.</returns>
        public static string LocationToString(Location location) =>
            $"{location.AddresLine}, {location.Locality}, {location.CountryRegion}";

        /// <summary>
        /// Gets a location. This method has higher resiliency towards failed requests via several attempts.
        /// </summary>
        /// <param name="client">The <see cref="LocationApiClient" /> which makes the operation.</param>
        /// <param name="address">The location's address.</param>
        /// <param name="city">The location's city.</param>
        /// <param name="department">The location's department.</param>
        /// <param name="country">The location's country.</param>
        /// <returns>The location.</returns>
        public static Location GetLocationResilient(this LocationApiClient client, string address, string city = "Montevideo", string department = "Montevideo", string country = "Uruguay")
        {
            Location? location = null;
            Exception? e = null;
            for (byte i = 0; i < 10; i++)
            {
                Task<Location> task = client.GetLocationAsync(address, city, department, country);
                if (task.Exception != null)
                {
                    e = task.Exception;
                }
                else if (task.IsCompletedSuccessfully)
                {
                    location = task.Result;
                    break;
                }
            }
            
            if (location == null) throw e!;
            return location;
        }

        /// <summary>
        /// Gets the distance between two locations. This method has higher resiliency towards failed requests via several attempts.
        /// </summary>
        /// <param name="client">The <see cref="LocationApiClient" /> which makes the operation.</param>
        /// <param name="from">The first location.</param>
        /// <param name="to">The second location.</param>
        /// <returns>The distance.</returns>
        public static Distance GetDistanceResilient(this LocationApiClient client, Location from, Location to)
        {
            Distance? distance = null;
            Exception? e = null;
            for (byte i = 0; i < 10; i++)
            {
                Task<Distance> task = client.GetDistanceAsync(from, to);
                if (task.Exception != null)
                {
                    e = task.Exception;
                }
                else if (task.IsCompletedSuccessfully)
                {
                    distance = task.Result;
                    break;
                }
            }
            
            if (distance == null) throw e!;
            return distance;
        }
    }
}