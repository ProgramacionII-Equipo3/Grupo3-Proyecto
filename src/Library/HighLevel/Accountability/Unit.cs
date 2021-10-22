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
        public string Name { get; set; }

        /// <summary>
        /// The unit's abbreviation.
        /// </summary>
        public string Abbreviation { get; set; }

        /// <summary>
        /// A weight associated to the unit to perform conversions with other units of the same measure.
        /// </summary>
        private double weight { get; set; }

        /// <summary>
        /// The measure form which the unit is.
        /// </summary>
        public Measure Measure { get; }

        /// <summary>
        /// The list of available units.
        /// </summary>
        internal static List<Unit> values = new List<Unit>();

        private Unit(string name, string abbreviation, Measure measure)
        {
            this.Name = name;
            this.Abbreviation = abbreviation;
            this.Measure = measure;

            values.Add(this);
        }

        /// <summary>
        /// Gets the unit which has a concrete abbreviation.
        /// </summary>
        /// <param name="abbreviation">The unit's abbreviation.</param>
        /// <returns></returns>
        public Unit GetByAbbr(string abbreviation) =>
            values.Where(unit => unit.Abbreviation == abbreviation).FirstOrDefault();
    }
}
