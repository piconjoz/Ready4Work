namespace backend.Components.JobListing.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class JobScheme
{
    [Key]
    [Column("scheme_id")]
    private int SchemeId { get; set; }
    
    [Required]
    [Column("scheme_name")]
    private string SchemeName { get; set; }
    
    // private constructor for ef core
    private JobScheme() { }
    
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