namespace YouTube.AspNetCore.Tutorial.Basic.MapperApp
{
    public interface IMapper       
    {
        Destination Map<Source, Destination>(Source request,int depth);
        Destination Map<Source, Destination>(Source request, Destination outcome,int depth);
    }
}
