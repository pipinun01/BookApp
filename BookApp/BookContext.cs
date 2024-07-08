using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BookApp
{
    public class BookContext : DbContext
    {   
        public DbSet<Book> Books { get; set; }        
        public DbSet<Author> Authors { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PriceOffer> PriceOffers { get; set; }
        //public DbSet<Review> Reviews { get; set; }


        public BookContext(DbContextOptions<BookContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //// Many-to-Many relationship between Books and Authors
            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.AuthorsLink)
                .HasForeignKey(ba => ba.BookId);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.bookAuthors)
                .HasForeignKey(ba => ba.AuthorId);

            // Many-to-Many relationship between Books and BookTags
            modelBuilder.Entity<BookTags>()
                .HasKey(ba => new { ba.BooksBookId, ba.TagsTagId });

            modelBuilder.Entity<BookTags>()
                .HasOne(ba => ba.Tags)
                .WithMany(b => b.BookTags)
                .HasForeignKey(ba => ba.TagsTagId);

            modelBuilder.Entity<BookTags>()
                .HasOne(ba => ba.Book)
                .WithMany(ba => ba.TagsLink)
                .HasForeignKey(ba => ba.BooksBookId);
            //base.OnModelCreating(modelBuilder);
        }
    }
}
