using BoggleWebApi.Services;
using Microsoft.EntityFrameworkCore;
//using BoggleWebApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddDbContext<ApiContext>
//(opt => opt.UseInMemoryDatabase("BoggleDb"));

//ads services and routing logic so controllers/actions can handle requests.
builder.Services.AddControllers();
builder.Services.AddScoped<IBoggleService, BoggleService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//services cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins(
                                "http://localhost:8080",
                                "http://localhost:5000",
                                "http://localhost:4200",
                                "http://127.0.0.1:8080",
                                "http://127.0.0.1:5000",
                                "http://127.0.0.1:5500",
                                "http://127.0.0.1:4200"
                              )
                            .AllowAnyHeader()
                            .AllowAnyMethod();
}));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseHttpsRedirection();

app.UseRouting();

//app cors
app.UseCors("corsapp");

app.UseAuthorization();

app.MapControllers();

app.Run();
