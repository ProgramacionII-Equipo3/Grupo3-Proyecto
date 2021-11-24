using System;
using Library.Core;

namespace Library.States.Companies
{
    /// <summary>
    /// This class has the responsibility of send to a user a message with all the commands that can be used.
    /// </summary>
    public class CompanyInitialMenuState : MultipleOptionState
    {
        private string id;

        /// <summary>
        /// Initializes an instance of <see cref="CompanyInitialMenuState" />
        /// </summary>
        public CompanyInitialMenuState(string id)
        {
            this.id = id;
            this.commands = new (string, string, Func<(State, string?)>)[]
            {
                ("/publish", "Permite realizar una publicación de un material",                                     this.PublishMaterial),
                ("/checkhabilitation", "Permite ver las habilitaciones del emprendedor que solicito el material",   this.CheckHabilitation),
                ("/companyreport", "Permite acceder al reporte de todos los materiales enviados por la empresa",    this.CompanyReport),
            };
        }

        private (State, string?) PublishMaterial()
        {
            return (new CompanyPublishMaterialState(this.id), null);
        }

        private (State, string?) CheckHabilitation()
        {
            return (new CompanyCheckEntrepreneurHabilitationsState(this.id), null);
        }

        private (State, string?) CompanyReport()
        {
            return(new CompanySentReportState(this.id), null);
        }

        /// <inheritdoc />
        protected override string GetInitialResponse() =>
            "¿Que deseas hacer?";

        /// <inheritdoc />
        protected override string GetErrorString() =>
            "Lo siento, no reconocí ese comando.";
    }
}
