using System.Collections.Generic;
using Library;
using Library.Core;
using Library.Core.Processing;
using Library.HighLevel.Entrepreneurs;
using Library.InputHandlers;
using Library.InputHandlers.Abstractions;
using Library.Utils;
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
        /// <summary>
        /// Initializes an instance of <see cref="EntrepreneurSearchByZoneState" />.
        /// </summary>
        public EntrepreneurSearchByZoneState(string id): base(
            new InputProcessorState<(Location, double)>(
                new SearchDataProcessor(),
                result =>
                {
                    List<AssignedMaterialPublication> publications = Singleton<Searcher>.Instance.SearchByLocation(result.Item1, result.Item2);
                    return (new EntrepreneurInitialMenuState(id, string.Join('\n', publications)), null);
                },
                () => (new EntrepreneurInitialMenuState(id), null)
            )
        ) {}

        private class SearchDataProcessor : FormProcessor<(Location, double)>
        {
            private Location? location;
            private double? distance;

            public SearchDataProcessor()
            {
                this.inputHandlers = new InputHandler[]
                {
                    ProcessorHandler<Location>.CreateInfallibleInstance(
                        location => this.location = location,
                        new LocationProcessor(() => "Ingrese una dirección para buscar en sus alrededores (formato: Dirección, Ciudad, Departamento, País).")
                    ),
                    ProcessorHandler<double>.CreateInfallibleInstance(
                        distance => this.distance = distance,
                        new UnsignedDoubleProcessor(() => "Ingrese el radio de búsqueda (en metros).")
                    )
                };
            }

            protected override Result<(Location, double), string> getResult() =>
                Result<(Location, double), string>.Ok((this.location.Unwrap(), this.distance.Unwrap()));
        }
    }
}