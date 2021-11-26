using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Library.HighLevel.Materials
{
    /// <summary>
    /// This class represents a category of materials, through which they can be easily classified.
    /// Created because of SRP and LSP, the category can be used by any class without problems.
    /// </summary>
    public class MaterialCategory
    {
        private readonly string StringValue;

        /// <summary>
        /// The category's name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The list of existent categories.
        /// </summary>
        public static ReadOnlyCollection<MaterialCategory> Categories = new string[]
        {
            "Metales", "Plásticos", "Materiales técnicos", "Químicos", "Otros", "Celulósicos", "Eléctricos", "Textiles", "Metálicos ferrosos", "Metálicos", "Electrónica e Informática", "Solventes", "Vidrio", "Residuos orgánicos", "Residuos y subproductos"
        }.Select(name => new MaterialCategory(name)).ToList().AsReadOnly();

        /// <summary>
        /// The list of materials which belong to this category.
        /// The class <see cref="List{T}" /> is used instead of the interface <see cref="IList{T}" />
        /// because the method <see cref="List{T}.AsReadOnly()" /> is necessary for the property <see cref="MaterialCategory.Materials" />.
        /// </summary>
        private List<Material> materials = new List<Material>();

        /// <summary>
        /// A public, read-only list of materials which belong to this category.
        /// </summary>
        public ReadOnlyCollection<Material> Materials => materials.AsReadOnly();

        /// <summary>
        /// Initializes an instance of <see cref="MaterialCategory" /> class.
        /// </summary>
        /// <param name="name">The material's category name.</param>
        public MaterialCategory(string name)
        {
            this.StringValue = name;
            this.Name = name.ToLowerInvariant();
        }

        internal void AddMaterial(Material material)
        {
            this.materials.Add(material);
        }

        /// <summary>
        /// Gets a concrete <see cref="MaterialCategory" /> given its name.
        /// </summary>
        /// <param name="name">The category's name.</param>
        /// <returns>An instance of <see cref="MaterialCategory" />.</returns>
        public static MaterialCategory? GetByName(string name)
        {
            name = name.ToLowerInvariant();
            return MaterialCategory.Categories.Where(c => c.Name.ToLowerInvariant() == name).FirstOrDefault();
        }

        /// <inheritdoc />
        public override string ToString() => this.StringValue;
    }
}
