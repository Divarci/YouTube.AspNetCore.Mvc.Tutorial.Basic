namespace YouTube.AspNetCore.Tutorial.Basic.Exceptions
{
    public class ClientSideExceptions : Exception
    {
        public ClientSideExceptions(string? message) : base(message)
        {
        }
    }
}
