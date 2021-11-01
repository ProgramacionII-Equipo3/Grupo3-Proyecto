namespace Library.HighLevel.Entrepreneurs
{
    /// <summary>
    /// This class represents the specialization's of a entrepreneur
    /// </summary>
    public class EntrepreneurSpecialization 
    {
        /// <summary>
        /// The specialization of entrepreneur's
        /// </summary>
        /// <value></value>
        public string Specialization { get; private set; }

        /// <summary>
        /// The EntrepreneurSpecialization's Constructor
        /// </summary>
        /// <param name="specialization"></param>
        public EntrepreneurSpecialization (string specialization)
        {
            this.Specialization = specialization;
        }
    }
}