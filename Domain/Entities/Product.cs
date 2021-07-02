using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Domain.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<ProductTags> ProductTags { get; set; }

        public Product()
        {
            Images = new List<ProductImage>();
            ProductTags = new List<ProductTags>();
        }

        public void AddTag(int tagId)
        {
            ProductTags.Add(new ProductTags() { ProductId = Id, TagId = tagId });
        }

        public void RemoveTag(int tagId)
        {
            var tag = ProductTags.FirstOrDefault(f => f.TagId == tagId);
            ProductTags.Remove(tag);
        }
    }
}
