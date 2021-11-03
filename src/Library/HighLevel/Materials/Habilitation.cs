namespace Library.HighLevel.Materials
{
    /// <summary>
    /// This abstract class represents habilitations entrepreneurs can possess,
    /// which are necessary to meet certain material manipulation requirements.
    /// </summary>
    public abstract class Habilitation
    {
        /// <summary>
        /// It is a document with the necessary habilitations to manipulate a material
        /// </summary>
        readonly string DocLink;

        /// <summary>
        /// Its a boolean value which evaluate if the requirements are satisfies
        /// </summary>
      
        public bool IsCorrect {get; private set;}

        /// <summary>
        /// Its create a habilitation 
        /// </summary>
        /// <param name="DocLink">It is a document with the necessary habilitations to manipulate a material</param>
        public Habilitation (string DocLink)
        {
            this.DocLink= DocLink;
            this.IsCorrect=false;
        }
        /// <summary>
        /// This method validate the necessary requirements 
        /// </summary>
        /// <param name="habilitation"></param>
        public static void Validate (Habilitation habilitation)
        {
            habilitation.IsCorrect= true;
        }
    }
}
