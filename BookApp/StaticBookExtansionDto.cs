using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public static IQueryable<BookListDto> OrderBooksBy(this IQueryable<BookListDto> books, OrderByOptions orderByOptions = OrderByOptions.SimpleOrder) 
        {
            switch(orderByOptions)
            {
                //Из-за разбиения на страницы всегда нужно выполнять сортировку.По умолчанию сортировка выполняется по первичному ключу.Это делается быстро
                case OrderByOptions.SimpleOrder:
                    return books.OrderByDescending(x=>x.BookId);
                //Упорядочение книг в зависимости от голосов. Книги, у которых нет ни одного голоса (возвращается null), идут внизу
                case OrderByOptions.ByVotes:
                    return books.OrderByDescending(x => x.ReviewsAverageVotes);
                //Упорядочение по дате публикации, вверху – самые свежие книги
                case OrderByOptions.ByPublicationDate:
                    return books.OrderByDescending(x => x.PublishedOn);
                //ByPriceLowestFirst ByPriceHighestFirst -> Упорядочение по актуальной цене с учетом рекламной цены (по возрастанию и по убыванию цены)
                case OrderByOptions.ByPriceLowestFirst:
                    return books.OrderBy(x => x.ActualPrice);
                case OrderByOptions.ByPriceHighestFirst:
                    return books.OrderByDescending(x => x.ActualPrice);
                default:
                    throw new ArgumentOutOfRangeException(nameof(orderByOptions), orderByOptions, null);
            }
        }
    }

    public enum OrderByOptions
    {
        SimpleOrder,
        ByVotes,
        ByPublicationDate,
        ByPriceLowestFirst,
        ByPriceHighestFirst
    }
}
