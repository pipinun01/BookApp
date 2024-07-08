using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookApp
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title{ get; set; }
        public string? Description { get; set; }
        public DateTime PublishedOn { get; set; }
        public string? Publisher {  get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }


        //Связи
        public PriceOffer Promotion { get; set; }
        public List<Reviews> Reviews { get; set; }
        public List<BookAuthor> AuthorsLink { get; set; }
        public List<BookTags> TagsLink { get; set; } 
    }
    
}
