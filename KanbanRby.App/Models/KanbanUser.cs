using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace KanbanRby.Models;
[Table("kanban_user")]
public class KanbanUser : BaseModel
{
    [Column("user_id")]
    public Guid UserId { get; set; }
    
    [Column("kanban_id")]
    public int KanbanId { get; set; }
    
    [Reference(typeof(Kanban), includeInQuery: true)]
    public Kanban KanbanDetails { get; set; }
}