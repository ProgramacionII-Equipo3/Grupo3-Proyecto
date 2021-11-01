using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Library.HighLevel.Accountability;

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
        private List<Requirement> requirements = new List<Requirement>();

        /// <summary>
        /// A public read-only list of the requirements which are needed to manipulate this material.
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<Requirement> Requirements => this.requirements.AsReadOnly();

        /// <summary>
        /// The category the material belongs to.
        /// </summary>
        public MaterialCategory Category;

        /// <summary>
        /// Saves all the keyword's related to the publication
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <returns></returns>
        public List<string> Keyword = new List<string>();
        private Material(string name, Measure measure, IEnumerable<Requirement> requirements, MaterialCategory category, List<string> keyword)
        {
            this.Name = name;
            this.Measure = measure;
            this.requirements = requirements.ToList();
            this.Category = category;
            this.Keyword = keyword;
        }

        /// <summary>
        /// Creates an instance of <see cref="Material" />, adding it to the collection of materials of its category in the process.
        /// </summary>
        /// <param name="name">The material's name.</param>
        /// <param name="measure">The material's measure.</param>
        /// <param name="requirements">The material's requirements.</param>
        /// <param name="category">The material's category</param>
        /// <returns>A <see cref="Material" /> instance.</returns>
        public Material CreateInstance(string name, Measure measure, IEnumerable<Requirement> requirements, MaterialCategory category, List<string> keyword)
        {
            Material result = new Material(name, measure, requirements, category, keyword);
            category.addMaterial(result);
            return result;
        }

        /// <summary>
        /// Checks whether this material has a concrete name.
        /// </summary>
        /// <param name="name">The name to compare with.</param>
        public bool MatchesName(string name) => this.Name == name;
    }
}
