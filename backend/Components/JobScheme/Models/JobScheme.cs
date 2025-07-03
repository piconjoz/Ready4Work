namespace backend.Components.JobScheme.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class JobScheme
{
    [Key]
    [Column("scheme_id")]
    public int SchemeId { get; set; }
    
    [Required]
    [Column("scheme_name")]
    public string SchemeName { get; set; }
    
    // public constructor for ef core
    public JobScheme() { }
    
    // internal constructor - only services can create job schemes
    internal JobScheme(string schemeName)
    {
        SchemeName = schemeName;
    }
    
    // internal getter methods - only services can read data
    internal int GetSchemeId() => SchemeId;
    internal string GetSchemeName() => SchemeName;
    
    // internal setter methods - only services can update data
    internal void SetSchemeId(int schemeId) => SchemeId = schemeId;
    internal void SetSchemeName(string schemeName) => SchemeName = schemeName;
}