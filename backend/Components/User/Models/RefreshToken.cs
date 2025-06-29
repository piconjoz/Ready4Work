using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Components.User.Models
{
    public class RefreshToken
    {
        [Key]
        [Column("refresh_token_id")]
        private int RefreshTokenId { get; set; }

        [Required]
        [Column("user_id")]
        private int UserId { get; set; }

        [Required]
        [Column("token")]
        [StringLength(255)]
        private string Token { get; set; } = string.Empty;

        [Required]
        [Column("expires_at")]
        private DateTime ExpiresAt { get; set; }

        [Column("created_at")]
        private DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("is_revoked")]
        private bool IsRevoked { get; set; } = false;

        [Column("revoked_at")]
        private DateTime? RevokedAt { get; set; }

        // private constructor for ef core
        private RefreshToken() { }

        // internal constructor - only services can create refresh tokens
        internal RefreshToken(int userId, string token, DateTime expiresAt)
        {
            UserId = userId;
            Token = token;
            ExpiresAt = expiresAt;
            CreatedAt = DateTime.UtcNow;
        }

        // internal getter methods - only services can read data
        internal int GetRefreshTokenId() => RefreshTokenId;
        internal int GetUserId() => UserId;
        internal string GetToken() => Token;
        internal DateTime GetExpiresAt() => ExpiresAt;
        internal DateTime GetCreatedAt() => CreatedAt;
        internal bool GetIsRevoked() => IsRevoked;
        internal DateTime? GetRevokedAt() => RevokedAt;

        // internal setter methods - only services can modify
        internal void SetRefreshTokenId(int refreshTokenId) => RefreshTokenId = refreshTokenId;
        internal void SetUpdatedAt(DateTime updatedAt) => CreatedAt = updatedAt; // if you want to track updates

        // business logic methods
        internal void Revoke()
        {
            IsRevoked = true;
            RevokedAt = DateTime.UtcNow;
        }

        internal bool IsExpired() => DateTime.UtcNow >= ExpiresAt;
        internal bool IsValid() => !IsRevoked && !IsExpired();
    }
}