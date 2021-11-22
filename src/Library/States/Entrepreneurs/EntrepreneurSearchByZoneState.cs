using System.Collections.Generic;
using Library;
using Library.Core;
using Library.Core.Processing;
using Library.HighLevel.Entrepreneurs;
using Library.InputHandlers;
using Library.InputHandlers.Abstractions;
using System.Linq;
using Library.HighLevel.Materials;
using Ucu.Poo.Locations.Client;

namespace Library.States.Entrepreneurs
{
    /// <summary>
    /// This class represents the state of an entrepreneur that is searching publications with a location and distance.
    /// </summary>
    public class EntrepreneurSearchByZoneState : WrapperState
    {
        public double distanceSpecified;

        /// <summary>
        /// Initializes an instance of <see cref="EntrepreneurSearchByZoneState" />.
        /// </summary>
        public EntrepreneurSearchByZoneState(): base(
            InputProcessorState.CreateInstance<Location>(
                new LocationProcessor(() => "Please insert the Location you want to search."),
                locationSpecified =>
                {
                    List<AssignedMaterialPublication> publications = Singleton<Searcher>.Instance.SearchByLocation(locationSpecified, distanceSpecified);
                    return (new EntrepreneurMenuState(string.Join('\n', publications)), null);
                },
                () => (new EntrepreneurMenuState(), null)
            )
        ) {}
    }
}