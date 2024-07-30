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
    public class TodoProjectUserMapConfiguration : IEntityTypeConfiguration<TodoProjectUserMap>
    {
        public void Configure(EntityTypeBuilder<TodoProjectUserMap> builder)
        {
            builder.HasKey(x => new { x.UserId, x.TodoProjectId });
            builder.HasOne(x => x.TodoProject).WithMany(x => x.TodoProjectUserMap).HasForeignKey(x => x.TodoProjectId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.User).WithMany(x => x.TodoProjectUserMap).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
