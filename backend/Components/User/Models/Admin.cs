namespace backend.Users.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Admin
{
    [Key]
    [Column("admin_id")]
    private int AdminId { get; set; }
    
    [Required]
    [Column("user_id")]
    private int UserId { get; set; }
    
    [Column("created_at")]
    private DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [Column("updated_at")]
    private DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    // private constructor for ef core
    private Admin() { }
    
    // internal constructor - only services can create admins
    internal Admin(int userId)
    {
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
    
    // internal getter methods - only services can read data
    internal int GetAdminId() => AdminId;
    internal int GetUserId() => UserId;
    internal DateTime GetCreatedAt() => CreatedAt;
    internal DateTime GetUpdatedAt() => UpdatedAt;
    
    // internal setter methods - only services can update data
    internal void SetAdminId(int adminId) => AdminId = adminId;
    internal void SetUpdatedAt(DateTime updatedAt) => UpdatedAt = updatedAt;
}