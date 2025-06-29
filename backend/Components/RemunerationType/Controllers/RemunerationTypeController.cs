namespace backend.RemunerationType.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using backend.RemunerationType.DTOs;
using backend.RemunerationType.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

[ApiController]
[Route("api/remuneration-types")]
[Authorize(Roles = "Admin")] // Only admin users can manage remuneration types
public class RemunerationTypeController : ControllerBase
{
    private readonly IRemunerationTypeService _remunerationTypeService;

    public RemunerationTypeController(IRemunerationTypeService remunerationTypeService)
    {
        _remunerationTypeService = remunerationTypeService;
    }

    // GET /api/remuneration-types
    // Gets all remuneration types
    [HttpGet]
    public async Task<ActionResult<List<RemunerationTypeResponseDTO>>> GetAllRemunerationTypes()
    {
        try
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            var remunerationTypes = await _remunerationTypeService.GetAllRemunerationTypeDTOsAsync();
            return Ok(remunerationTypes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving remuneration types" });
        }
    }

    // GET /api/remuneration-types/{id}
    // Gets a specific remuneration type by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<RemunerationTypeResponseDTO>> GetRemunerationType(int id)
    {
        try
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            var remunerationType = await _remunerationTypeService.GetRemunerationTypeByIdAsync(id);
            if (remunerationType == null)
            {
                return NotFound(new { message = "Remuneration type not found" });
            }

            var remunerationTypeDTO = _remunerationTypeService.ConvertToResponseDTO(remunerationType);
            return Ok(remunerationTypeDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving the remuneration type" });
        }
    }

    // POST /api/remuneration-types
    // Creates a new remuneration type
    [HttpPost]
    public async Task<ActionResult<RemunerationTypeResponseDTO>> CreateRemunerationType([FromBody] CreateRemunerationTypeRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            var remunerationType = await _remunerationTypeService.CreateRemunerationTypeAsync(request.Type);
            var remunerationTypeDTO = _remunerationTypeService.ConvertToResponseDTO(remunerationType);
            
            return CreatedAtAction(nameof(GetRemunerationType), new { id = remunerationTypeDTO.RemunerationId }, remunerationTypeDTO);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while creating the remuneration type" });
        }
    }

    // PUT /api/remuneration-types/{id}
    // Updates an existing remuneration type
    [HttpPut("{id}")]
    public async Task<ActionResult<RemunerationTypeResponseDTO>> UpdateRemunerationType(int id, [FromBody] UpdateRemunerationTypeRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            var remunerationType = await _remunerationTypeService.UpdateRemunerationTypeAsync(id, request.Type);
            var remunerationTypeDTO = _remunerationTypeService.ConvertToResponseDTO(remunerationType);
            
            return Ok(remunerationTypeDTO);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating the remuneration type" });
        }
    }

    // DELETE /api/remuneration-types/{id}
    // Deletes a remuneration type
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRemunerationType(int id)
    {
        try
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            var result = await _remunerationTypeService.DeleteRemunerationTypeAsync(id);
            if (!result)
            {
                return NotFound(new { message = "Remuneration type not found" });
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting the remuneration type" });
        }
    }

    // Helper methods to extract claims from JWT token
    private int? GetUserIdFromToken()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
        {
            return userId;
        }
        return null;
    }
}

// Helper DTOs for request validation
public class CreateRemunerationTypeRequest
{
    [Required(ErrorMessage = "Type is required")]
    [StringLength(20, MinimumLength = 2, ErrorMessage = "Type must be between 2 and 20 characters")]
    [RegularExpression(@"^[a-zA-Z0-9\s\-_]+$", ErrorMessage = "Type must contain only letters, numbers, spaces, hyphens, and underscores")]
    public string Type { get; set; }
}

public class UpdateRemunerationTypeRequest
{
    [Required(ErrorMessage = "Type is required")]
    [StringLength(20, MinimumLength = 2, ErrorMessage = "Type must be between 2 and 20 characters")]
    [RegularExpression(@"^[a-zA-Z0-9\s\-_]+$", ErrorMessage = "Type must contain only letters, numbers, spaces, hyphens, and underscores")]
    public string Type { get; set; }
}