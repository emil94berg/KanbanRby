using KanbanRby.Components;
using KanbanRby.Factories;
using KanbanRby.Factories.Interfaces;
using KanbanRby.Services.Interfaces;
using KanbanRby.Services;
using KanbanRby.Sessions;
using KanbanRby.Sessions.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Services
builder.Services.AddSingleton<ISupabaseService, SupabaseService>();
builder.Services.AddScoped<IKanbanBoardService, KanbanBoardService>();
builder.Services.AddScoped<ISupabaseAuthService, SupabaseAuthService>();
builder.Services.AddScoped<IUserSession, UserSession>();


builder.Services.AddScoped<ICardManagementService, CardManagementService>();
builder.Services.AddScoped(typeof(ICrudFactory<>), typeof(CrudFactory<>));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();