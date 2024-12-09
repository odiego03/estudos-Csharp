using Microsoft.EntityFrameworkCore;
using WebApi8.Data;
using WebApi8.Services.Autor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//aqui diz q os metodos q estao no interface esta tbm implentado no service
builder.Services.AddScoped<IAutorInterface, AutorService>();

//aqui esta dizendo q  antes de construir o nosso projeto primeiro pegue a string de conexao q ta no appSettings e mandar pro nosso dbContext e assim passamos a string  de conexao pra dbContext
builder.Services.AddDbContext<AppDbContext>
(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaulConnection"));

});
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
