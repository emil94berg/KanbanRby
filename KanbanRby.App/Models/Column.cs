using System.ComponentModel.DataAnnotations;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace KanbanRby.Models;

[Table("Column")]
public class Column : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    
    [Reference(typeof(Kanban))]
    [Column("kanban_id")]
    public int KanbanId { get; set; }
    
    [Column("name")]
    public string? Name { get; set; }
    
    [Column("description")]
    public string? Description { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

}