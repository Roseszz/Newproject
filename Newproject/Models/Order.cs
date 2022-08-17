using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System;
namespace Newproject.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int BookId { get; set; }  //FK : liên kết sang PK bảng Book 

        //Order - Mobile: Many To One
        public Book Book { get; set; }  //truy xuất các thông tin của bảng Book 

        public string UserEmail { get; set; }

        [Required]
        public int OrderQuantity { get; set; }

        [Required]
        public double OrderPrice { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }
    }
}
