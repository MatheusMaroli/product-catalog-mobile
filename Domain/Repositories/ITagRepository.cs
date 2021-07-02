using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Repositories
{
    public interface ITagRepository
    {
        public Tag GetTagById(int id);
        public IEnumerable<Tag> GetAll();
        public IEnumerable<ProductTags> GetCatalog(int tagId);
        public void Save(Tag tag);
        public void Update(Tag tag);
    }
}
