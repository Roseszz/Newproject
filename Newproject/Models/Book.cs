using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
namespace Newproject.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Book  name must be at least 5 characters")]
        [MaxLength(30)]
        public string Name { get; set; }
        [Url]
        public string Image { get; set; }
        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime TimePublish { get; set; }

        //public ICollection<Order> Orders { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }


        public Author Author { get; set; }

        [Required]
        [Display(Name = "Author name")]
        public int AuthorId { get; set; }

    }
}
