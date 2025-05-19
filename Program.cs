using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Projeto_FinancasAPI.Context;
using Projeto_FinancasAPI.Repository.Categorias;
using Projeto_FinancasAPI.Repository.Contas;
using Projeto_FinancasAPI.Repository.Transacoes;
using Projeto_FinancasAPI.Services.Categorias;
using Projeto_FinancasAPI.Services.Contas;
using Projeto_FinancasAPI.Services.Transacoes;


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

//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//   .AddEntityFrameworkStores<AppDbContext>(); // Corrigido com a referência necessária  


builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>();
//    .AddDefaultTokenProviders();  

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        //options.LoginPath = "/login"; // Redireciona para a página de login
        //options.AccessDeniedPath = "/acesso-negado"; // Página de acesso negado
        options.Cookie.HttpOnly = true; // Protege contra acesso via JavaScript
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Apenas HTTPS
        options.Cookie.Expiration = TimeSpan.FromMinutes(10); // Duração do cookie
    });


builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
//builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));  
builder.Services.AddScoped<IContaRepository, ContaRepository>();
builder.Services.AddScoped<ITransacoesRepository, TransacoesRepository>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<ContaService>();
builder.Services.AddScoped<TransacoesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.  
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
