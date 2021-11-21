using System;
using Library.Core;

namespace Library.States.Companies
{
    /// <summary>
    /// 
    /// </summary>
    public class CompanyInitialMenuState : MultipleOptionState
    {
        /// <summary>
        /// 
        /// </summary>
        public CompanyInitialMenuState()
        {
            this.commands = new (string, string, Func<(State, string?)>)[]
            {
                ("/publish", "Permite realizar una publicación de un material",                                     this.PublishMaterial),
                ("/checkhabilitation", "Permite ver las habilitaciones del emprendedor que solicito el material",   this.CheckHabilitation),
                ("/companyreport", "Permite acceder al reporte de todos los materiales enviados por la empresa",    this.CompanyReport),
            };
        }

        private (State, string) PublishMaterial()
        {
            throw new NotImplementedException();
        }

        private (State, string) CheckHabilitation()
        {
            throw new NotImplementedException();
        }

        private (State, string) CompanyReport()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override string GetInitialResponse() =>
            "¿Que deseas hacer?";

        /// <inheritdoc />
        protected override string GetErrorString() =>
            "Lo siento, no reconocí ese comando.";
    }
}