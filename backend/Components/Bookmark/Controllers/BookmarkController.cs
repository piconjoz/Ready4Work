using backend.Components.Bookmark.Models;
using backend.Components.Bookmark.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/bookmarks")]
    public class BookmarkController : ControllerBase
    {
        private readonly IBookmarkService _service;

        public BookmarkController(IBookmarkService service)
        {
            _service = service;
        }

        // GET api/bookmarks/{applicantId}
        [HttpGet("{applicantId}")]
        public async Task<IActionResult> GetBookmarks(int applicantId)
        {
            var list = await _service.GetBookmarksAsync(applicantId);
            return Ok(list);
        }

        // POST api/bookmark/{applicantId}/{jobsId}
        [HttpPost("{applicantId}/{jobsId}")]
        public async Task<IActionResult> Bookmark(int applicantId, int jobsId)
        {
            await _service.BookmarkJobAsync(applicantId, jobsId);
            return Ok(new { message = "Bookmarked" });
        }

        // DELETE api/bookmark/{applicantId}/{jobsId}
        [HttpDelete("{applicantId}/{jobsId}")]
        public async Task<IActionResult> Unbookmark(int applicantId, int jobsId)
        {
            await _service.UnbookmarkJobAsync(applicantId, jobsId);
            return Ok(new { message = "Unbookmarked" });
        }

        // DELETE api/bookmark/{applicantId}/clear
        [HttpDelete("{applicantId}/clear")]
        public async Task<IActionResult> ClearAll(int applicantId)
        {
            await _service.ClearAllBookmarksAsync(applicantId);
            return Ok(new { message = "All bookmarks cleared" });
        }
    }
}