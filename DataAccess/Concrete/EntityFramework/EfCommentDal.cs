using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Dtos.Comment;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCommentDal : EfEntityRepositoryBase<Comment, DataBaseContext>, ICommentDal
    {       
        public void Add(Comment comment)
        {
            using (var context = new DataBaseContext())
            {
                context.Comments.Add(comment);
                context.SaveChanges();
            }
        }

        public void Update(Comment comment)
        {
            using (var context = new DataBaseContext())
            {
                var result = (from u in context.Comments
                              where u.Id == comment.Id
                              select u).FirstOrDefault();

                if (result != null)
                {
                    result.BlogId = comment.BlogId;
                    result.ParentId = comment.ParentId;
                    result.CommentText = comment.CommentText;
                    result.Name = comment.Name;
                    result.Email = comment.Email;
                    result.UpdateDate = comment.UpdateDate;
                    result.Status = comment.Status;

                    context.SaveChanges();
                }
            }
        }

        public void UpdateStatus(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = (from r in context.Comments
                              where r.Id == id
                              select r).FirstOrDefault();

                result.Status = true;

                context.SaveChanges();
            }
        }

        public List<CommentViewDto> List()
        {
            using (var context = new DataBaseContext())
            {
                var result = (from r in context.Comments
                              join b in context.Blogs on r.BlogId equals b.Id into blogs
                              from b in blogs.DefaultIfEmpty()
                              where r.Deleted == false
                              orderby r.Status ascending
                              select new
                              {
                                  r.Id,
                                  r.BlogId,
                                  r.ParentId,
                                  r.CommentText,
                                  r.Name,
                                  r.Email,
                                  r.Status,
                                  r.CreateDate,
                                  BlogTitle = b == null ? null : b.Title,
                                  BlogSlug = b == null ? null : b.Slug
                              }).ToList();

                List<CommentViewDto> resumeList = new List<CommentViewDto>();

                foreach (var r in result)
                {
                    resumeList.Add(new CommentViewDto
                    {
                        Id = r.Id,
                        BlogId = r.BlogId,
                        BlogTitle = r.BlogTitle,
                        BlogSlug = r.BlogSlug,
                        ParentId = r.ParentId,
                        CommentText = r.CommentText,
                        Name = r.Name,
                        Email = r.Email,                        
                        Status = r.Status,
                        CreateDate = r.CreateDate.ToString("dd MMMM yyyy HH:mm"),
                    });
                }

                return resumeList;
            }
        }

        public CommentViewDto ListById(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = (from r in context.Comments
                              join b in context.Blogs on r.BlogId equals b.Id into blogs
                              from b in blogs.DefaultIfEmpty()
                              where r.Id == id
                              orderby r.Status ascending
                              select new
                              {
                                  r.Id,
                                  r.BlogId,
                                  r.ParentId,
                                  r.CommentText,
                                  r.Name,
                                  r.Email,
                                  r.Status,
                                  r.CreateDate,
                                  BlogTitle = b == null ? null : b.Title,
                                  BlogSlug = b == null ? null : b.Slug
                              }).FirstOrDefault();

                CommentViewDto contactList = new CommentViewDto()
                {
                    Id = result.Id,
                    BlogId = result.BlogId,
                    BlogTitle = result.BlogTitle,
                    BlogSlug = result.BlogSlug,
                    ParentId = result.ParentId,
                    CommentText = result.CommentText,
                    Name = result.Name,
                    Email = result.Email,
                    Status = result.Status,
                    CreateDate = result.CreateDate.ToString("dd MMMM yyyy HH:mm"),
                };

                return contactList;
            }
        }

        public bool CheckExistById(int id)
        {
            using (var context = new DataBaseContext())
            {
                var exist = context.Comments.Any(c => c.Id == id);
                return exist;
            }
        }

        public void Delete(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = (from c in context.Comments
                              where c.Id == id
                              select c).FirstOrDefault();

                result.Deleted = true;
                result.Status = false;

                context.SaveChanges();
            }
        }
    }
}
