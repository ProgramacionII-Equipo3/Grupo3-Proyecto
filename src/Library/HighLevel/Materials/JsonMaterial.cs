using Library.Utils;

namespace Library.HighLevel.Materials
{
    /// <summary>
    /// This struct holds the JSON information of a <see cref="Material" />.
    /// </summary>
    public struct JsonMaterial : IJsonHolder<Material>
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
