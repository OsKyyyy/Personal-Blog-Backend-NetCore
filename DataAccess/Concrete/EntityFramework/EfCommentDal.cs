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
