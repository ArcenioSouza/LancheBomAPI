using LancheBom.Database;
using LancheBom.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("ConnectionSQLite")));
//string mySqlConnection = builder.Configuration.GetConnectionString("ConnectionMySQL");
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("lanche_bom"));

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddCors();
builder.Services.AddScoped<PedidoService>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lanche Bom", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var scopeService = app.Services.CreateScope();

var context = scopeService.ServiceProvider.GetService<ApplicationDbContext>();
AdicionarDados adicionarDados = new AdicionarDados();
adicionarDados.AdicionarDadosNoBanco(context);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
