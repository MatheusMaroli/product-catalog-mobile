
using System.Collections.Generic;

namespace Mobile.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte[] Img { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
