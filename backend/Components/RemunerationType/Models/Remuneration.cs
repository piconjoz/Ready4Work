namespace backend.RemunerationType.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class RemunerationType
{
    [Key]
    [Column("renumeration_id")]
    private int RemunerationId { get; set; }
    
    [Required]
    [Column("type")]
    private string Type { get; set; }
    
    // private constructor for ef core
    private RemunerationType() { }
    
    // internal constructor - only services can create remuneration types
    internal RemunerationType(string type)
    {
        Type = type;
    }
    
    // internal getter methods - only services can read data
    internal int GetRemunerationId() => RemunerationId;
    internal string GetType() => Type;
    
    // internal setter methods - only services can update data
    internal void SetRemunerationId(int remunerationId) => RemunerationId = remunerationId;
    internal void SetType(string type) => Type = type;
}