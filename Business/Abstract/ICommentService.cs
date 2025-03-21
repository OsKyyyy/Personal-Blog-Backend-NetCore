﻿using Core.Utilities.Results.Abstract;
using Entities.Dtos.Comment;

namespace Business.Abstract
{
    public interface ICommentService
    {
        IResult Add(CommentAddDto commentAddDto);
        IResult Update(CommentUpdateDto commentUpdateDto);
        IDataResult<List<CommentViewDto>> List();
        IDataResult<CommentViewDto> ListById(int id);
        IResult CheckExistById(int id);
        IResult Delete(int id);
        IResult UpdateStatus(int id);
    }
}
