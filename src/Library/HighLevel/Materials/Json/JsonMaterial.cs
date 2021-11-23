using Library.Utils;

namespace Library.HighLevel.Materials.Json
{
    /// <summary>
    /// This class represents the JSON information of a material.
    /// </summary>
    public struct JsonMaterial
    {
        /// <summary>
        /// The material's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The material's measure.
        /// </summary>
        public string Measure { get; set; }

        /// <summary>
        /// The material's category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Converts to a material.
        /// </summary>
        /// <returns>The material.</returns>
        public Material ToMaterial() =>
            Material.CreateInstance(
                this.Name,
                Accountability.Measure.GetByName(this.Measure).Unwrap(),
                MaterialCategory.GetByName(this.Category).Unwrap());

        /// <summary>
        /// Converts a material into a <see cref="JsonMaterial" />.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <returns>A <see cref="JsonMaterial" />.</returns>
        public static JsonMaterial FromMaterial(Material material) =>
            new JsonMaterial()
            {
                Name = material.Name,
                Measure = material.Measure.Name,
                Category = material.Category.Name
            };
    }
}
