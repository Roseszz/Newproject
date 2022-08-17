using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Newproject.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Category must ave atleast 5 chaaracter")]
        [MaxLength(20, ErrorMessage = "Category must cuck no more than 20 chaaracter")]
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
