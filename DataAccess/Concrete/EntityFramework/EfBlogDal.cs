﻿using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Dtos.Blog;

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

                    context.SaveChanges();
                }
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
    }
}
