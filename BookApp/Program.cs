using Microsoft.EntityFrameworkCore;

namespace BookApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string connection = "Data Source=DESKTOP-71EDCUE\\SQLEXPRESS;Initial Catalog=BookApp;Integrated Security=True;Encrypt=False";
            var optionsBuilder = new DbContextOptionsBuilder<BookContext>();
            optionsBuilder.UseSqlServer(connection);
            var options = optionsBuilder.Options;
            using (var ctx = new BookContext(options))
            {
                var bookReview = ctx.Books.Include(i => i.Reviews).FirstOrDefault();

                var firstBook = ctx.Books
                    .Include(book => book.AuthorsLink)
                        .ThenInclude(bookAuthor => bookAuthor.Author)
                    .Include(book => book.Reviews)
                    .Include(book => book.TagsLink)
                        .ThenInclude(tags=>tags.Tags)
                    .Include(book => book.Promotion).FirstOrDefault();

                var firstBook_Filter = ctx.Books
                    .Include(book => book.AuthorsLink
                        .OrderBy(bookAuthor => bookAuthor.Order))
                        .ThenInclude(bookauthor => bookauthor.Author)
                    .Include(book => book.Reviews
                        .Where(review => review.NumStars == 5))
                    .Include(book => book.Promotion).First();
               
                var firstBook3 = ctx.Books.First();
                var dfdf = ctx.Entry(firstBook3);
                ctx.Entry(firstBook3).Collection(book=>book.AuthorsLink).Load(); // Явно загружает связующую таблицу,BookAuthor
                foreach (var i in firstBook3.AuthorsLink) // Чтобы загрузить всех возможных авторов, код должен перебрать все записи BookAuthor ...
                {
                    ctx.Entry(i).Reference(bookAuthor=>bookAuthor.Author).Load(); // … и загрузить все связанныеклассы Author
                }
                ctx.Entry(firstBook3).Collection(book => book.Reviews).Load();
                ctx.Entry(firstBook3).Reference(book => book.Promotion).Load();


                var firstBook4 = ctx.Books.First();
                var numReviews = ctx.Entry(firstBook4).Collection(book => book.Reviews).Query().Count();
                var startRating = ctx.Entry(firstBook4).Collection(book => book.Reviews).Query().Select(s => s.NumStars).ToList();

                var firstBook5 = ctx.Books.Select(book => new
                {
                    book.BookId,
                    book.Title,
                    Author = book.AuthorsLink.Select(s => s.Author.Name)
                }).First();

                var df = ctx.Books;

                var answer = StaticBookExtansionDto.MapBookToDto(df).ToList();
            }            
        }
    }
}
