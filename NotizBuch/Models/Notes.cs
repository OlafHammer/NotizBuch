using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NotizBuch.Models
{
    public class Notes
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime CreationTime { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

       

    }
}
