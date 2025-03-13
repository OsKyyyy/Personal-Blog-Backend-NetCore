﻿using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Entities.Dtos.Blog
{
    public class BlogUpdateDto : IDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile Image { get; set; }
        public int UpdateUserId { get; set; }
    }
}
