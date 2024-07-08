using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp
{
    public class BookAuthor
    {
        //public int BookAuthorId { get; set; }
        
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public int Order {  get; set; }
        public Author Author { get; set; }
        public Book Book { get; set; }
    }
}
