using System.Collections.Generic;
using BookmarkEntity = backend.Components.Bookmark.Models.Bookmark;
namespace backend.Components.Bookmark.Services
{
    public interface IBookmarkService
    {
        Task<BookmarkEntity> BookmarkJobAsync(int applicantId, int jobsId);
        Task UnbookmarkJobAsync(int applicantId, int jobsId);
        Task ClearAllBookmarksAsync(int applicantId);
        Task<List<BookmarkEntity>> GetBookmarksAsync(int applicantId);
    }
}