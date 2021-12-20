using System;
using System.Collections.Generic;

namespace NotizBuchClient
{
    public class Notes
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime CreationTime { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }


        // Vergleicht zwei Objekte vom Typ Notes 
        public class NotesComparer : EqualityComparer<Notes>
        {
            public override bool Equals(Notes x, Notes y)
            {
                return x.Id == y.Id &&
                    x.Title == y.Title &&
                    x.CreationTime == y.CreationTime &&
                    x.Username == y.Username &&
                    x.Text == y.Text;
            }

            public override int GetHashCode(Notes obj)
            {
                return (obj.Title + obj.Text).GetHashCode();
            }
        }
    }
}
