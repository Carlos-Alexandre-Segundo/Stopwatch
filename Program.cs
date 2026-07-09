using ConsoleApp1;

var builder = WebApplication.CreateBuilder(args);

const string ReactPolicy = "ReactPolicy";

builder.Services.AddControllers();

builder.Services.AddSingleton<StopwatchTimer>();
//AddSingleton é para sempre usar a mesma instância, mesmo mudando as requisições https.

builder.Services.AddOpenApi();

var app = builder.Build();

builder.Services.AddCors(options =>
{
    options.AddPolicy(ReactPolicy,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

builder.Services.AddOpenApi();

app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
