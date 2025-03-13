using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Dtos.Blog;
using Entities.Dtos.Comment;
using Entities.Dtos.Tag;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBlogDal : EfEntityRepositoryBase<Blog, DataBaseContext>, IBlogDal
    {       
        public int Add(Blog blog)
        {
            using (var context = new DataBaseContext())
            {
                context.Blogs.Add(blog);
                int rowsAffected = context.SaveChanges(); 

                if (rowsAffected > 0)
                {
                    return blog.Id;
                }
                else
                {
                    return 0;
                }
            }
        }

        public void Update(Blog blog)
        {
            using (var context = new DataBaseContext())
            {
                var result = (from b in context.Blogs
                              where b.Id == blog.Id
                              select b).FirstOrDefault();

                if (result != null)
                {
                    result.Title = blog.Title;
                    result.Content = blog.Content;
                    result.Slug = blog.Slug;
                    result.UpdateDate = blog.UpdateDate;
                    result.UpdateUserId = blog.UpdateUserId;
                }
                if (!string.IsNullOrEmpty(blog.Image))
                {
                    result.Image = blog.Image;
                }

                context.SaveChanges();
            }
        }

        public List<BlogViewDto> List()
        {
            using (var context = new DataBaseContext())
            {
                var result = (from b in context.Blogs
                              join u in context.Users on b.CreateUserId equals u.Id
                              where b.Deleted == false
                              orderby b.CreateDate descending
                              select new
                              {
                                  b.Id,
                                  b.Title,
                                  b.Content,
                                  b.Image,
                                  b.Slug,
                                  b.CreateDate,
                                  u.FirstName,
                                  u.LastName
                              }).ToList();

                List<BlogViewDto> resumeList = new List<BlogViewDto>();

                foreach (var b in result)
                {
                    resumeList.Add(new BlogViewDto
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Content = b.Content,
                        Image = b.Image,
                        Slug = b.Slug,
                        CreateDate = b.CreateDate.ToString("dd MMMM yyyy HH:mm"),
                        CreateUser = b.FirstName + " " + b.LastName
                    });
                }

                return resumeList;
            }
        }
        
        public BlogViewDto ListById(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = (from b in context.Blogs
                              join u in context.Users on b.CreateUserId equals u.Id
                              join t in context.Tags on b.Id equals t.BlogId
                              where b.Id == id
                              select new
                              {
                                  b.Id,
                                  b.Title,
                                  b.Content,
                                  b.Image,
                                  b.Slug,
                                  b.CreateDate,
                                  u.FirstName,
                                  u.LastName,
                                  Tags = context.Tags
                                     .Where(t => t.BlogId == b.Id)
                                     .Select(t => new TagViewDto
                                     {
                                         Id = t.Id,
                                         BlogId = t.BlogId,
                                         Name = t.Name,
                                         CreateDate = t.CreateDate.ToString("dd MMMM yyyy HH:mm"),
                                         CreateUser = context.Users
                                             .Where(u => u.Id == t.CreateUserId)
                                             .Select(u => u.FirstName + " " + u.LastName)
                                             .FirstOrDefault()
                                     }).ToList(),
                                  Comments = context.Comments
                                     .Where(c => c.BlogId == b.Id)
                                     .Select(c => new CommentViewDto
                                     {
                                         Id = c.Id,
                                         BlogId = c.BlogId,
                                         ParentId = c.ParentId,
                                         CommentText = c.CommentText,
                                         Name = c.Name,
                                         Email = c.Email,
                                         CreateDate = c.CreateDate.ToString("dd MMMM yyyy HH:mm")
                                     }).ToList()
                              }).FirstOrDefault();

                var commentTree = BuildCommentHierarchy(result.Comments);

                BlogViewDto list = new BlogViewDto()
                {
                    Id = result.Id,
                    Title = result.Title,
                    Content = result.Content,
                    Image = result.Image,
                    Slug = result.Slug,
                    CreateDate = result.CreateDate.ToString("dd MMMM yyyy HH:mm"),
                    CreateUser = result.FirstName + " " + result.LastName,
                    Tags = result.Tags,
                    Comments = commentTree
                };

                return list;
            }
        }

        public BlogViewDto ListBySlug(string slug)
        {
            using (var context = new DataBaseContext())
            {
                var result = (from b in context.Blogs
                              join u in context.Users on b.CreateUserId equals u.Id
                              join t in context.Tags on b.Id equals t.BlogId
                              where b.Slug == slug
                              select new
                              {
                                  b.Id,
                                  b.Title,
                                  b.Content,
                                  b.Image,
                                  b.Slug,
                                  b.CreateDate,
                                  u.FirstName,
                                  u.LastName,
                                  Tags = context.Tags
                                     .Where(t => t.BlogId == b.Id)
                                     .Select(t => new TagViewDto
                                     {
                                         Id = t.Id,
                                         BlogId = t.BlogId,
                                         Name = t.Name,
                                         CreateDate = t.CreateDate.ToString("dd MMMM yyyy HH:mm"),
                                         CreateUser = context.Users
                                             .Where(u => u.Id == t.CreateUserId)
                                             .Select(u => u.FirstName + " " + u.LastName)
                                             .FirstOrDefault()
                                     }).ToList(),
                                  Comments = context.Comments
                                     .Where(c => c.BlogId == b.Id && c.Status == true && c.Deleted == false)
                                     .Select(c => new CommentViewDto
                                     {
                                         Id = c.Id,
                                         BlogId = c.BlogId,
                                         ParentId = c.ParentId,
                                         CommentText = c.CommentText,
                                         Name = c.Name,
                                         Email = c.Email,
                                         CreateDate = c.CreateDate.ToString("dd MMMM yyyy HH:mm")
                                     }).ToList()
                              }).FirstOrDefault();

                var commentTree = BuildCommentHierarchy(result.Comments);

                BlogViewDto list = new BlogViewDto()
                {
                    Id = result.Id,
                    Title = result.Title,
                    Content = result.Content,
                    Image = result.Image,
                    Slug = result.Slug,
                    CreateDate = result.CreateDate.ToString("dd MMMM yyyy HH:mm"),
                    CreateUser = result.FirstName + " " + result.LastName,
                    Tags = result.Tags,
                    Comments = commentTree
                };

                return list;
            }
        }

        public bool CheckExistById(int id)
        {
            using (var context = new DataBaseContext())
            {
                var exists = context.Blogs.Any(b => b.Id == id);
                return exists;
            }
        }

        public void Delete(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = (from b in context.Blogs
                              where b.Id == id
                              select b).FirstOrDefault();

                result.Deleted = true;

                context.SaveChanges();
            }
        }

        public List<CommentViewDto> BuildCommentHierarchy(List<CommentViewDto> comments)
        {
            var commentMap = comments.ToDictionary(c => c.Id, c => c);
            var commentTree = new List<CommentViewDto>();

            foreach (var comment in comments)
            {
                if (comment.ParentId == 0)
                {
                    commentTree.Add(comment);
                }
                else
                {
                    if (comment.ParentId.HasValue && commentMap.ContainsKey(comment.ParentId.Value))
                    {
                        var parentComment = commentMap[comment.ParentId.Value];
                        if (parentComment.Children == null)
                        {
                            parentComment.Children = new List<CommentViewDto>();
                        }
                        parentComment.Children.Add(comment);
                    }
                }
            }

            return commentTree;
        }
    }
}
