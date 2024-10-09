using Comanda.Api;
using Microsoft.EntityFrameworkCore;
using SistemaDeComandas.BancoDeDados;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


// Add services to the container.
var conexao = builder.Configuration.GetConnectionString("Conexao");
builder.Services.AddDbContext<ComandaContexto>(opt =>
{
    opt.UseMySql(conexao, ServerVersion.Parse("10.4.24-MariaDB"));
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// AQUI criação do banco
using (var e = app.Services.CreateScope())
{
    var banco = e.ServiceProvider.
        GetRequiredService<ComandaContexto>();
    banco.Database.Migrate();
    
    // Semear os dados iniciais
    InicializarDados.Semear(banco); 
}
    




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
