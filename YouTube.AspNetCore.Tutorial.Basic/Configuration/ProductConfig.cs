using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;

namespace YouTube.AspNetCore.Tutorial.Basic.Configuration
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(25);

            builder.Navigation(x => x.ProductFeature).AutoInclude();

            builder.HasData(new Product
            {
                Id = 1,
                Name = "Brick",
                Price = 2.5m,
                CategoryId = 1,
                Quantity = 15
            });

           
        }
    }
}
