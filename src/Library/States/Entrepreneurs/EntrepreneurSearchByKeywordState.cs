using System.Collections.Generic;
using System.Linq;
using Library;
using Library.Core;
using Library.Core.Processing;
using Library.HighLevel.Materials;
using Library.HighLevel.Entrepreneurs;
using Library.InputHandlers;
using Library.InputHandlers.Abstractions;

namespace Library.States.Entrepreneurs
{
    /// <summary>
    /// This class represents the state of an entrepreneur that is searching publications with a keyword.
    /// </summary>
    public class EntrepreneurSearchByKeywordState : WrapperState
    {
        /// <summary>
        /// Initializes an instance of <see cref="EntrepreneurSearchByKeywordState" />.
        /// </summary>
        public EntrepreneurSearchByKeywordState(string id): base(
            InputProcessorState.CreateInstance<string>(
                new BasicStringProcessor(() => "Inserte las palabras claves pertenecientes al material que deseas."),
                keyword => 
                {
                    List<AssignedMaterialPublication> publications = Singleton<Searcher>.Instance.SearchByKeyword(keyword);
                    return (new EntrepreneurInitialMenuState(id, string.Join('\n', publications)), null);
                },
                () => (new EntrepreneurInitialMenuState(id), null)
            )
        ) {}
    }
}