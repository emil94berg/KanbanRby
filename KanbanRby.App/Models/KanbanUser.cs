using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace KanbanRby.Models;
[Table("kanban_user")]
public class KanbanUser : BaseModel
{
    [PrimaryKey("id", false)]
    public long Id { get; set; }
    
    [Column("kanban_id")]
    public long KanbanId { get; set; }
    
    [Column("user_id")]
    public Guid UserId { get; set; }
    
    [Reference(typeof(Kanban), includeInQuery: true)]
    public Kanban KanbanDetails { get; set; }
}
