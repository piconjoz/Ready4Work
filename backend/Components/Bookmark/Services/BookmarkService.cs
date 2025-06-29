using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Components.Bookmark.Repository;
using BookmarkEntity = backend.Components.Bookmark.Models.Bookmark;
using System;

namespace backend.Components.Bookmark.Services
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IBookmarkRepository _repo;

        public BookmarkService(IBookmarkRepository repo)
        {
            _repo = repo;
        }

        public async Task<BookmarkEntity> BookmarkJobAsync(int applicantId, int jobsId)
        {
            if (!await _repo.IsBookmarkedAsync(applicantId, jobsId))
            {
                var bm = new BookmarkEntity
                {
                    ApplicantId = applicantId,
                    JobsId = jobsId,
                    CreatedAt = DateTime.UtcNow
                };
                return await _repo.AddBookmarkAsync(bm);
            }
            // already bookmarked: fetch existing
            var existing = (await _repo.GetBookmarksAsync(applicantId))
                           .FirstOrDefault(b => b.JobsId == jobsId);
            if (existing == null)
            {
                throw new InvalidOperationException("Bookmark not found for the given applicant and job.");
            }
            return existing;
        }

        public Task UnbookmarkJobAsync(int applicantId, int jobsId)
            => _repo.RemoveBookmarkAsync(applicantId, jobsId);

        public Task ClearAllBookmarksAsync(int applicantId)
            => _repo.ClearBookmarksAsync(applicantId);

        public Task<List<BookmarkEntity>> GetBookmarksAsync(int applicantId)
            => _repo.GetBookmarksAsync(applicantId);
    }
}