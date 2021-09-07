using System;
using System.Collections.Generic;
using Advertisement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Advertisement.Infrastructure.DataAccess.EntitiesConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        private Category[] categories = new Category[]
        {
            new Category
            {
                Id = 1,
                Name = "Автомобили"
            }
        };
        
        ///<inheritdoc />>
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.CreatedAt).IsRequired();
            
            builder.HasData(categories);
        }
    }
}