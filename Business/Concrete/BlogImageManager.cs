using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constant;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using System.Text.RegularExpressions;
using Entities.Dtos.BlogImage;
using DataAccess.Concrete.EntityFramework;
using Entities.Dtos.Blog;
using System.Linq;
using System.Security.Policy;
using System;
using Azure.Core;

namespace Business.Concrete
{
    public class BlogImageManager : IBlogImageService
    {
        IBlogImageDal _blogImageDal;

        public BlogImageManager(IBlogImageDal blogImageDal)
        {
            _blogImageDal = blogImageDal;
        }
         
        [SecuredOperation("Admin")]
        public IResult Add(int blogId, string content)
        {
            List<string> imageUrls = ExtractImageUrls(content);
            List<BlogImage> blogImage = new List<BlogImage>();

            foreach (var url in imageUrls)
            {
                blogImage.Add(new BlogImage
                {
                    BlogId = blogId,
                    Url = url,
                });
            }

            _blogImageDal.Add(blogImage);

            return new SuccessResult(Messages.BlogImageAdded);
        }

        public IResult UpdateImages(string content, List<BlogImageViewDto> images)
        {
            List<string> newImageUrls = ExtractImageUrls(content);

            List<string> existingUrls = images.Select(image => image.Url).ToList();

            List<string> urlsToDelete = existingUrls.Except(newImageUrls).ToList();

            if (urlsToDelete.Count > 0)
            {
                foreach (var item in urlsToDelete)
                {
                    _blogImageDal.Delete(item);
                }

                DeleteTemp(urlsToDelete);
            }

            List<string> urlsToAdd = newImageUrls.Except(existingUrls).ToList();

            if (urlsToAdd.Count > 0)
            {
                List<BlogImage> blogImage = new List<BlogImage>();

                foreach (var item in urlsToAdd)
                {
                    blogImage.Add(new BlogImage
                    {
                        BlogId = images[0].BlogId,
                        Url = item,
                    });
                }
                _blogImageDal.Add(blogImage);
            }


            //List<BlogImageViewDto> newBlogImage = new List<BlogImageViewDto>();

            //foreach (var url in imageUrls)
            //{
            //    newBlogImage.Add(new BlogImageViewDto
            //    {
            //        Url = url
            //    });
            //}
            return new SuccessResult("");
        }

        public IDataResult<List<BlogImageViewDto>> ListById(int blogId)
        {
            var result = _blogImageDal.ListById(blogId);
            return new SuccessDataResult<List<BlogImageViewDto>>(result, Messages.BlogImageInfoListed);
        }

        static List<string> ExtractImageUrls(string html)
        {
            List<string> urls = new List<string>();
            string pattern = @"<img[^>]+src=""([^""]+)""";

            MatchCollection matches = Regex.Matches(html, pattern);

            foreach (Match match in matches)
            {
                urls.Add(match.Groups[1].Value);
            }

            return urls;
        }

        public void DeleteTemp(List<string> urlsToDelete)
        {
            foreach (var item in urlsToDelete)
            {
                Uri uri = new Uri(item);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", uri.AbsolutePath.TrimStart('/'));

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }            
        }
    }
}
