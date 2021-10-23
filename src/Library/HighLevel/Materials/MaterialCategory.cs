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
        public string Name { get; }

        /// <summary>
        /// The list of existent categories.
        /// </summary>
        /// <value></value>
        public static ReadOnlyCollection<MaterialCategory> Categories = new string[]
        {
            "A", "B", "C"
        }.Select(name => new MaterialCategory(name)).ToList().AsReadOnly();

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
    }
}
