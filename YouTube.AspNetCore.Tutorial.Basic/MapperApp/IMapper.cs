namespace YouTube.AspNetCore.Tutorial.Basic.MapperApp
{
    public interface IMapper<EntityIn, EntityOut> 
        where EntityIn : class
        where EntityOut : class
    {
        Destination Map<Source, Destination>(Source request);
        Destination Map<Source, Destination>(Source request, Destination outcome);
    }
}
