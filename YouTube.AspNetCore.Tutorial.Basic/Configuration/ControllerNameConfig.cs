using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;

namespace YouTube.AspNetCore.Tutorial.Basic.Configuration
{
    public class ControllerNameConfig : IEntityTypeConfiguration<ControllerName>
    {
        public void Configure(EntityTypeBuilder<ControllerName> builder)
        {
            builder.Navigation(x => x.Domains).AutoInclude();
        }
    }
}
