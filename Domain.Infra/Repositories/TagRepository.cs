using Domain.Entities;
using Domain.Infra.Contexts;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infra.Repositories
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(ProductCatalogContext context) : base(context)
        {
        }

        public IEnumerable<Tag> GetAll()
        {
            return _context.Tags.ToList();
        }

        public IEnumerable<ProductTags> GetCatalog(int tagId)
        {
            var tag = _context
                .Tags
                .Include(tag => tag.Catalogs)
                .FirstOrDefault(f => f.Id == tagId);
            return tag.Catalogs;
        }

        public Tag GetTagById(int id)
        {
            return _context.Tags.FirstOrDefault(f => f.Id == id);
        }

        public void Save(Tag tag)
        {
            base.GenericSave(tag);
        }

        public void Update(Tag tag)
        {
            base.GenericUpdate(tag);
        }
    }
}
