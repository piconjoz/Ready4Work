using backend.Components.Bookmark.DTOs;
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
            var dtos = list.Select(b => new BookmarkDTO
            {
                BookmarkId  = b.BookmarkId,
                ApplicantId = b.ApplicantId,
                JobsId      = b.JobsId,
                CreatedAt   = b.CreatedAt
            }).ToList();
            return Ok(dtos);
        }

        // POST api/bookmark/{applicantId}/{jobsId}
        [HttpPost("{applicantId}/{jobsId}")]
        public async Task<IActionResult> Bookmark(int applicantId, int jobsId)
        {
            var bm = await _service.BookmarkJobAsync(applicantId, jobsId);
            var dto = new BookmarkDTO
            {
                BookmarkId  = bm.BookmarkId,
                ApplicantId = bm.ApplicantId,
                JobsId      = bm.JobsId,
                CreatedAt   = bm.CreatedAt
            };
            return Ok(dto);
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