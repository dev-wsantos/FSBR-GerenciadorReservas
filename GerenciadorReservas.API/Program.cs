var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

builder.Services.AddCors(o =>
{
    o.AddPolicy("PermitirAcessoAplicacaoMVC",
       policy =>
       {
           policy.WithOrigins("https://localhost:7242/")
                 .AllowAnyMethod()
                 .AllowAnyHeader();
       });
});

app.UseCors("PermitirAcessoAplicacaoMVC");

app.UseAuthorization();

app.MapControllers();

app.Run();
