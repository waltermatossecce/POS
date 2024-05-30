using POS.Application.Extensions;
using POS.Infraestructura.Extensions;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;


//politica de CORS
var Cors = "Cors";

// Add services to the container.

builder.Services.AddInjectionInfraestructura(Configuration);
builder.Services.AddInjectionApplication(Configuration);
    
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: Cors,
        builder =>
        {
            builder.WithOrigins("*"); //para que me acepte todos los dominios
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors(Cors);

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
