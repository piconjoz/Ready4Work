using System.Collections.Generic;
using backend.Components.Bookmark.Repository;
using BookmarkEntity = backend.Components.Bookmark.Models.Bookmark;

namespace backend.Components.Bookmark.Services
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IBookmarkRepository _repo;

        public BookmarkService(IBookmarkRepository repo)
        {
            _repo = repo;
        }

        public async Task BookmarkJobAsync(int applicantId, int jobsId)
        {
            if (!await _repo.IsBookmarkedAsync(applicantId, jobsId))
            {
                var bm = new BookmarkEntity
                {
                    ApplicantId = applicantId,
                    JobsId = jobsId,
                    CreatedAt = DateTime.UtcNow
                };
                await _repo.AddBookmarkAsync(bm);
            }
        }

        public Task UnbookmarkJobAsync(int applicantId, int jobsId)
            => _repo.RemoveBookmarkAsync(applicantId, jobsId);

        public Task ClearAllBookmarksAsync(int applicantId)
            => _repo.ClearBookmarksAsync(applicantId);

        public Task<List<BookmarkEntity>> GetBookmarksAsync(int applicantId)
            => _repo.GetBookmarksAsync(applicantId);
    }
}