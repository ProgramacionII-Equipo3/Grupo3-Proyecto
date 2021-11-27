using System;
using Library.HighLevel.Accountability;
using Library.HighLevel.Entrepreneurs;
using Library.HighLevel.Companies;
using Library.Core.Processing;
using Library.InputHandlers;
using Library.InputHandlers.Abstractions;
using Library.Utils;
using System.Linq;
using Library.HighLevel.Materials;

namespace Library.States.Entrepreneurs
{
    public class EntrepreneurBuyMaterialState : InputHandlerState
    {
        public EntrepreneurBuyMaterialState(string id) : base(
            exitState: () => new EntrepreneurInitialMenuState(id, null),
            nextState: () => new EntrepreneurInitialMenuState(id, null),
            inputHandler: ProcessorHandler.CreateInstance<(Company, MaterialPublication)>(
                result =>
                {
                    if (result.Item1 is not null && result.Item2 is not null)
                    {
                        MaterialSalesLine sale = new MaterialSalesLine(result.Item2.Material, result.Item2.Amount, result.Item2.Price, new DateTime());
                        result.Item1.MaterialSales.Add(sale);

                        Entrepreneur? entrepreneur = Singleton<EntrepreneurManager>.Instance.GetById(id);
                        BoughtMaterialLine buy = new BoughtMaterialLine(result.Item1.Name, result.Item2.Material, new DateTime(), result.Item2.Price, result.Item2.Amount);
                        entrepreneur?.BoughtMaterials.Add(buy);
                        return "";
                    }
                    else
                    {
                        return "Lo siento, no pude concluir la compra.";
                    }
                    
                },
                new CollectDataProcessor(id)
            )
        ) {}

        private class CollectDataProcessor : FormProcessor<(Company, MaterialPublication)>
        {
            private Company? company;
            private MaterialPublication? publication;

            public CollectDataProcessor(string id)
            {
                this.inputHandlers =  new InputHandler[]
                {
                    ProcessorHandler.CreateInfallibleInstance<string>(
                        cName =>
                        {
                            if (Singleton<CompanyManager>.Instance.GetByName(cName) is Company rCompany)
                            {
                                this.company = rCompany;
                            }
                        },
                        new BasicStringProcessor(() => "Por favor ingresa el nombre de la compañía que tiene publicado el material.")
                    ),
                    ProcessorHandler.CreateInfallibleInstance<string>(
                        pName =>
                        {
                            if (this.company.Publications.Any(p => p.Material.Name == pName))
                            {
                                this.publication = this.company.Publications.Where(p => p.Material.Name == pName).FirstOrDefault();
                            }
                        },
                        new BasicStringProcessor(() => "Por favor ingresa el nombre del material de la publicación.")
                    )
                };
            }

            protected override Result<(Company, MaterialPublication), string> getResult() =>
                Result<(Company, MaterialPublication), string>.Ok((this.company.Unwrap(), this.publication.Unwrap()));
        }
    }
}