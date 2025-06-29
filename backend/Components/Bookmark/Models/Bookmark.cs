using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Components.Bookmark.Models
{
    public class Bookmark
    {
        [Key]
        public int BookmarkId { get; set; }
        
        [Required]
        public int ApplicantId { get; set; }
        
        [Required]
        public int JobsId { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}