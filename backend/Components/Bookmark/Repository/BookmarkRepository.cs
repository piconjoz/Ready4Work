using System.Collections.Generic;
using System.Linq;
using backend.Data;
using Microsoft.EntityFrameworkCore;
using BookmarkEntity = backend.Components.Bookmark.Models.Bookmark;

namespace backend.Components.Bookmark.Repository
{
    public class BookmarkRepository : IBookmarkRepository
    {
        private readonly ApplicationDbContext _context;

        public BookmarkRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookmarkEntity>> GetBookmarksAsync(int applicantId)
        {
            return await _context.Bookmarks
                .Where(b => b.ApplicantId == applicantId)
                .ToListAsync();
        }

        public async Task<BookmarkEntity> AddBookmarkAsync(BookmarkEntity bookmark)
        {
            _context.Bookmarks.Add(bookmark);
            await _context.SaveChangesAsync();
            return bookmark;
        }

        public async Task RemoveBookmarkAsync(int applicantId, int jobsId)
        {
            var bm = await _context.Bookmarks
                .FirstOrDefaultAsync(b => b.ApplicantId == applicantId && b.JobsId == jobsId);
            if (bm != null)
            {
                _context.Bookmarks.Remove(bm);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearBookmarksAsync(int applicantId)
        {
            var list = _context.Bookmarks.Where(b => b.ApplicantId == applicantId);
            _context.Bookmarks.RemoveRange(list);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsBookmarkedAsync(int applicantId, int jobsId)
        {
            return await _context.Bookmarks
                .AnyAsync(b => b.ApplicantId == applicantId && b.JobsId == jobsId);
        }
    }
}