using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace KanbanRby.Models;

[Table("Task")]
public class Task : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    
    [Reference(typeof(Card), joinType: ReferenceAttribute.JoinType.Left, includeInQuery:true)]
    [Column("card_id")]
    public int CardId { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("row_id")]
    public int RowId { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}