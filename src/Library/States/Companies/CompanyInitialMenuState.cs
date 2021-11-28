using System;
using Library.Core;
using Library.HighLevel.Companies;

namespace Library.States.Companies
{
    /// <summary>
    /// This class has the responsibility of send to an user a message with all the commands that can be used.
    /// </summary>
    public class CompanyInitialMenuState : MultipleOptionState
    {
        private string id;

        /// <summary>
        /// Initializes an instance of <see cref="CompanyInitialMenuState" />.
        /// </summary>
        public CompanyInitialMenuState(string id)
        {
            this.id = id;
            this.commands = new (string, string, Func<(State, string?)>)[]
            {
                ("/publish",           "Realizar una publicación de un material.",                                             this.PublishMaterial),
                ("/removepublication", "Borrar una publicación",                                                               this.RemovePublication),
                ("/checkhabilitation", "Mostrar las habilitaciones de un emprendedor que solicito el material",                this.CheckHabilitation),
                ("/checkpublications",  "Mostrar todas las publicaciones de la empresa",                                       this.CheckPublications),
                ("/companyreport",     "Acceder al reporte de los materiales enviados por la empresa a partir de un día dado", this.CompanyReport),
                ("/removesale",        "Invalidate a sale made by an entrepreneur",                                            this.RemoveSale)
            };
        }

        private (State, string?) PublishMaterial()
        {
            return (new CompanyPublishMaterialState(this.id), null);
        }

        private (State, string?) RemovePublication()
        {
            return (new CompanyRemovePublicationState(this.id), null);
        }

        private (State, string?) CheckHabilitation()
        {
            return (new CompanyCheckEntrepreneurHabilitationsState(this.id), null);
        }

        private (State, string?) CheckPublications()
        {
            if (Singleton<CompanyManager>.Instance.GetCompanyOf(this.id) is Company company)
            {
                return (this, $"{string.Join("\n", company.Publications)}");
            }
            else
            {
                return (this, null);
            }
        }

        private (State, string?) CompanyReport()
        {
            return(new CompanySentReportState(this.id), null);
        }

        private (State, string?) RemoveSale()
        {
            return (new CompanyRevertSaleState(this.id), null);
        }

        /// <inheritdoc />
        protected override string getInitialResponse() =>
            "¿Qué deseas hacer?";

        /// <inheritdoc />
        protected override string getErrorString() =>
            "Lo siento, no reconocí ese comando.";
    }
}
