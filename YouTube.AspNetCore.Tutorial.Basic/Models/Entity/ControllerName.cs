namespace YouTube.AspNetCore.Tutorial.Basic.Models.Entity
{
    public class ControllerName
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Domain> Domains { get; set; }
    }
}
