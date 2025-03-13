using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.Dtos.Blog;

namespace DataAccess.Abstract
{
    public interface IBlogDal : IEntityRepository<Blog>
    {
        int Add(Blog blog);
        void Update(Blog blog);
        void Delete(int id);
        List<BlogViewDto> List();
        BlogViewDto ListById(int id);
        BlogViewDto ListBySlug(string slug);
        bool CheckExistById(int id);
    }
}
