using System;
using System.Globalization;
using Library;
using Library.InputHandlers;
using Library.HighLevel.Companies;
using Library.Core.Processing;
using Ucu.Poo.Locations.Client;

namespace Library.InputHandlers
{
    /// <summary>
    /// Represents a processor which generates a location from input.
    /// </summary>
    public class LocationProcessor : FormProcessor<Location>
    {

        private string address;
        private string city;
        private string department;
        private string country;

        private Location result = null;

        /// <param name="locationName">The name of the object of which the program wants the location.</param>
        public LocationProcessor(string locationName)
        {
            this.inputHandlers = new IInputHandler[]
            {
                    ProcessorHandler.CreateInstance<string>(
                        s => this.address = s,
                        new BasicStringProcessor(() => $"Please insert the {locationName}'s address.")
                    ),
                    ProcessorHandler.CreateInstance<string>(
                        s => this.city = s,
                        new BasicStringProcessor(() => $"Please insert the {locationName}'s city.")
                    ),
                    ProcessorHandler.CreateInstance<string>(
                        s => this.department = s,
                        new BasicStringProcessor(() => $"Please insert the {locationName}'s department.")
                    ),
                    ProcessorHandler.CreateInstance<string>(
                        s => this.country = s,
                        new BasicStringProcessor(() => $"Please insert the {locationName}'s country.")
                    )
            };
        }

        /// <inheritdoc />
        protected override Result<Location, string> getResult()
        {
            Location result = Utils.GetLocation(this.address, this.city, this.department, this.country);
            if (!result.Found) return Result<Location, string>.Err("The location is not valid.");
            else return Result<Location, string>.Ok(result);
        }
    }
}
