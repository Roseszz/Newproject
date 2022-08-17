using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Newproject.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Author  name must be at least 3 characters")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int Age { get; set; }
        public ICollection<Book> Book { get; set; }
    }
}
