using Library.HighLevel.Accountability;

namespace Library.HighLevel.Materials
{
    /// <summary>
    /// This class represents a type of material which can be sold by companies and purchased by entrepreneurs.
    /// We used the principle Creator, Material is the class in charge of creating instance of itself.
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

        
        //private List<Requirement> requirements = new List<Requirement>();
        //public ReadOnlyCollection<Requirement> Requirements => this.requirements.AsReadOnly();

        /// <summary>
        /// The category the material belongs to.
        /// </summary>
        public MaterialCategory Category;

        private Material(string name, Measure measure, MaterialCategory category)
        {
            this.Name = name;
            this.Measure = measure;
            this.Category = category;
        }

        /// <summary>
        /// Creates an instance of <see cref="Material" />, adding it to the collection of materials of its category in the process.
        /// </summary>
        /// <param name="name">The material's name.</param>
        /// <param name="measure">The material's measure.</param>
        /// <param name="category">The material's category</param>
        /// <returns>A <see cref="Material" /> instance.</returns>

        public static Material CreateInstance(string name, Measure measure, MaterialCategory category)
        {
            Material result = new Material(name, measure, category);
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
