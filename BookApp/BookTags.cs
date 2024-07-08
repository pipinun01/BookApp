using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp
{
    public class BookTags
    {
        public int TagsTagId { get; set; }
        public int BooksBookId { get; set; }

        public Book Book { get; set; }
        public Tag Tags { get; set; }
    }
}
