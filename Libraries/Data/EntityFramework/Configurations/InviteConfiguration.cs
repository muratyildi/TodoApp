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
    public class InviteConfiguration : IEntityTypeConfiguration<Invite>
    {
        public void Configure(EntityTypeBuilder<Invite> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x=>x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(15); 

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.HasOne(x => x.FromUser)
               .WithMany(u => u.SentInvites)
               .HasForeignKey(x => x.FromUserId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ToUser)
                .WithMany(u => u.ReceivedInvites)
                .HasForeignKey(x => x.ToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Project)
                .WithMany(p => p.Invites)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
