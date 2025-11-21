using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace KanbanRby.Models;

[Table("card")]
public class Card :  BaseModel
{
    [PrimaryKey("id")]
    public long Id { get; set; }
    
    [Reference(typeof(Column))]
    [Column("column_id")]
    public long ColumnId { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("description")]
    public string? Description { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}