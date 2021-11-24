using System;
using System.Linq;
using Library.Core.Processing;
using Library.HighLevel.Materials;
using Library.InputHandlers.Abstractions;

namespace Library.InputHandlers
{
    /// <summary>
    /// This class represents an <see cref="InputProcessor{T}" /> which generates objects of type <see cref="MaterialCategory" />.
    /// </summary>
    public class MaterialCategoryProcessor : ProcessorWrapper<MaterialCategory>
    {
        /// <summary>
        /// Initializes an instance of <see cref="MaterialCategoryProcessor" />
        /// </summary>
        /// <param name="initialResponseGetter">A function which determines the processor's default response.</param>
        public MaterialCategoryProcessor(Func<string> initialResponseGetter) : base(
            PipeProcessor<MaterialCategory>.CreateInstance<string>(
                n => MaterialCategory.GetByName(n) is MaterialCategory category
                    ? Result<MaterialCategory, string>.Ok(category)
                    : Result<MaterialCategory, string>.Err("Lo siento, no existe esa categoría."),
                new BasicStringProcessor(() => $"{initialResponseGetter()}\nCategorías disponibles:{string.Join(null, MaterialCategory.Categories.Select(c => $"\n        {c}"))}")
            )
        )
        {
        }

    }
}