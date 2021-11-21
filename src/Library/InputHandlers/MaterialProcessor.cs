using Library.Core.Processing;
using Library.HighLevel.Accountability;
using Library.InputHandlers.Abstractions;
using Library.HighLevel.Materials;
using Library.Utils;

namespace Library.InputHandlers
{
    /// <summary>
    /// This class has the responsibility of create an material with the input data of the user.
    /// </summary>
    public class MaterialProcessor : FormProcessor<Material>
    {
        private string? name;
        private Measure? measure;
        private MaterialCategory? category;

        /// <summary>
        /// Initializes an instance of <see cref="MaterialProcessor" />
        /// </summary>
        public MaterialProcessor()
        {
            this.inputHandlers = new InputHandler[]
            {
                ProcessorHandler.CreateInfallibleInstance<string>(
                    n => this.name = n,
                    new BasicStringProcessor(() => "Por favor ingresa el nombre del material.")
                ),
                ProcessorHandler.CreateInfallibleInstance<Measure>(
                    m => this.measure = m,
                    new MeasureProcessor(() => "Por favor ingresa la medida del material:\n        \"weight\": peso\n        \"length\": longitud")
                ),
                ProcessorHandler.CreateInfallibleInstance<MaterialCategory>(
                    c => this.category = c,
                    new MaterialCategoryProcessor(() => "Por favor ingresa la categoría del material."))
            };
        }


        /// <inheritdoc />
        protected override Result<Material, string> getResult() =>
            Result<Material, string>.Ok(Material.CreateInstance(this.name.Unwrap(), this.measure.Unwrap(), this.category.Unwrap()));
    }
}