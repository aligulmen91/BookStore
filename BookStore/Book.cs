using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore
{
    //this is one of our objects. we deine the things that we will work
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //we define it to have auto-increment field
        public int Id { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
