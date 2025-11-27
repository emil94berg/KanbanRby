using Supabase.Postgrest.Models;

namespace KanbanRby.Factories.Interfaces;

public interface ICrudFactory<TModel> where TModel : BaseModel, new()
{
    Task<List<TModel>> GetAllAsync();
    Task<TModel?> GetByIdAsync(object id);
    Task<TModel> CreateAsync(TModel model);
    Task<TModel> UpdateAsync(TModel model);
    Task DeleteAsync(object id);
}