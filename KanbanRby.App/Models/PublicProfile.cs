using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace KanbanRby.Models;

[Table("profiles")]
public class PublicProfile : BaseModel
{
    [PrimaryKey("id")]
    public Guid id { get; set; }
    
    [Column("email")]
    public string email { get; set; }
    
    [Column("display_name")]
    public string displayName { get; set; }
}