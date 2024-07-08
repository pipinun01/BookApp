﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp
{
    public class Reviews
    {
        [Key]
        public int ReviewId { get; set; }
        public string? VoterName { get; set;}
        public int NumStars {  get; set; }
        public string? Comment { get; set; }
        public int BookId { get; set; }

        public Book Book { get; set; }

    }
}
