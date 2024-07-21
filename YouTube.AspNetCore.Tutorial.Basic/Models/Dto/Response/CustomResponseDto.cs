namespace YouTube.AspNetCore.Tutorial.Basic.Models.Dto.Response
{
    public class CustomResponseDto<TEntity>
    {
        public TEntity? Data { get; set; }
        public int StatusCode { get; set; }
        public List<string>? Errors { get; set; }
    }
}
