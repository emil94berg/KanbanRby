using System.ComponentModel.DataAnnotations.Schema;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace KanbanRby.Models;
[Supabase.Postgrest.Attributes.Table("kanban_user")]
public class KanbanUser : BaseModel
{
    [PrimaryKey("id", false)]
    public long Id { get; set; }
    
    [Supabase.Postgrest.Attributes.Column("kanban_id")]
    public long KanbanId { get; set; }
    
    [Supabase.Postgrest.Attributes.Column("user_id")]
    public Guid UserId { get; set; }
    
    [Reference(typeof(Kanban), includeInQuery: true)]
    public Kanban KanbanDetails { get; set; }
}
