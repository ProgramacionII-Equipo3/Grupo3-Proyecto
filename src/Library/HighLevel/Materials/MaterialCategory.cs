using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Library.HighLevel.Materials
{
    /// <summary>
    /// This class represents a category of materials, through which they can be easily classified.
    /// </summary>
    public class MaterialCategory
    {
        /// <summary>
        /// The category's name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The list of existent categories.
        /// </summary>
        public static ReadOnlyCollection<MaterialCategory> Categories = new string[]
        {
            "A", "B", "C"
        }.Select(name => new MaterialCategory(name.ToLowerInvariant())).ToList().AsReadOnly();

        private List<Material> materials = new List<Material>();

        /// <summary>
        /// The list of materials which belong to this category.
        /// </summary>
        public readonly ReadOnlyCollection<Material> Materials;

        private MaterialCategory(string name)
        {
            this.Name = name;
            this.Materials = materials.AsReadOnly();
        }

        internal void addMaterial(Material material)
        {
            this.materials.Add(material);
        }

        /// <summary>
        /// Gets a concrete <see cref="MaterialCategory" /> given its name.
        /// </summary>
        /// <param name="name">The category's name.</param>
        /// <returns>An instance of <see cref="MaterialCategory" />.</returns>
        public Material GetByName(string name)
        {
            name = name.ToLowerInvariant();
            return this.materials.Where(material => material.Name.Equals(name)).FirstOrDefault();
        }
    }
}
