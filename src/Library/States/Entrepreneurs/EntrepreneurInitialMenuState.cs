using Library.HighLevel.Entrepreneurs;
using Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Library.HighLevel.Materials;
using Library.Utils;
using Library.HighLevel.Companies;
using System.Text;

namespace Library.States.Entrepreneurs
{
    /// <summary>
    /// This class represents a <see cref="State" /> for an entrepreneur in the initial menu.
    /// </summary>
    public class EntrepreneurInitialMenuState : MultipleOptionState
    {
        private string? initialResponse;
        private string? id;

        /// <summary>
        /// Initializes an instance of <see cref="EntrepreneurInitialMenuState" />.
        /// </summary>
        public EntrepreneurInitialMenuState(string? id,string? initialResponse = null)
        {
            this.initialResponse=initialResponse;
            this.id = id;
            this.commands = new (string, string, Func<(State, string?)>)[]
            {
                
                ("/searchFK", "Busca materiales utilizando palabras claves.", this.searchByKeyword),
                ("/searchFC", "Busca materiales por categorías.", this.searchByCategory),
                ("/searchFZ", "Busca materiales por zona.", this.searchByZone),
                ("/materialgen","Muestra que materiales son constantemente generados.", this.materialsgen),
                ("/materialSpunt","Muestra que materiales son generados puntualmente.", this.materialspunt),
                ("/ereport","Muestra los reportes de materiales recibidos en cierta fecha.", this.ereport)
            };
        }
        private (State, string?) ereport()
        {
            return (new EntrepreneurCreateReportState(
                Singleton<EntrepreneurManager>.Instance.GetById(this.id).Unwrap()
            ), null);
        }

        private (State, string) materialsgen()
        {
            IEnumerable<AssignedMaterialPublication> gen_publication = Singleton<CompanyManager>.Instance.Publications.Where(
                gen_publication =>
                    gen_publication.Publication.Type.PublicationType == MaterialPublicationTypeData.MaterialPublicationType.CONTINUOUS
            );

            return (this, string.Join('\n', gen_publication) + "\n" + this.GetDefaultResponse());
        }

        private (State, string) materialspunt()
        {
            IEnumerable<AssignedMaterialPublication> spunt_publication = Singleton<CompanyManager>.Instance.Publications.Where(
                spunt_publication =>
                    spunt_publication.Publication.Type.PublicationType == MaterialPublicationTypeData.MaterialPublicationType.SCHEDULED
            );
            return (this, string.Join ('\n', spunt_publication)+"\n"+this.GetDefaultResponse());
        }

        private (State, string?) searchByKeyword()
        {
            return (new EntrepreneurSearchByKeywordState(), null);
        }

        private (State, string?) searchByCategory()
        {
            return (new EntrepreneurSearchByCategoryState(), null);
        }

        private (State, string?) searchByZone()
        {
            return (new EntrepreneurSearchByZoneState(), null);
        }
        
       /// <inheritdoc />
        protected override string GetInitialResponse()
        {
            if(initialResponse is null) return "What do you want to do?";
            string response = initialResponse;
            initialResponse = null;
            return $"{response}\nWhat do you want to do?";
        }

        /// <inheritdoc />
        protected override string GetErrorString() =>
            "Invalid option.";

    }
}