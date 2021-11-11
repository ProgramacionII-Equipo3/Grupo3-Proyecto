namespace Library.HighLevel.Materials
{
    /// <summary>
    /// This class represents habilitations entrepreneurs can possess,
    /// which are necessary to meet certain material manipulation requirements.
    /// We used the principle Expert and SRP to create this class, it is the 
    /// class itself that validates the habilitation.
    /// </summary>
    public class Habilitation
    {
        /// <summary>
        /// A link to a document with the necessary habilitations to manipulate a material.
        /// </summary>
        public readonly string DocLink;

        /// <summary>
        /// A boolean value which evaluates if the habilitation is validated.
        /// </summary>
        public bool IsCorrect { get; private set; }

        /// <summary>
        /// A text that describes the habilitations that a entrepreneur has.
        /// </summary>
        readonly string DescriptiveText;

        /// <summary>
        /// Creates an habilitation.
        /// </summary>
        /// <param name="DocLink">It is a link to a document with the necessary habilitations to manipulate a material.</param>
        /// <param name="descriptiveText">It is a text that describes the habilitations.</param>
        public Habilitation(string DocLink, string descriptiveText)
        {
            this.DocLink = DocLink;
            this.IsCorrect = false;
            this.DescriptiveText = descriptiveText;
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
