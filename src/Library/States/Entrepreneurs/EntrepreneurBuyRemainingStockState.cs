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
                    return (new EntrepreneurConfirmPurchaseState(
                        id,
                        publication,
                        publication.Publication.Amount,
                        new Amount(0, publication.Publication.Amount.Unit)),
                        null);
                }
                else
                {
                    return (new EntrepreneurInitialMenuState(id), null);
                }

                
            },
            exitState: () => (new EntrepreneurInitialMenuState(id), null)
        )
        {
        }
    }
}
