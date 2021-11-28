using System.Linq;
using Library.Core.Processing;
using Library.InputHandlers;
using Library.InputHandlers.Abstractions;
using Library.HighLevel.Companies;
using Library.HighLevel.Entrepreneurs;
using Library.HighLevel.Accountability;
using Library.Core;
using Library.Utils;

namespace Library.States.Companies
{
    /// <summary>
    /// When an company wants to revert a sale, this command has the responsibility of do it.
    /// </summary>
    public class CompanyRevertSaleState : InputProcessorState<(MaterialSalesLine, Entrepreneur)>
    {
        /// <summary>
        /// Initializes an instance of <see cref="CompanyRevertSaleState" /> class.
        /// </summary>
        /// <param name="id">The user´s id.</param>
        public CompanyRevertSaleState(string id) : base(
            exitState: () => (new CompanyInitialMenuState(id), null),
            nextState: result =>
                {
                    State newState = new CompanyInitialMenuState(id);
                    if (Singleton<CompanyManager>.Instance.GetCompanyOf(id) is Company company)
                    {
                        if (company.MaterialSales.Where(s => s.SaleID == result.Item1.SaleID).FirstOrDefault() is MaterialSalesLine sale)
                        {
                           switch (sale.Amount.Add(result.Item1.Amount))
                           {
                               case true:
                               {
                                   return (newState, $"La venta ha sido revertida con éxito.\n{newState.GetDefaultResponse()}");
                               }
                               case false:
                               {
                                   return (newState, $"No pude revertir la venta, chequea si ingresaste los datos correctamente.\n{newState.GetDefaultResponse()}");
                               }
                           }
                           
                        }

                        return (newState, $"No pude encontrar la venta, chequea si ingresaste los datos correctamente.\n{newState.GetDefaultResponse()}");
                    }

                    return (newState, "Lo siento, no te reconozco como un representante de una empresa.");
                },
            processor: new SaleDataProcessor(id)
        )
        { }

        private class SaleDataProcessor : FormProcessor<(MaterialSalesLine, Entrepreneur)>
        {
            private MaterialSalesLine? sale;
            private Entrepreneur? entrepreneur;

            public SaleDataProcessor(string id)
            {
                this.inputHandlers = new InputHandler[]
                {                
                    new ProcessorHandler<string>(
                        id =>
                        {
                            if (Singleton<CompanyManager>.Instance.GetCompanyOf(id) is Company company)
                            {
                                if (company.MaterialSales.Where(s => s.SaleID.ToString() == id).FirstOrDefault() is MaterialSalesLine rSale)
                                {
                                    this.sale = rSale;
                                    return null;
                                }

                                return "Lo siento, no encontré una venta con ese ID.";
                            }

                            return "Lo siento, no te reconozco como un representante de compañía.";
                        },
                        new BasicStringProcessor(() => "Ingresa el ID del material vendido.")
                    ),
                    new ProcessorHandler<string>(
                        eName =>
                        {
                            if (Singleton<EntrepreneurManager>.Instance.GetByName(eName) is Entrepreneur entrepreneurBuyer)
                            {
                                if (entrepreneurBuyer.Name == sale!.Buyer)
                                {
                                    this.entrepreneur = entrepreneurBuyer;
                                    return null;
                                }
                                
                                return "No reconozco al nombre del emprendedor ingresado como el comprador del material.";
                            }

                            return "Lo siento, no reconozco un emprendedor por ese nombre.";
                        },
                        new BasicStringProcessor(() => "Por favor ingresa el nombre del emprendedor que compró el material.")
                    )
                };
            }

            protected override Result<(MaterialSalesLine, Entrepreneur), string> getResult() =>
                Result<(MaterialSalesLine, Entrepreneur), string>.Ok((this.sale.Unwrap(), this.entrepreneur.Unwrap()));
        }
    } 
}
