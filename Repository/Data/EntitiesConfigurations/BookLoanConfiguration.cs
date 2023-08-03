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
    public  class BookLoanConfiguration : IEntityTypeConfiguration<BookLoan>
    {
        public void Configure(EntityTypeBuilder<BookLoan> builder)
        {
            builder.ToTable("BookLoans");

            builder.HasKey(bl => bl.Id);

            builder.Property(bl => bl.LoanDate).IsRequired();
            builder.Property(bl => bl.DueDate).IsRequired();
            builder.Property(bl => bl.Returned).IsRequired();

            builder.HasOne(bl => bl.Book)
                   .WithMany(b => b.BookLoans)
                   .HasForeignKey(bl => bl.BookId);

            builder.HasOne(bl => bl.User)
               .WithMany()
               .HasForeignKey(bl => bl.UserId);
        }
    }
}
