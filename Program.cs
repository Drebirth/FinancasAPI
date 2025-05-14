using Microsoft.EntityFrameworkCore;
using Projeto_FinancasAPI.Context;
using Projeto_FinancasAPI.Models;
using Projeto_FinancasAPI.Repository;
using Projeto_FinancasAPI.Repository.Categorias;
using Projeto_FinancasAPI.Services.Categoria;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoSqlServer")));
//builder.Services.AddDbContext<AppDbContext>(options =>
//options.UseSqlite(builder.Configuration.GetConnectionString("ConexaoSqlite")));

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
//builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<CategoriaService>();


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
