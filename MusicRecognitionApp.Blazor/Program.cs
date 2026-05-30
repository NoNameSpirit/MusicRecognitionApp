using MudBlazor.Services;
using MusicRecognitionApp.Blazor.Components;
using MusicRecognitionApp.Blazor.Components.Pages.Table.Model;
using MusicRecognitionApp.Blazor.Components.Pages.Table.PageTableProvider;
using MusicRecognitionApp.Blazor.Extensions;
using MusicRecognitionApp.Infrastructure.Data.Contexts;
using MusicRecognitionApp.Infrastructure.Data.Entities;
using MusicRecognitionApp.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ITableDetailProvider<RecognizedSongEntity>, TrackDetailProvider>();

builder.Services.AddCoreServices()
                .AddDatabaseServices(builder.Configuration)
                .AddInfrustructureServices()
                .AddApplicationServices()
                .AddAuthServices(builder.Configuration);

builder.Services.AddMudServices();

builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MusicRecognitionContext>();
    context.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
