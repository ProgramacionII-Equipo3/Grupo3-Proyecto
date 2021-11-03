using Ucu.Poo.Locations.Client;
using System.Threading.Tasks;
namespace Library.HighLevel.Accountability
{
    /// <summary>
    /// This class represents a location in the world.
    /// </summary>
    public class Location
    {
        Task<Location> Localization { get; set; }
        public Location(string address, string city, string department, string country)
        {
            LocationApiClient client = new LocationApiClient();
            this.Localization = client.GetLocationAsync(address, city, department, country);
        }
        

        /// <summary>
        /// Calculates the distance between two <see cref="Location" />s.
        /// </summary>
        /// <param name="l1">The first location.</param>
        /// <param name="l2">The second location.</param>
        /// <returns>The distance between the two locations, in kilometers.</returns>
        public static double Distance(Location l1, Location l2) => -1;
    }
}
