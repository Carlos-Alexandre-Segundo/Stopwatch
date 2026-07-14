using ConsoleApp1;

var builder = WebApplication.CreateBuilder(args);

const string ReactPolicy = "ReactPolicy";

builder.Services.AddControllers();
builder.Services.AddSingleton<StopwatchTimer>();
//AddSingleton é para sempre usar a mesma instância, mesmo mudando as requisições https.
builder.Services.AddOpenApi();

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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors(ReactPolicy);

app.MapControllers();

app.Run();
