namespace Library.HighLevel.Entrepreneurs
{
    /// <summary>
    /// This class represents the specialization's of a entrepreneur.
    /// Created because of SRP.
    /// </summary>
    public class Specialization 
    {
        /// <summary>
        /// The specialization of entrepreneur's.
        /// </summary>
        /// <value></value>
        public string Specializations { get; private set; }

        /// <summary>
        /// The EntrepreneurSpecialization's Constructor.
        /// </summary>
        /// <param name="specialization"></param>
        public Specialization (string specialization)
        {
            this.Specializations = specialization;
        }
    }
}