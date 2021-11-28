using System;
using Library.Core;
using Library.Core.Processing;
using Library.HighLevel.Accountability;
using Library.HighLevel.Entrepreneurs;
using Library.HighLevel.Companies;
using Library.InputHandlers;
using Library.InputHandlers.Abstractions;
using Library.Utils;
using System.Linq;
using Library.HighLevel.Materials;

namespace Library.States.Entrepreneurs
{
    /// <summary>
    /// This class has the responsibility of allow an entrepreneur user buy a material.
    /// </summary>
    public class EntrepreneurBuyMaterialState : InputProcessorState<(AssignedMaterialPublication, Amount)>
    {
        /// <summary>
        /// Initializes an instance of <see cref="EntrepreneurBuyMaterialState" /> class.
        /// </summary>
        /// <param name="id">The user´s id.</param>
        public EntrepreneurBuyMaterialState(string id) : base(
            exitState: () => (new EntrepreneurInitialMenuState(id, null), null),
            nextState: result =>
                {
                    var newState = new EntrepreneurInitialMenuState(id, null);
                    var remainingAmount = new Amount(
                        result.Item1.Publication.Amount.Quantity,
                        result.Item1.Publication.Amount.Unit);
                    switch (remainingAmount.Substract(result.Item2))
                    {
                        case 0:
                            return (new EntrepreneurConfirmPurchaseState(id, result.Item1, result.Item2, remainingAmount), null);
                        case 1:
                            return (newState, $"Las cantidades del material y la compra son inválidas entre sí.\n{newState.GetDefaultResponse()}");
                        case 2:
                            return (new EntrepreneurBuyRemainingStockState(id, result.Item1), null);
                        default:
                            throw new Exception();
                    }
                },
            processor: new CollectDataProcessor(id)
        )
        { }

        private class CollectDataProcessor : FormProcessor<(AssignedMaterialPublication, Amount)>
        {
            private Company? company;
            private MaterialPublication? publication;
            private Amount? amount;

            public CollectDataProcessor(string id)
            {
                this.inputHandlers = new InputHandler[]
                {
                    new ProcessorHandler<string>(
                        cName =>
                        {
                            if (Singleton<CompanyManager>.Instance.GetByName(cName) is Company rCompany)
                            {
                                this.company = rCompany;
                                return null;
                            }

                            return "Esta compañía no existe.";
                        },
                        new BasicStringProcessor(() => "Por favor ingresa el nombre de la compañía que tiene publicado el material.")
                    ),
                    new ProcessorHandler<string>(
                        pName =>
                        {
                            if (this.company!.Publications.Where(p => p.Material.Name == pName).FirstOrDefault() is MaterialPublication publication)
                            {
                                this.publication = publication;
                                return null;
                            }

                            return "Este material no existe dentro de la compañía.";
                        },
                        new BasicStringProcessor(() => "Por favor ingresa el nombre del material de la publicación.")
                    ),
                    ProcessorHandler<Amount>.CreateInfallibleInstance(
                        a => this.amount = a,
                        new AmountProcessor(
                            () => "Por favor ingresa la cantidad de material que desea comprar. (por ejemplo: 7 kg)",
                            () => this.publication!.Material.Measure
                        )
                    )
                };
            }

            protected override Result<(AssignedMaterialPublication, Amount), string> getResult() =>
                Result<(AssignedMaterialPublication, Amount), string>.Ok((new AssignedMaterialPublication(this.publication.Unwrap(), this.company.Unwrap()), this.amount.Unwrap()));
        }
    }
}