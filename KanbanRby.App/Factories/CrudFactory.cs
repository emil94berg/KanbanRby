using KanbanRby.Factories.Interfaces;
using Supabase.Postgrest;
using Supabase.Postgrest.Models;
using Client = Supabase.Client;
using KanbanRby.Services.Interfaces;  

namespace KanbanRby.Factories;

public class CrudFactory<TModel> : ICrudFactory<TModel> where TModel : BaseModel, new()
{
    private readonly ISupabaseService _supabaseService;

    public CrudFactory(ISupabaseService supabaseService)
    {
        _supabaseService = supabaseService;
    }
    
    private async Task<Client> GetClient() => await  _supabaseService.GetClientAsync();
    
    // Retrieve all records
    public async Task<List<TModel>> GetAllAsync()
    {
        var client = await GetClient();
        var response = await client.From<TModel>().Get();
        return response.Models;
    }
    
    // Retrieve a single record by primary key, id
    public async Task<TModel?> GetByIdAsync(object id)
    {
        var client = await GetClient();
        var response = await client
            .From<TModel>()
            .Filter("id", Constants.Operator.Equals, id.ToString()!)
            .Single();
        return response;
    }
    
    // Insert new record
    public async Task<TModel> CreateAsync(TModel model)
    {
        var client = await GetClient();
        var response = await client.From<TModel>().Insert(model);
        return response.Models.First();
    }
    
    // Update an existing record
    public async Task<TModel> UpdateAsync(TModel model)
    {
        var client = await GetClient();
        var response = await client.From<TModel>().Update(model);
        return response.Models.First();
    }
    
    // Delete a record by primary key, id
    public async Task DeleteAsync(object id)
    {
        var client = await GetClient();
        await client
            .From<TModel>()
            .Filter("id", Constants.Operator.Equals, id.ToString()!)
            .Delete();
    }
    public async Task<List<TModel>> GetByForeignIdAsync(string fk, object id)
    {
        var client = await GetClient();
        var response = await client
            .From<TModel>()
            .Select("*")
            .Filter(fk, Constants.Operator.Equals, id)
            .Get();

        return response.Models;
    }
}