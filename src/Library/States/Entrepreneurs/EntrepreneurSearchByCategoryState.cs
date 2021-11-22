using System.Collections.Generic;
using Library;
using Library.Core;
using Library.Core.Processing;
using Library.HighLevel.Entrepreneurs;
using Library.InputHandlers;
using Library.InputHandlers.Abstractions;
using System.Linq;
using Library.HighLevel.Materials;

namespace Library.States.Entrepreneurs
{
    /// <summary>
    /// This class represents the state of an entrepreneur that is searching publications with a keyword.
    /// </summary>
    public class EntrepreneurSearchByCategoryState : WrapperState
    {
        /// <summary>
        /// Initializes an instance of <see cref="EntrepreneurSearchByKeywordState" />.
        /// </summary>
        public EntrepreneurSearchByCategoryState(string id): base(
            InputProcessorState.CreateInstance<MaterialCategory>(
                new MaterialCategoryProcessor(() => "Inserte la categoría del material que quieres buscar."),
                category =>
                {
                    List<AssignedMaterialPublication> publications = Singleton<Searcher>.Instance.SearchByCategory(category);
                    return (new EntrepreneurInitialMenuState(string.Join('\n', publications)), null);
                },
                () => (new EntrepreneurInitialMenuState(id), null)
            )
        ) {}
    }
}