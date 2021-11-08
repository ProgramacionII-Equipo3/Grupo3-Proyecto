namespace Library.HighLevel.Materials
{
    /// <summary>
    /// This class represents habilitations entrepreneurs can possess,
    /// which are necessary to meet certain material manipulation requirements.
    /// We used the principle Expert to create this class.
    /// </summary>
    public class Habilitation
    {
        /// <summary>
        /// A link to a document with the necessary habilitations to manipulate a material.
        /// </summary>
        readonly string DocLink;

        /// <summary>
        /// A boolean value which evaluates if the habilitation is validated.
        /// </summary>
        public bool IsCorrect { get; private set; }

        /// <summary>
        /// Creates an habilitation.
        /// </summary>
        /// <param name="DocLink">It is a link to a document with the necessary habilitations to manipulate a material.</param>
        public Habilitation(string DocLink)
        {
            this.DocLink = DocLink;
            this.IsCorrect = false;
        }

        /// <summary>
        /// Validates the given habilitation.
        /// </summary>
        /// <param name="habilitation">The habilitation to validate.</param>
        public static void Validate(Habilitation habilitation)
        {
            habilitation.IsCorrect = true;
        }
    }
}
