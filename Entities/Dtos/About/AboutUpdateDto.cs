﻿using Core.Entities;

namespace Entities.Dtos.About
{
    public class AboutUpdateDto : IDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int UpdateUserId { get; set; }
    }
}
