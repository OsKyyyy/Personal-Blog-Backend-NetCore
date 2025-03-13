using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Dtos.Blog;
using Entities.Dtos.BlogImage;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBlogImageDal : EfEntityRepositoryBase<BlogImage, DataBaseContext>, IBlogImageDal
    {       
        public void Add(List<BlogImage> blogImage)
        {
            using (var context = new DataBaseContext())
            {
                context.BlogImages.AddRange(blogImage);
                context.SaveChanges();
            }
        }

        public List<BlogImageViewDto> ListById(int blogId)
        {
            using (var context = new DataBaseContext())
            {
                var result = (from b in context.BlogImages
                              where b.BlogId == blogId
                              select new
                              {
                                  b.Id,
                                  b.BlogId,
                                  b.Url                                  
                              }).ToList();

                List<BlogImageViewDto> list = new List<BlogImageViewDto>();

                foreach (var b in result)
                {
                    list.Add(new BlogImageViewDto
                    {
                        Id = b.Id,
                        BlogId = b.BlogId,
                        Url = b.Url                       
                    });
                }

                return list;
            }
        }

        public void Delete(string url)
        {
            using (var context = new DataBaseContext())
            {
                var result = (from b in context.BlogImages
                              where b.Url == url
                              select b).FirstOrDefault();

                context.BlogImages.Remove(result);
                context.SaveChanges();
            }
        }
    }
}
