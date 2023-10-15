using MongoDB.Driver;
using RinhaBackend;
using RinhaBackend.Repository;
using RinhaBackend.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
Console.WriteLine($"mongodb://{EnvironmentConfig.MongoConnection.User}:{EnvironmentConfig.MongoConnection.Secrete}@{EnvironmentConfig.MongoConnection.Host}/admin");
var mongoClient = new MongoClient($"mongodb://{EnvironmentConfig.MongoConnection.User}:{EnvironmentConfig.MongoConnection.Secrete}@{EnvironmentConfig.MongoConnection.Host}/admin");
builder.Services.AddSingleton<IMongoClient>(_ => mongoClient);
builder.Services.AddScoped<IMongoRepository, MongoRepository>();
builder.Services.AddScoped<IPessoaService, PessoaService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
