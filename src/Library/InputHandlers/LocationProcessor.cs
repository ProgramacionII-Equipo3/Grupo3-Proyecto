using System;
using System.Globalization;
using System.Threading.Tasks;
using Library;
using Library.InputHandlers;
using Library.InputHandlers.Abstractions;
using Library.HighLevel.Companies;
using Library.Core.Processing;
using Ucu.Poo.Locations.Client;

namespace Library.InputHandlers
{
    /// <summary>
    /// Represents a processor which generates a location from input.
    /// </summary>
    public class LocationProcessor : ProcessorWrapper<Location>
    {
        ///
        public LocationProcessor(Func<string> initialResponseGetter): base(
            PipeProcessor<Location>.CreateInstance<string>(
                s =>
                {
                    string[] sections = s.Split(", ");
                    if (sections.Length != 4)
                        return Result<Location, string>.Err("The given location text is incoherent, it must have address, city, department and country.");
                    
                    Location? location = null;
                    Exception? e = null;
                    for(byte i = 0; i < 10; i++)
                    {
                        Task<Location> task = Singleton<LocationApiClient>.Instance.GetLocationAsync(
                            sections[0].Trim(),
                            sections[1].Trim(),
                            sections[2].Trim(),
                            sections[3].Trim());
                        if(task.Exception != null)
                        {
                            e = task.Exception;
                        } else if (task.IsCompletedSuccessfully)
                        {
                            location = task.Result;
                            break;
                        }
                    }

                    if (location == null) throw e!;

                    if (!location.Found) return Result<Location, string>.Err("The given location is invalid.");

                    return Result<Location, string>.Ok(location);
                },
                new BasicStringProcessor(initialResponseGetter)
            )
        ) {}
    }
}
