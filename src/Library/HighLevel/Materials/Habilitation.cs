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
        public readonly string DescriptiveText;

        /// <summary>
        /// Creates an habilitation from JSON data.
        /// </summary>
        /// <param name="docLink">It is a link to a document with the necessary habilitations to manipulate a material.</param>
        /// <param name="isCorrect">Whether the habilitation was already validated.</param>
        /// <param name="descriptiveText">It is a text that describes the habilitations.</param>
        public Habilitation(string docLink, bool isCorrect, string descriptiveText)
        {
            this.DocLink = docLink;
            this.IsCorrect = isCorrect;
            this.DescriptiveText = descriptiveText;
        }

        /// <summary>
        /// Creates an habilitation.
        /// </summary>
        /// <param name="docLink">It is a link to a document with the necessary habilitations to manipulate a material.</param>
        /// <param name="descriptiveText">It is a text that describes the habilitations.</param>
        public Habilitation(string docLink, string descriptiveText)
        {
            this.DocLink = docLink;
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

        /// <inheritdoc />
        public override string ToString()
        {
            string miniText = this.IsCorrect ? "validado" : "no validado";
            return $"{this.DocLink}\n    {this.DescriptiveText} ({miniText})";
        }
    }
}
