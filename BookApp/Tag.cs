using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp
{
    public class Tag
    {
        public int TagId {  get; set; }
        public string? Name { get; set; }
        public List<BookTags> BookTags { get; set; }
    }
}
