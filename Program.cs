using LifeRPG;
using LifeRPG.Models;     // нужен для ScheduleConfig
using LifeRPG.Services;   // нужен для ShiftScheduleService
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Singleton = один-единственный объект на всё приложение.
// Панель настроек правит именно его, а все остальные читают тот же самый объект.
builder.Services.AddSingleton<ScheduleConfig>();

// ShiftScheduleService в конструкторе просит ScheduleConfig:
//   public ShiftScheduleService(ScheduleConfig config)
// DI сам подставит тот самый singleton-конфиг, который зарегистрирован строкой выше.
builder.Services.AddSingleton<ShiftScheduleService>();

await builder.Build().RunAsync();