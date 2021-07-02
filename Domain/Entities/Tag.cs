using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Tag
    {
        [Key, Required]
        public int Id { get; set; }
        [MaxLength(500), Required]
        public string Name { get; set; }
        public ICollection<ProductTags> Catalogs { get; set; }
    }
}
