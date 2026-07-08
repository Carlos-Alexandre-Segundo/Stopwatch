var builder = WebApplication.CreateBuilder(args);

builder.Services = WebApplication<StopwatchTimer>();

builder.Services.AddControllers();

builder.Services.AddSingleton<StopwatchTimer>();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
