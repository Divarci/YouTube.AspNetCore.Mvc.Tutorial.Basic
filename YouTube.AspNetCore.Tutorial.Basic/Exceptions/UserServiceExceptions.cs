namespace YouTube.AspNetCore.Tutorial.Basic.Exceptions
{
    public class UserServiceExceptions : Exception
    {
        public UserServiceExceptions(string? message) : base(message)
        {
        }
    }
}
