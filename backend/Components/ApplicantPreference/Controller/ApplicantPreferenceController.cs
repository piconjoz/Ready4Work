using Microsoft.AspNetCore.Mvc;
using backend.Components.ApplicantPreference.Services.Interfaces;
using backend.Components.ApplicantPreference.DTOs;
using System.Threading.Tasks;

namespace backend.Components.ApplicantPreference.Controllers
{
    [ApiController]
    [Route("api/applicant/preferences")]
    public class ApplicantPreferenceController : ControllerBase
    {
        private readonly IApplicantPreferenceService _service;
        public ApplicantPreferenceController(IApplicantPreferenceService service) => _service = service;

        // GET  /api/applicant/preferences/{applicantId}
        [HttpGet("{applicantId}")]
        public async Task<ActionResult<ApplicantPreferenceDTO>> Get(int applicantId)
        {
            var dto = await _service.GetByApplicantIdAsync(applicantId);
            return dto == null ? NotFound(new { message = "Preferences not found" }) : Ok(dto);
        }

        // POST /api/applicant/preferences/{applicantId}
        [HttpPost("{applicantId}")]
        public async Task<ActionResult<ApplicantPreferenceDTO>> Upsert(
            int applicantId,
            [FromBody] ApplicantPreferenceDTO dto)
        {
            if (dto == null) return BadRequest(new { message = "Payload required" });
            dto.ApplicantId = applicantId;

            var saved = await _service.CreateOrUpdateAsync(dto);
            return Ok(saved);
        }
    }
}