using System;
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
        /// <param name="predicate">A function which determines if a name is valid.</param>
        public MaterialProcessor(Func<string, string?> predicate)
        {
            this.inputHandlers = new InputHandler[]
            {
                new ProcessorHandler<string>(
                    n =>
                    {
                        if(predicate(n) is string error) return error;
                        this.name = n;
                        return null;
                    },
                    new BasicStringProcessor(() => "Por favor ingresa el nombre del material.")
                ),
                ProcessorHandler<Measure>.CreateInfallibleInstance(
                    m => this.measure = m,
                    new MeasureProcessor(() => "Por favor ingresa la medida del material:\n        \"weight\": peso\n        \"length\": longitud\n        \"volume\": volumen")
                ),
                ProcessorHandler<MaterialCategory>.CreateInfallibleInstance(
                    c => this.category = c,
                    new MaterialCategoryProcessor(() => "Por favor ingresa la categoría del material."))
            };
        }


        /// <inheritdoc />
        protected override Result<Material, string> getResult() =>
            Result<Material, string>.Ok(Material.CreateInstance(this.name.Unwrap(), this.measure.Unwrap(), this.category.Unwrap()));
    }
}