using System;

namespace backend.Components.Bookmark.DTOs
{
    public class BookmarkDTO
    {
        public int BookmarkId { get; set; }
        public int ApplicantId { get; set; }
        public int JobsId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}