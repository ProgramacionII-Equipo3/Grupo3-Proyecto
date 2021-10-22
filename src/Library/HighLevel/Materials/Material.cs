using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Library.HighLevel.Materials
{
    /// <summary>
    /// This class represents a type of material which can be sold by companies and purchased by entrepreneurs.
    /// </summary>
    public class Material
    {
        /// <summary>
        /// The material's name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The measure with which the amounts of the material are measured.
        /// </summary>
        public Measure Measure { get; private set; }

        /// <summary>
        /// A private list of the requirements which are needed to manipulate this material.
        /// </summary>
        private List<IRequirement> requirements = new List<IRequirement>();

        /// <summary>
        /// A public read-only list of the requirements which are needed to manipulate this material.
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<IRequirement> Requirements => this.requirements.AsReadOnly();

        /// <summary>
        /// The category the material belongs to.
        /// </summary>
        public MaterialCategory Category;

        private Material(string name, Measure measure, IEnumerable<IRequirement> requirements, MaterialCategory category)
        {
            this.Name = name;
            this.Measure = measure;
            this.requirements = requirements.ToList();
            this.Category = category;
        }

        /// <summary>
        /// Creates a <see cref="Material" /> instance, adding it to the collection of materials of its category in the process.
        /// </summary>
        /// <param name="name">The material's name.</param>
        /// <param name="measure">The material's measure.</param>
        /// <param name="requirements">The material's requirements.</param>
        /// <param name="category">The material's category</param>
        /// <returns>A <see cref="Material" /> instance.</returns>
        public Material CreateInstance(string name, Measure measure, IEnumerable<IRequirement> requirements, MaterialCategory category)
        {
            Material result = new Material(name, measure, requirements, category);
            category.AddMaterial(result);
            return result;
        }
    }
}
