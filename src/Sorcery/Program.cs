// Licensed under MIT, copyright Mateusz Gienieczko, .

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Sorcery;
using Sorcery.ModularCourse;
using Sorcery.Shared;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
builder.Services.AddSingleton<ICourseFactory, CourseFactory>();
builder.Services.AddSingleton<BlogBook>();
builder.Services.AddSingleton<CourseBook>();

await builder.Build().RunAsync();
