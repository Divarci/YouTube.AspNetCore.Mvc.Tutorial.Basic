using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;

namespace YouTube.AspNetCore.Tutorial.Basic.Configuration
{
    public class DomainConfig : IEntityTypeConfiguration<Domain>
    {
        public void Configure(EntityTypeBuilder<Domain> builder)
        {
            builder.Navigation(x => x.Role).AutoInclude();
            builder.Navigation(x=>x.ControllerName).AutoInclude();
        }
    }
}
