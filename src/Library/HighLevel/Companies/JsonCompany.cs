namespace Library.HighLevel.Companies
{
    /// <summary>
    /// This struct acts as a JSON data holder for companies.
    /// </summary>
    public struct JsonCompany : IJsonHolder<Company>
    {
#warning Implement JsonCompany after the Company data is fused into the class Company.

        void IJsonHolder<Company>.FromValue(Company value)
        {
            throw new System.NotImplementedException();
        }

        Company IJsonHolder<Company>.ToValue()
        {
            throw new System.NotImplementedException();
        }
    }
}