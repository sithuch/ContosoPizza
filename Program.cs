using ContosoPizza.Data;
using ContosoPizza.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddSqlite<PizzaContext>("Data Source=ContosoPizza.db");
builder.Services.AddScoped<PizzaService>(); // 👈 Add this
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
app.CreateDbIfNotExists();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();