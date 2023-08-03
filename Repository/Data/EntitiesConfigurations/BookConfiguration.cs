using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.EntitiesConfigurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Book");
            builder.HasKey(e => e.Id);

            builder.Property(l => l.Title).IsRequired().HasMaxLength(300);
            builder.Property(l => l.Author).IsRequired().HasMaxLength(100);
            builder.Property(l => l.ISBN).HasMaxLength(13);
            builder.Property(l => l.PublicationDate).IsRequired();
            builder.Property(l => l.Pages).IsRequired();
            builder.Property(l => l.Genre).HasMaxLength(50);
            builder.Property(l => l.Available).IsRequired();
            builder.Property(l => l.Summary).HasMaxLength(1000);

            builder.HasMany(l => l.BookLoans)
               .WithOne(bl => bl.Book)
               .HasForeignKey(bl => bl.BookId);
        }
    }
}
