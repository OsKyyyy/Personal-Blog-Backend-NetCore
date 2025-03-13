using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.Dtos.Comment;

namespace DataAccess.Abstract
{
    public interface ICommentDal : IEntityRepository<Comment>
    {
        void Add(Comment comment);
        void Update(Comment comment);
        void UpdateStatus(int id);
        List<CommentViewDto> List();
        CommentViewDto ListById(int id);
        void Delete(int id);
        bool CheckExistById(int id);

    }
}
