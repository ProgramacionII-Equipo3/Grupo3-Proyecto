using System.Collections.Generic;
using System.Linq;
using Library.Core.Processing;
using Library.Core;
using Library.HighLevel.Accountability;
using Library.HighLevel.Companies;
using Library.HighLevel.Materials;
using Library.InputHandlers.Abstractions;
using Library.InputHandlers;
using Library.Utils;
using Ucu.Poo.Locations.Client;

namespace Library.States.Companies
{
    /// <summary>
    /// This class has the responsibility of publish an offer.
    /// </summary>
    public class CompanyPublishMaterialState : InputHandlerState
    {
        /// <summary>
        /// Initializes an instance of <see cref="CompanyPublishMaterialState" />
        /// </summary>
        /// <param name="id">User's id.</param>
        /// <returns></returns>
        public CompanyPublishMaterialState(string id) : base(
            exitState: () => new CompanyInitialMenuState(id),
            nextState: () => new CompanyInitialMenuState(id),
            inputHandler: ProcessorHandler.CreateInstance<(Material, Amount, Price, Location, MaterialPublicationTypeData, IList<string>)>(
                (result) => 
                {
                    if (Singleton<CompanyManager>.Instance.GetCompanyOf(id) is Company company)
                    {
                        if (company.PublishMaterial(result.Item1, result.Item2, result.Item3, result.Item4, result.Item5, result.Item6))
                        {
                            return null;
                        }
                        return "Disculpa, no pude realizar la publicación, vuelve a intentarlo.";
                    } else
                    {
                        return "Lo siento, no te reconozco como un representante de una empresa.";
                    }
                },
                new CollectDataProcessor(id)
            )
        )
        {
        }

        /// <inheritdoc />
        public override (State?, string?) ProcessMessage(string id, ref UserData data, string msg)
        {
            return base.ProcessMessage(id, ref data, msg);
        }

        private class CollectDataProcessor : FormProcessor<(Material, Amount, Price, Location, MaterialPublicationTypeData, IList<string>)>
        {
            private Material? material;
            private Amount? amount;
            private Price? price;
            private Location? location;
            private MaterialPublicationTypeData? materialPublicationTypeData;
            private IList<string>? keywords;

            /// <summary>
            /// Initializes an instance of <see cref="CollectDataProcessor" />
            /// </summary>
            public CollectDataProcessor(string id)
            {
                this.inputHandlers = new InputHandler[]
                {
                    ProcessorHandler.CreateInstance<Material>(
                        m =>
                        {
                            if(Singleton<CompanyManager>.Instance.GetCompanyOf(id) is Company company)
                            {
                                if(company.Publications.Any(ap => ap.Publication.Material.Name == m.Name))
                                {
                                    return "Ya hay un material con este nombre.";
                                }
                                this.material = m;
                                return null;
                            } else
                            {
                                return "No se puede chequear la unicidad de este material en la compañía.";
                            }
                        },
                        new MaterialProcessor()
                    ),
                    ProcessorHandler.CreateInfallibleInstance<Amount>(
                        a => this.amount = a,
                        new AmountProcessor()
                    ),
                    ProcessorHandler.CreateInfallibleInstance<Price>(
                        p => this.price = p,
                        new PriceProcessor()
                    ),
                    ProcessorHandler.CreateInfallibleInstance<Location>(
                        l => this.location = l,
                        new LocationProcessor(() => "Por favor ingresa la ubicación de donde se encuentra el material.")
                    ),
                    ProcessorHandler.CreateInfallibleInstance<MaterialPublicationTypeData>(
                        t => this.materialPublicationTypeData = t,
                        new MaterialPublicationTypeDataProcessor(() => "Por favor, indica el tipo de la publicación:\n        /normal: Publicación normal\n        /scheduled: Publicación con una fecha concreta\n        /continuous: Publicación constante")
                    ),
                    ProcessorHandler.CreateInfallibleInstance<string[]>(
                        k => this.keywords = k.ToList(),
                        new ListProcessor<string>(() => "Por favor ingrese la lista de palabras claves asociadas al material.", new BasicStringProcessor(
                            () => "Por favor ingrese una de las palabras claves."
                        )
                    ))
                };
            }
            protected override Result<(Material, Amount, Price, Location, MaterialPublicationTypeData, IList<string>), string> getResult() =>
                Result<(Material, Amount, Price, Location, MaterialPublicationTypeData, IList<string>), string>.Ok((this.material.Unwrap(), this.amount.Unwrap(), this.price.Unwrap(), this.location.Unwrap(), this.materialPublicationTypeData.Unwrap(), this.keywords.Unwrap()));
        }

    }
}
