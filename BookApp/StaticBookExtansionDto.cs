using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp
{
    public static class StaticBookExtansionDto
    {
        public static IQueryable<BookListDto> MapBookToDto(this IQueryable<Book> books)
        {
            return books.Select(s => new BookListDto
            {
                BookId = s.BookId,
                Title = s.Title,
                Price = s.Price,
                PublishedOn = s.PublishedOn,
                ActualPrice = s.Promotion == null ? s.Price : s.Promotion.NewPrice,
                PromotionPromotionalText = s.Promotion == null ? null : s.Promotion.PromotionalText,
                AuthorsOrdered =  s.AuthorsLink.Select(se => se.Author.Name.StartsWith("Akvadetrim 1")).FirstOrDefault().ToString(),
                ReviewsCount = s.Reviews.Count(),
                ReviewsAverageVotes = s.Reviews.Select(rev => (double?)rev.NumStars).Average(),
                TagStrings = s.TagsLink.Select(tag => tag.Tags.Name).ToArray(),
            });
        }
    }
}
