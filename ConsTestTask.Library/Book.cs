﻿using System;

namespace ConsTestTask.Library
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int? YearPublish { get; set; }
        public string Genre { get; set; }
        public string Contents { get; set; }
    }
}
