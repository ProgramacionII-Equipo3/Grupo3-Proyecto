using System.Collections.Generic;
using System.Linq;

namespace Library.HighLevel.Accountability
{
    /// <summary>
    /// This class represent a unit used for measuring amounts of material.
    /// Created because of SRP.
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

        /// <summary>
        /// Creates and instance of Unit.
        /// </summary>
        /// <param name="name">Unit´s name.</param>
        /// <param name="abbreviation">Unit´s abbreviation.</param>
        /// <param name="weight">Unit´s associated weight.</param>
        /// <param name="measure">Unit´s measure.</param>
        public Unit(string name, string abbreviation, double weight, Measure measure)
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
        public static Option<double> GetConversionFactor(Unit fromUnit, Unit toUnit) =>
            Unit.AreCompatible(fromUnit, toUnit)
                ? Option<double>.From(fromUnit.weight / toUnit.weight)
                : Option<double>.None;

        /// <summary>
        /// Checks whether two units are compatible with each other.
        /// That is, whether they belong to the same measure.
        /// </summary>
        public static bool AreCompatible(Unit u1, Unit u2) => u1.Measure == u2.Measure;
    }
}
