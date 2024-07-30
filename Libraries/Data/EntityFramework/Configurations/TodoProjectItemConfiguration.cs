using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EntityFramework.Configurations
{
    public class TodoProjectItemConfiguration : IEntityTypeConfiguration<TodoProjectItem>
    {
        public void Configure(EntityTypeBuilder<TodoProjectItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.StartDate).IsRequired();

            builder.HasOne(x=>x.TodoProject).WithMany(x=>x.TodoProjectItems).HasForeignKey(x=>x.ProjectId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
