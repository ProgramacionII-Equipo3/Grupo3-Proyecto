using System;
using Library.Core.Processing;
using Library.HighLevel.Materials;
using Library.InputHandlers.Abstractions;

namespace Library.InputHandlers
{
    public class MaterialCategoryProcessor : ProcessorWrapper<MaterialCategory>
    {
        public MaterialCategoryProcessor(Func<string> initialResponseGetter) : base(
            PipeProcessor<MaterialCategory>.CreateInstance<string>(
                n => MaterialCategory.GetByName(n) is MaterialCategory category
                    ? Result<MaterialCategory, string>.Ok(category)
                    : Result<MaterialCategory, string>.Err("Lo siento, no existe esa categoría."),
                new BasicStringProcessor(initialResponseGetter)
            )
        )
        {
        }

    }
}