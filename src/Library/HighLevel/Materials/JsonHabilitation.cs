namespace Library.HighLevel.Materials
{
    /// <summary>
    /// This struct holds the JSON information of an <see cref="Habilitation" />.
    /// </summary>
    public struct JsonHabilitation : IJsonHolder<Habilitation>
    {
        /// <summary>
        /// A link to a document with the necessary habilitations to manipulate a material.
        /// </summary>
        public string DocLink { get; set; }

        /// <summary>
        /// A boolean value which evaluates if the habilitation is validated.
        /// </summary>
        public bool IsCorrect { get; set; }

        /// <summary>
        /// A text that describes the habilitations that a entrepreneur has.
        /// </summary>
        public string DescriptiveText { get; set; }

        /// <inheritdoc />
        public void FromValue(Habilitation value)
        {
            this.DocLink = value.DocLink;
            this.IsCorrect = value.IsCorrect;
            this.DescriptiveText = value.DescriptiveText;
        }

        /// <inheritdoc />
        public Habilitation ToValue() =>
            new Habilitation(this.DocLink, this.IsCorrect, this.DescriptiveText);
    }
}
