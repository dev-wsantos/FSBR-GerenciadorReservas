using GerenciadorReservas.Infra.IoC;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirAcessoAplicacaoMVC",
       policy =>
       {
           policy.WithOrigins("https://localhost:7242/")
                 .AllowAnyMethod()
                 .AllowAnyHeader();
       });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("PermitirAcessoAplicacaoMVC");

app.UseAuthorization();

app.MapControllers();

app.Run();
