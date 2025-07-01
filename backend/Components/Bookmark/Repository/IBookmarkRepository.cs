using backend.Components.Bookmark.Models;
using BookmarkEntity = backend.Components.Bookmark.Models.Bookmark;
using System.Collections.Generic;

namespace backend.Components.Bookmark.Repository
{
    public interface IBookmarkRepository
    {

        Task<List<BookmarkEntity>> GetBookmarksAsync(int applicantId);
        Task<BookmarkEntity> AddBookmarkAsync(BookmarkEntity bookmark);
        Task RemoveBookmarkAsync(int applicantId, int jobsId);
        Task ClearBookmarksAsync(int applicantId);
        Task<bool> IsBookmarkedAsync(int applicantId, int jobsId);
    }
}