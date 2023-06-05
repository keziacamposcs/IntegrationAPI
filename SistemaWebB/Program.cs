using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SistemaWebB.Models;
using SistemaWebB.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//Contexto com Banco de Dados do SQLite
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));
// Fim - SQLite

// Add services to the container.
builder.Services.AddControllersWithViews();

// Adicionar a configuração do Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sistema B - API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Adicionar o Swagger ao pipeline de roteamento
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nome do Seu Aplicativo API V1");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
