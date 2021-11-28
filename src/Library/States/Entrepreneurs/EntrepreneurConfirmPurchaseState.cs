using System;
using Library.HighLevel.Accountability;
using Library.HighLevel.Companies;
using Library.HighLevel.Entrepreneurs;
using Library.HighLevel.Materials;
using Library.InputHandlers;
using Library.Utils;

namespace Library.States.Entrepreneurs
{
    /// <summary>
    /// 
    /// </summary>
    public class EntrepreneurConfirmPurchaseState : InputProcessorState<bool>
    {
        /// <summary>
        /// Initializes an instance of <see cref="EntrepreneurBuyRemainingStockState" />.
        /// </summary>
        /// <param name="id">The entrepreneur's id.</param>
        /// <param name="publication">The publication.</param>
        /// <param name="purchasedAmount">The amount of material to purchase.</param>
        /// <param name="remainingAmount">The amount of material that remains in stock after the purchase.</param>
        public EntrepreneurConfirmPurchaseState(string id, AssignedMaterialPublication publication, Amount purchasedAmount, Amount remainingAmount) : base(
            processor: new YesNoProcessor(() => $"El costo de la compra es {MoneyQuantityUtils.Calculate(purchasedAmount, publication.Publication.Price)}.\n¿Quiere realizar esta compra?"),
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
                        purchasedAmount);
                    MaterialSalesLine sale = new MaterialSalesLine(
                        publication.Publication.Material,
                        purchasedAmount,
                        publication.Publication.Price,
                        time);
                    Entrepreneur? entrepreneur = Singleton<EntrepreneurManager>.Instance.GetById(id);
                    Company company = publication.Company;
                    entrepreneur!.BoughtMaterials.Add(purchase);
                    company.MaterialSales.Add(sale);
                    publication.Publication.Amount = remainingAmount;
                }
                return (new EntrepreneurInitialMenuState(id), null);
            },
            exitState: () => (new EntrepreneurInitialMenuState(id), null)
        )
        {
        }
    }
}
