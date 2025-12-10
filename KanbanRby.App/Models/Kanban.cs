using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace KanbanRby.Models;

[Table("Kanban")]
public class Kanban : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    
    [Column("user_id")]
    public string? UserId { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("description")]
    public string? Description { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    [Reference(typeof(KanbanUser), ReferenceAttribute.JoinType.Left, foreignKey: "kanban_user")]
    public List<KanbanUser> KanbanUsers { get; set; }
}
