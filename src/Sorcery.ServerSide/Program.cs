using AspNetStatic;
using MudBlazor.Services;
using Sorcery.ModularCourse;
using Sorcery.ServerSide;
using Sorcery.ServerSide.AspNetStatic;
using Sorcery.ServerSide.Components;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents();

builder.Services.AddSingleton<ICourseFactory, CourseFactory>()
    .AddSingleton<CourseBook>()
    .AddSingleton<BlogBook>();

builder.Services.AddSingleton<IStaticResourcesInfoProvider>(
    new SorceryStaticResourcesInfoProvider()
        .AddAllPages(builder.Environment)
        .AddAllWebContent(builder.Environment));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .DisableAntiforgery();

var outputDirectory = Environment.GetEnvironmentVariable("SOURCERY_STATIC_CONTENT_DIR");
app.GenerateStaticContent(outputDirectory ?? "./static-content", exitWhenDone: true);

app.Run();
