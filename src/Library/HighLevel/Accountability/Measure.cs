using System.Collections.ObjectModel;
using System.Linq;

namespace Library.HighLevel.Accountability
{
    /// <summary>
    /// This class represents a measure through which a certain amount of material can be measured.
    /// We used the pattern Creator, we assigned the method Length and Weigth to Measure because
    /// it's the class that knows about it.
    /// </summary>
    public class Measure
    {
        /// <summary>
        /// The measure's name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The measure's available units.
        /// </summary>
        private readonly Unit[] units;

        /// <summary>
        /// Creates an instance of <see cref="Measure" />, assigning its units in the process.
        /// </summary>
        /// <param name="name">The measure's name.</param>
        /// <param name="unitsData">An array of tuples containing data about its units.</param>
        private Measure(string name, (string name, string abbr, double weight)[] unitsData)
        {
            this.Name = name;
            this.units = unitsData.Select(data => new Unit(data.name, data.abbr, data.weight, this)).ToArray();
        }

        /// <summary>
        /// The length measure.
        /// </summary>
        public static Measure Length = new Measure("Length", new (string, string, double)[]
        {
            ("millimeter", "mm",  0.001),
            ("centimeter", "cm",  0.01),
            ("decimeter",  "dm",  0.1),
            ("meter",      "m",   1),
            ("decameter",  "dam", 10),
            ("hectometer", "hm",  100),
            ("kilometer",  "km",  1000)
        });

        /// <summary>
        /// The weight measure.
        /// </summary>
        public static Measure Weight = new Measure("Weight", new (string, string, double)[]
        {
            ("gram",      "g",   0.001),
            ("decagram",  "dag", 0.01),
            ("hectogram", "hg",  0.1),
            ("kilogram",  "kg",  1),
            ("tonne",     "t",   1000)
        });
    }
}
