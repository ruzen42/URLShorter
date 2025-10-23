using URLShorter.Services;
using NeoSimpleLogger;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddProvider(new LoggerProvider());

builder.Services.AddSingleton<IUrlShortenerService, InMemoryUrlShortenerService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

await app.RunAsync();