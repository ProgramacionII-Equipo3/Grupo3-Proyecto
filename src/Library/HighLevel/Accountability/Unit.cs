using System.Collections.Generic;
using System.Linq;

namespace Library.HighLevel.Accountability
{
    /// <summary>
    /// This class represent a unit used for measuring amounts of material.
    /// </summary>
    public class Unit
    {
        /// <summary>
        /// The unit's name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The unit's abbreviation.
        /// </summary>
        public readonly string Abbreviation;

        /// <summary>
        /// A weight associated to the unit to perform conversions with other units of the same measure.
        /// </summary>
        private readonly double weight;

        /// <summary>
        /// The measure form which the unit is.
        /// </summary>
        public readonly Measure Measure;

        /// <summary>
        /// The list of available units.
        /// </summary>
        internal static List<Unit> values = new List<Unit>();

        internal Unit(string name, string abbreviation, double weight, Measure measure)
        {
            this.Name = name;
            this.Abbreviation = abbreviation;
            this.Measure = measure;
            this.weight = weight;

            values.Add(this);
        }

        /// <summary>
        /// Gets the unit which has a concrete abbreviation.
        /// </summary>
        /// <param name="abbreviation">The unit's abbreviation.</param>
        /// <returns></returns>
        public Unit GetByAbbr(string abbreviation) =>
            values.Where(unit => unit.Abbreviation == abbreviation).FirstOrDefault();

        /// <summary>
        /// Calculates the conversion factor to translate measures from a unit to another.
        /// </summary>
        /// <param name="fromUnit">The unit of the initial measure.</param>
        /// <param name="toUnit">The unit of the final measure.</param>
        /// <returns>The number to multiply to the initial measure's numeric value to get the final measure's numeric value, or null if the units belong to different measures.</returns>
        public static double? GetConversionFactor(Unit fromUnit, Unit toUnit) =>
            fromUnit.Measure == toUnit.Measure
                ? fromUnit.weight / toUnit.weight
                : null;
    }
}
