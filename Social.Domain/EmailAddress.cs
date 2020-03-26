namespace Social.Domain
{
    public class EmailAddress : ValueObject<string>
    {
        public EmailAddress(string value) : base(value)
        {

        }
    }
}