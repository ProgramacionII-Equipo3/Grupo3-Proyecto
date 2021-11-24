using Library.Utils;

namespace Library.HighLevel.Materials
{
    /// <summary>
    /// This class holds the JSON information of a <see cref="Material" />.
    /// </summary>
    public class JsonMaterial : IJsonHolder<Material>
    {
        /// <summary>
        /// The material's name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The material's measure.
        /// </summary>
        public string Measure { get; set; } = string.Empty;

        /// <summary>
        /// The material's category.
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <inheritdoc />
        public Material ToValue() =>
            Material.CreateInstance(
                this.Name,
                Accountability.Measure.GetByName(this.Measure).Unwrap(),
                MaterialCategory.GetByName(this.Category).Unwrap());

        /// <inheritdoc />
        public void FromValue(Material value)
        {
            this.Name = value.Name;
            this.Measure = value.Measure.Name;
            this.Category = value.Category.Name;
        }
    }
}
