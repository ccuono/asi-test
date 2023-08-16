using System.Reflection;
using AsiTest.Http.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var applicationName = Assembly.GetExecutingAssembly().GetName().Name;
    if (applicationName != null)
    {
        var filePath = Path.Combine(System.AppContext.BaseDirectory, $"{applicationName}.xml");
        c.IncludeXmlComments(filePath);
    }
    c.UseDateOnlyTimeOnlyStringConverters();
});

builder.Services.AddDateOnlyTimeOnlyStringConverters();
builder.Services.ConfigureDataServices(builder.Configuration);

if (bool.TryParse(builder.Configuration["database:seed"], out var seedData) && seedData)
{
    builder.Services.SeedData(builder.Configuration);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();