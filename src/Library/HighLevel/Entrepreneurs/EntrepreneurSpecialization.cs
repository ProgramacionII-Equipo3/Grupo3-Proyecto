namespace Library.HighLevel.Entrepreneurs
{
    /// <summary>
    /// This class has the responsibility 
    /// </summary>
    public class EntrepreneurSpecialization 
    {
        public string Specialization { get; private set; }

        public EntrepreneurSpecialization (string specialization)
        {
            this.Specialization = specialization;
        }
    }
}