namespace YouTube.AspNetCore.Tutorial.Basic.Exceptions
{
    public class MapperException : Exception
    {
        public MapperException(string? message) : base(message)
        {
        }
    }
}
