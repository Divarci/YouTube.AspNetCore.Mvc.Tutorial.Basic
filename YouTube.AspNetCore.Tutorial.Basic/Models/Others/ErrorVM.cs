namespace YouTube.AspNetCore.Tutorial.Basic.Models.Others
{
    public class ErrorVM
    {
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }
    }
}
