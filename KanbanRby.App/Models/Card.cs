using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace KanbanRby.Models;

[Table("Card")]
public class Card :  BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    
    [Reference(typeof(Column), joinType: ReferenceAttribute.JoinType.Left, includeInQuery:true)]
    [Column("column_id")]
    public int ColumnId { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("description")]
    public string? Description { get; set; }
    
    [Column("row_id")]
    public int RowId { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}