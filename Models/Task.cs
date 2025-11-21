using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace KanbanRby.Models;

[Table("task")]
public class Task : BaseModel
{
    [PrimaryKey("id")]
    public long Id { get; set; }
    
    [Reference(typeof(Card))]
    [Column("card_id")]
    public long CardId { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}