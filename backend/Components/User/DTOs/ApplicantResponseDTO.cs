namespace backend.User.DTOs;

public class ApplicantResponseDto
{
    public int ApplicantId { get; set; }
    public int UserId { get; set; }
    public int ProgrammeId { get; set; }
    public int AdmitYear { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // user info included for convenience
    public UserResponseDto? User { get; set; }
}
