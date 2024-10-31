using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCC.CW1.Frontend._14669.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int NumberOfPages { get; set; }
        public bool IsAvaiable { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}