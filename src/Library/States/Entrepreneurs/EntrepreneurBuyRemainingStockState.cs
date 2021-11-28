using System;
using Library.Core;
using Library.Core.Processing;
using Library.InputHandlers;
using Library.HighLevel.Accountability;
using Library.HighLevel.Entrepreneurs;
using Library.HighLevel.Materials;

namespace Library.States.Entrepreneurs
{
    /// <summary>
    /// This class represents the state of an entrepreneur who can choose to buy the remaining stock of a material publication
    /// after being confirmed that the amount they want is not satisfied by the current stock.
    /// </summary>
    public class EntrepreneurBuyRemainingStockState : InputProcessorState<bool>
    {
        /// <summary>
        /// Initializes an instance of <see cref="EntrepreneurBuyRemainingStockState" />.
        /// </summary>
        /// <param name="id">The entrepreneur's id.</param>
        /// <param name="publication">The publication.</param>
        public EntrepreneurBuyRemainingStockState(string id, AssignedMaterialPublication publication) : base(
            processor: new YesNoProcessor(() => $"No hay suficiente stock para dicha compra (solo quedan {publication.Publication.Amount}).\n¿Quiere comprar esta cantidad?"),
            nextState: b =>
            {
                if (b)
                {
                    DateTime time = DateTime.Today;
                    BoughtMaterialLine purchase = new BoughtMaterialLine(
                        publication.Company.Name,
                        publication.Publication.Material,
                        time,
                        publication.Publication.Price,
                        publication.Publication.Amount);
                    Entrepreneur? entrepreneur = Singleton<EntrepreneurManager>.Instance.GetById(id);
                    entrepreneur!.BoughtMaterials.Add(purchase);
                    publication.Publication.ClearStock();
                }

                return (new EntrepreneurInitialMenuState(id), null);
            },
            exitState: () => (new EntrepreneurInitialMenuState(id), null)
        )
        {
        }
    }
}
