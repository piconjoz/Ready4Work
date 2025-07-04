using System.ComponentModel.DataAnnotations;

public class EmailRequest
{
    [Required]
    public string? ToEmail { get; set; }
}