namespace YouTube.AspNetCore.Tutorial.Basic.Exceptions
{
    public class ServerSideExceptions : Exception
    {
        public ServerSideExceptions(string? message) : base(message)
        {
        }
    }
}
