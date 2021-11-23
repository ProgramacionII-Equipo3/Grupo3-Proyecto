namespace Library.HighLevel.Companies
{
    /// <summary>
    /// This struct acts as a JSON data holder for the type <see cref="CompanyInvitation" />.
    /// </summary>
    public struct JsonCompanyInvitation : IJsonHolder<CompanyInvitation>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }

        void IJsonHolder<CompanyInvitation>.FromValue(CompanyInvitation value)
        {
            this.Code = value.Code;
        }

        CompanyInvitation IJsonHolder<CompanyInvitation>.ToValue() =>
            new CompanyInvitation(this.Code);
    }
}
